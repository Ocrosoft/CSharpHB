using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;

namespace ChessServer
{
    public partial class FormMain : Form
    {
        public enum ClientState
        {
            离线=1,空闲=2,下挑战书=3,接挑战书=4,对弈=5
        }
        //获得真正的IP
        private void splitIP()
        {
            int begin = textBox1.Text.IndexOf("本机IP");
            string IP1 = textBox1.Text.Substring(begin, 50);
            begin = IP1.IndexOf(";");
            int end = IP1.IndexOf("<");
            string IP = IP1.Substring(begin + 1, end - begin - 1);
            comboBoxIP.Items.Add(IP);
            comboBoxIP.SelectedIndex = 0;
        }
        private void getIP(string url)//访问百度搜索"IP"，获得源代码
        {
            string strHTML = "";
            WebClient myWebClient = new WebClient();
            Stream myStream = myWebClient.OpenRead(url);
            StreamReader sr = new StreamReader(myStream, System.Text.Encoding.GetEncoding("utf-8"));
            strHTML = sr.ReadToEnd();
            myStream.Close();
            textBox1.Text = strHTML;
            splitIP();
        }
        IPAddress _serverIP = IPAddress.Parse("0.0.0.0");
        int _serverPort = 0;
        private Socket _socketListen = null;
        private List<Socket> _listClient = new List<Socket>();
        private Thread threadListenConnect = null;
        private Thread threadReceivePacket = null;
        private int playerCnt = 0;
        public FormMain()
        {
            InitializeComponent();
            //getIP("http://www.baidu.com/s?ie=UTF-8&wd=ip");
            IPHostEntry ipHostEntry = Dns.GetHostEntry(Dns.GetHostName());
            comboBoxIP.Items.Add("127.0.0.1");
            for (int i = 0; i < ipHostEntry.AddressList.Length; i++)
            {
                if (ipHostEntry.AddressList[i].ToString().Length <= 15)
                {
                    comboBoxIP.Items.Add(ipHostEntry.AddressList[i].ToString());
                }
            }
            //comboBoxIP.DataSource = ipHostEntry.AddressList;
            //comboBoxIP.Text = ipHostEntry.AddressList[0].ToString();
            comboBoxIP.SelectedIndex = 0;
        }
        private void FormMain_Load(object sender, EventArgs e)
        {
            //
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            try
            {
                _socketListen = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                _serverIP = IPAddress.Parse(comboBoxIP.Text);
                _serverPort = Convert.ToInt32(numericUpDownPort.Value);
                IPEndPoint ipEndPoint = new IPEndPoint(_serverIP, _serverPort);
                _socketListen.Bind(ipEndPoint);

                threadListenConnect = new Thread(ServerListenConnect);
                threadListenConnect.IsBackground = true;
                threadListenConnect.Start();
                buttonStart.Text = "监听中...";
                comboBoxIP.Enabled = false;
                numericUpDownPort.Enabled = false;
                buttonStart.Enabled = false;
            }
            catch (Exception excep)
            {
                MessageBox.Show(this, excep.Message, "异常-Start", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void ServerListenConnect()
        {
            while (true)
            {
                try
                {
                    //最大20个等待接收的传入连接数
                    _socketListen.Listen(20);
                    Socket socket = _socketListen.Accept();
                    _listClient.Add(socket);
                    //提示消息框
                    //IPAddress ip = ((IPEndPoint)socket.RemoteEndPoint).Address;
                    //int port = ((IPEndPoint)socket.RemoteEndPoint).Port;
                    //MessageBox.Show(this, ip.ToString() + "已经通过" + port.ToString() + "端口连接到服务器!", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    threadReceivePacket = new Thread(ServerReceivePacket);
                    threadReceivePacket.IsBackground = true;
                    threadReceivePacket.Start(socket);
                }
                catch (Exception excep)
                {
                    MessageBox.Show(this, excep.Message, "异常-Connect", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        private void ServerReceivePacket(object objSocket)
        {
            while (true)
            {
                try
                {
                    Socket socket = (Socket)objSocket;
                    if (socket.Connected == true)
                    {
                        Byte[] bytePacket = new Byte[4000];
                        int length = socket.Receive(bytePacket);
                        String receivePacket = System.Text.Encoding.UTF8.GetString(bytePacket, 0, length);                        
                        ListViewItem item=new ListViewItem(receivePacket);
                        listViewPacket.Items.Add(item);
                        IPAddress fromIp, toIp;
                        int fromPort, toPort;
                        string content = "";
                        bool result=DecodePacket(receivePacket,out fromIp,out fromPort,out toIp,out toPort,out content);
                        if (result == true)
                        {
                            if (toIp.Equals(_serverIP) == true && toPort == _serverPort) { }
                            else if (toIp.Equals(IPAddress.Parse("255.255.255.255")) == true)
                                ServerRelayBroadcastPacket(content, fromIp, fromPort);
                            else
                                ServerRelayPtpPacket(content, fromIp, fromPort, toIp, toPort);
                            if (content.IndexOf("新人报到-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                playerCnt++;
                                labelClientCnt.Text = "客户端数量:" + playerCnt.ToString();
                                //ListViewItem item2 = new ListViewItem(nickName);
                                //item2.SubItems.Add(fromIp.ToString());
                                //item2.SubItems.Add(fromPort.ToString());
                                //listViewClient.Items.Add(item2);
                                AddItemToListViewClient(nickName, fromIp, fromPort, ClientState.空闲);
                            }
                            else if (content.IndexOf("我开溜了-") == 0)
                            {
                                RemoveItemFromListViewClient(fromIp, fromPort);
                                RemoveNodeFromListClient(socket);
                                playerCnt--;
                                labelClientCnt.Text = "客户端数量:" + playerCnt.ToString();
                                return;
                            }
                            /*
                             * 如果添加这段，会导致每有一个新人，服务端都会添加listview一次。
                            else if (content.IndexOf("回复新人-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                ListViewItem item2 = new ListViewItem(nickName);
                                item2.SubItems.Add(fromIp.ToString());
                                item2.SubItems.Add(fromPort.ToString());
                                listViewClient.Items.Add(item2);
                            }
                            */
                            else if (content.IndexOf("状态通报-") == 0)
                            {
                                ClientState state = (ClientState)Enum.Parse(typeof(ClientState), content.Remove(0, 5));
                                UpdateItemOfListViewClient(fromIp, fromPort, state);
                            }
                        }
                        //MessageBox.Show(this, "收到数据包：" + receivePacket, "数据包", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else return;
                }
                catch (Exception excep)
                {
                    MessageBox.Show(this, excep.Message, "异常-Receive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        public void ServerRelayBroadcastPacket(string content, IPAddress fromIp, int fromPort)
        {
            for (int i = 0; i <= _listClient.Count - 1; i++)
            {
                if (_listClient[i] != null && _listClient[i].Connected == true)
                {
                    try
                    {
                        IPAddress ip = ((IPEndPoint)_listClient[i].RemoteEndPoint).Address;
                        int port = ((IPEndPoint)_listClient[i].RemoteEndPoint).Port;
                        //MessageBox.Show(fromIp.ToString() + fromPort.ToString() + ip.ToString() + port.ToString());
                        if (fromIp.Equals(ip) == false || fromPort != port)
                        {
                            string sendPacket = fromIp.ToString() + "-" + fromPort.ToString() + "-" + ip.ToString() + "-"
                                + port.ToString() + "-" + content;
                            Byte[] bytePacket = System.Text.Encoding.UTF8.GetBytes(sendPacket);
                            _listClient[i].Send(bytePacket);
                        }
                    }
                    catch (Exception excep)
                    {
                        //MessageBox.Show(this, excep.Message, "异常-Receive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        public void ServerRelayPtpPacket(string content, IPAddress fromIp, int fromPort, IPAddress toIp, int toPort)
        {
            for (int i = 0; i <= _listClient.Count - 1; i++)
            {
                if (_listClient[i] != null && _listClient[i].Connected == true)
                {
                    try
                    {
                        IPAddress ip = ((IPEndPoint)_listClient[i].RemoteEndPoint).Address;
                        int port = ((IPEndPoint)_listClient[i].RemoteEndPoint).Port;
                        if (ip.Equals(toIp) == true && port == toPort)
                        {
                            string sendPacket = fromIp.ToString() + "-" + fromPort.ToString() + "-"
                                + toIp.ToString() + "-" + toPort.ToString() + "-" + content;
                            Byte[] bytePacket = System.Text.Encoding.UTF8.GetBytes(sendPacket);
                            //MessageBox.Show(sendPacket);
                            _listClient[i].Send(bytePacket);
                        }
                    }
                    catch (Exception excep)
                    {
                        //MessageBox.Show(this, excep.Message, "异常-Receive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        public bool DecodePacket(string packet, out IPAddress fromIp, out int fromPort, out IPAddress toIp, out int toPort, out string content)
        {
            int count = 0;
            int[] pos = new int[4];
            for(int i=0;i<=packet.Length-1;i++)
                if (packet[i] == '-')
                {
                    pos[count] = i;
                    count++;
                    if (count >= 4)
                        break;
                }
            if (count == 4)
            {
                try
                {
                    fromIp = IPAddress.Parse(packet.Substring(0, pos[0] - 0));
                    fromPort = Convert.ToInt32(packet.Substring(pos[0] + 1, pos[1] - pos[0] - 1));
                    toIp = IPAddress.Parse(packet.Substring(pos[1] + 1, pos[2] - pos[1] - 1));
                    toPort = Convert.ToInt32(packet.Substring(pos[2] + 1, pos[3] - pos[2] - 1));
                    content = packet.Remove(0, pos[3] + 1);
                    return true;
                }
                catch (Exception excep)
                {
                    fromIp = IPAddress.Parse("0.0.0.0");
                    fromPort = 0;
                    toIp = IPAddress.Parse("0.0.0.0");
                    toPort = 0;
                    content = "";
                    return false;
                }
            }
            else
            {
                fromIp = IPAddress.Parse("0.0.0.0");
                fromPort = 0;
                toIp = IPAddress.Parse("0.0.0.0");
                toPort = 0;
                content = "";
                return false;
            }
         }
        public void RemoveItemFromListViewClient(IPAddress ip, int port)
        {
            for (int i = 0; i <= listViewClient.Items.Count - 1; i++)
            {
                if (listViewClient.Items[i].SubItems[1].Text == ip.ToString()
                    && listViewClient.Items[i].SubItems[2].Text == port.ToString())
                {
                    listViewClient.Items.RemoveAt(i);
                    return;
                }
            }
        }
        public void AddItemToListViewClient(string nickName, IPAddress ip, int port,ClientState state)
        {
            for (int i = 0; i <= listViewClient.Items.Count - 1; i++)
            {
                if (listViewClient.Items[i].SubItems[1].Text == ip.ToString()
                    && listViewClient.Items[i].SubItems[2].Text == port.ToString())
                {
                    listViewClient.Items[i].Text = nickName;
                    listViewClient.Items[i].SubItems[3].Text = state.ToString();
                    return;
                }
            }
            ListViewItem item = new ListViewItem(nickName);
            item.SubItems.Add(ip.ToString());
            item.SubItems.Add(port.ToString());
            item.SubItems.Add(state.ToString());
            listViewClient.Items.Add(item);
        }
        public void RemoveNodeFromListClient(Socket socket)
        {
            //MessageBox.Show("test");
            for(int i=_listClient.Count-1;i>=0;i--)
            {
                if (_listClient[i] == socket)
                {
                    try
                    {
                        if (_listClient[i] != null)
                            _listClient[i].Close();
                        _listClient.RemoveAt(i);
                    }
                    catch (Exception excep)
                    {
                        //MessageBox.Show(this, excep.Message, "异常-Receive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
            }
        }
        public void UpdateItemOfListViewClient(IPAddress ip, int port, ClientState newState)
        {
            //对listViewClient进行遍历
            for (int i = 0; i <= listViewClient.Items.Count - 1; i++)
            {
                //如果是待更新的客户端行
                if (listViewClient.Items[i].SubItems[1].Text == ip.ToString()
                    && listViewClient.Items[i].SubItems[2].Text == port.ToString())
                {
                    listViewClient.Items[i].SubItems[3].Text = newState.ToString();
                    return;
                }
            }
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "需要关闭程序吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
            {
                try
                {
                    ServerSendBroadcastPacket("大厅关闭-");
                        if(_socketListen!=null)
                            _socketListen.Close();
                    for(int i=0;i<=_listClient.Count-1;i++)
                        if(_listClient[i]!=null)
                            _listClient[i].Close();
                }
                catch (Exception excep)
                {
                    //
                }
            }
        }
        public void ServerSendBroadcastPacket(string content)
        {
            for(int i=0;i<=_listClient.Count-1;i++)
            {
                if(_listClient[i]!=null&&_listClient[i].Connected==true)
                {
                    try
                    {
                        IPAddress ip=((IPEndPoint)_listClient[i].RemoteEndPoint).Address;
                        int port =((IPEndPoint)_listClient[i].RemoteEndPoint).Port;
                        string sendPacket=_serverIP.ToString()+"-"+_serverPort.ToString()+"-"
                            +ip.ToString()+"-"+port.ToString()+"-"+content;
                        Byte[] bytePacket=System.Text.Encoding.UTF8.GetBytes(sendPacket);
                        _listClient[i].Send(bytePacket);
                    }
                    catch(Exception excep)
                    {
                        //
                    }
                }
            }
        }
    }
}
