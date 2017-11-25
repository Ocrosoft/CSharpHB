using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace RhythmEdit
{
    public partial class FormEditor : Form
    {
        private double _currentTime;//当前时间（s）
        private string _currentTimeString;//当前时间（mm:ss::msms）
        public string _maker="";//制作者
        public string _fileName="";//歌曲名称
        public int _difficulty = 0;//难度
        public string _openSong = "";//打开的歌曲
        public string _openmp3 = "";//打开的MP3
        public string _description = "";//描述
        public Boolean _useKeyBoard = false;//使用快捷键
        public Boolean _playInput = false;//演奏键入模式
        public Boolean _playPause = false;//演奏键入暂停/继续
        public Boolean _exitFlag = false;//关闭flag
        //按键结构体
        struct MyKey
        {
            public string myKeyShow;//音符显示形式（易懂）
            public int timeMin, timeSec, timeMs;//分，秒，毫秒
            public int keyCount;//当前时间音符数
            public Boolean A, S, D, F, Space, J, K, L, Sem;//音符
        }
        MyKey mykey;//实例
        //启动代码(测试mp3)
        private void FormMain_Load(object sender, EventArgs e)
        {
            //执行新建
            MenuItemNew_Click(sender, e);
        }
        //钩子相关
        public struct KeyMSG
        {
            public int vkCode;
            public int scanCode;
            public int flags;
            public int time;
            public int dwExtraInfo;
        }
        public delegate int HookProc(int nCode, Int32 wParam, IntPtr lParam);
        static int hKeyboarfHook = 0;
        HookProc KeyboardHookProcedure;
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int SetWindowsHookEx(int idHook, HookProc lpfn, IntPtr hInstance, int threadId);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern bool UnhookWindowsHookEx(int idHook);
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern int CallNextHookEx(int idHook, int nCode, Int32 wParam, IntPtr lParam);
        //钩子按键事件
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (!_useKeyBoard) return 0;
            if (nCode >= 0)
            {
                KeyMSG m = (KeyMSG)Marshal.PtrToStructure(lParam, typeof(KeyMSG));
                //MessageBox.Show(m.vkCode.ToString());
                if (m.flags == 1&&m.vkCode == 46)//删除,略奇葩，flag是1
                {
                    listBoxSave.Items.Remove(listBoxSave.SelectedItem);
                    listBoxShow.Items.Remove(listBoxShow.SelectedItem);
                    //按取消按钮
                    buttonCancelReWrite.PerformClick();
                    return 1;
                }
                    if (m.flags == 0 && m.vkCode == (int)(Keys.A))
                    {
                        buttonA.PerformClick();
                        return 1;
                    }
                if (m.flags == 0 && m.vkCode == (int)(Keys.S))
                {
                    buttonS.PerformClick();
                    return 1;
                }
                if (m.flags == 0 && m.vkCode == (int)(Keys.D))
                {
                    buttonD.PerformClick();
                    return 1;
                }
                if (m.flags == 0 && m.vkCode == (int)(Keys.F))
                {
                    buttonF.PerformClick();
                    return 1;
                }
                if (m.flags == 0 && (m.vkCode == (int)(Keys.G) || (m.vkCode == (int)(Keys.H) || (m.vkCode == (int)(Keys.Space)))))
                {
                    buttonSpace.PerformClick();
                    return 1;
                }
                if (m.flags == 0 && m.vkCode == (int)(Keys.J))
                {
                    buttonJ.PerformClick();
                    return 1;
                }
                if (m.flags == 0 && m.vkCode == (int)(Keys.K))
                {
                    buttonK.PerformClick();
                    return 1;
                }
                if (m.flags == 0 && m.vkCode == (int)(Keys.L))
                {
                    buttonL.PerformClick();
                    return 1;
                }
                    if (m.flags == 0 && m.vkCode == 186)//分号
                    {
                        buttonSem.PerformClick();
                        return 1;
                    }
                if (!_playInput)//演奏键入模式下无效
                {
                    //WE-读取时间并暂停
                    if (m.flags == 0 && (m.vkCode == (int)Keys.W || m.vkCode == (int)Keys.E))
                    {
                        buttonToCurrentTime.PerformClick();
                        return 1;
                    }
                    //IO-写入并继续播放
                    if (m.flags == 0 && (m.vkCode == (int)Keys.I || m.vkCode == (int)Keys.O))
                    {
                        buttonWriteToList.PerformClick();
                        return 1;
                    }
                    //P-转到当前设定时间并播放
                    if (m.flags == 0 && m.vkCode == (int)Keys.P)
                    {
                        buttonTurnToPlay.PerformClick();
                        return 1;
                    }
                }
                else
                {
                    //演奏模式下使用TYU暂停
                    if (m.flags == 0 && (m.vkCode == (int)Keys.T || m.vkCode == (int)Keys.Y || m.vkCode == (int)Keys.U))
                    {
                        if (musicPlayer.playState == WMPLib.WMPPlayState.wmppsPaused)
                        { musicPlayer.Ctlcontrols.play();return 1; }
                        else {
                            buttonTurnToPlay.PerformClick();
                            return 1;
                        }
                    }
                }
                return 0;
            }
            return CallNextHookEx(hKeyboarfHook, nCode, wParam, lParam);
        }
        //安装钩子
        public void HookStart()
        {
            if (hKeyboarfHook == 0)
            {
                KeyboardHookProcedure = new HookProc(KeyboardHookProc);
                hKeyboarfHook = SetWindowsHookEx(13, KeyboardHookProcedure, Marshal.GetHINSTANCE(Assembly.GetExecutingAssembly().GetModules()[0]), 0);
                if (hKeyboarfHook == 0)
                {
                    HookStop();
                    throw new Exception("SethookWindowsHookEx failed.");
                }
            }
        }
        //卸载钩子
        public void HookStop()
        {
            bool retKeyboard = true;
            if (hKeyboarfHook != 0)
            {
                retKeyboard = UnhookWindowsHookEx(hKeyboarfHook);
                hKeyboarfHook = 0;
            }
            if (!(retKeyboard))
                throw new Exception("UnhookWindowsHookEx failed.");
        }
        //初始化
        public FormEditor()
        {
            InitializeComponent();
            //安装钩子
            try
            {
                HookStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("钩子安装失败！提示信息：" + ex.Message);
            }
        }
        //读取当前时间
        private void buttonToCurrentTime_Click(object sender, EventArgs e)
        {
            //焦点锁到写入按钮
            buttonWriteToList.Focus();
            //暂停播放
            musicPlayer.Ctlcontrols.pause();
            //获取当前播放时间并显示
            _currentTime = musicPlayer.Ctlcontrols.currentPosition;
            _currentTimeString = musicPlayer.Ctlcontrols.currentPositionString;
            if (_currentTimeString == "")
                _currentTimeString = "00:00.000";
            numericMin.Value = Convert.ToInt32(_currentTimeString.Substring(0, 2));
            numericSec.Value = Convert.ToInt32(_currentTimeString.Substring(3, 2));
            numericMs.Value = (int)((_currentTime - (int)_currentTime) * 1000);
        }
        //写到列表中
        private void MenuItemWrite_Click(object sender, EventArgs e)
        {
            //当选择为空时，不进行任何操作
            if (numericKeyCount.Value == 0)return;
            //焦点锁定到当前时间按钮
            buttonToCurrentTime.Focus();
            //继续播放
            musicPlayer.Ctlcontrols.play();
            //获取时间信息
            mykey.timeMin = (int)numericMin.Value;
            mykey.timeSec = (int)numericSec.Value;
            mykey.timeMs = (int)numericMs.Value;
            mykey.keyCount = (int)numericKeyCount.Value;
            //转换成显示格式
            if (numericMin.Value <= 9)
                mykey.myKeyShow = "0";
            else mykey.myKeyShow = "";
            mykey.myKeyShow += numericMin.Value.ToString();
            mykey.myKeyShow += ":";
            if (numericSec.Value <= 9)
                mykey.myKeyShow += "0";
            mykey.myKeyShow +=  numericSec.Value.ToString();
            mykey.myKeyShow += ".";
            if (numericMs.Value == 0)
                mykey.myKeyShow += "000";
            else if (numericMs.Value <= 9)
                mykey.myKeyShow += "00";
            else if (numericMs.Value <= 99)
                mykey.myKeyShow += "0";
            mykey.myKeyShow += numericMs.Value.ToString();
            mykey.myKeyShow += " " + mykey.keyCount;
            //获取音符
            if (checkBoxA.Checked) { mykey.myKeyShow += " A"; mykey.A = true; }
            else mykey.A = false;
            if (checkBoxS.Checked) { mykey.myKeyShow += " S"; mykey.S = true; }
            else mykey.S = false;
            if (checkBoxD.Checked) { mykey.myKeyShow += " D"; mykey.D = true; }
            else mykey.D = false;
            if (checkBoxF.Checked) { mykey.myKeyShow += " F"; mykey.F = true; }
            else mykey.F = false;
            if (checkBoxSpace.Checked) { mykey.myKeyShow += " Space"; mykey.Space = true; }
            else mykey.Space = false;
            if (checkBoxJ.Checked) { mykey.myKeyShow += " J"; mykey.J = true; }
            else mykey.J = false;
            if (checkBoxK.Checked) { mykey.myKeyShow += " K"; mykey.K = true; }
            else mykey.K = false;
            if (checkBoxL.Checked) { mykey.myKeyShow += " L"; mykey.L = true; }
            else mykey.L = false;
            if (checkBoxSem.Checked) { mykey.myKeyShow += " ;"; mykey.Sem = true; }
            else mykey.Sem = false;
            //写入到两个List中
            listBoxSave.Items.Add(mykey);
            listBoxShow.Items.Add(mykey.myKeyShow);
            //清理勾选,计数清零
            checkBoxA.Checked = false;
            checkBoxS.Checked = false;
            checkBoxD.Checked = false;
            checkBoxF.Checked = false;
            checkBoxSpace.Checked = false;
            checkBoxJ.Checked = false;
            checkBoxK.Checked = false;
            checkBoxL.Checked = false;
            checkBoxSem.Checked = false;
            numericKeyCount.Value = 0;
        }
        //计算按键数量
        private void updateKeyCount()
        {
            //计算当前Key的数量
            int keyCount = 0;
            if (checkBoxA.Checked) { keyCount++; }
            if (checkBoxS.Checked) { keyCount++; }
            if (checkBoxD.Checked) { keyCount++; }
            if (checkBoxF.Checked) { keyCount++; }
            if (checkBoxSpace.Checked) { keyCount++; }
            if (checkBoxJ.Checked) { keyCount++; }
            if (checkBoxK.Checked) { keyCount++; }
            if (checkBoxL.Checked) { keyCount++; }
            if (checkBoxSem.Checked) { keyCount++; }
            numericKeyCount.Value = keyCount;
        }
        //按键代码
        private void buttonA_Click(object sender, EventArgs e)
        {
            checkBoxA.Checked = !checkBoxA.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonS_Click(object sender, EventArgs e)
        {
            checkBoxS.Checked = !checkBoxS.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonD_Click(object sender, EventArgs e)
        {
            checkBoxD.Checked = !checkBoxD.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonF_Click(object sender, EventArgs e)
        {
            checkBoxF.Checked = !checkBoxF.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonSpace_Click(object sender, EventArgs e)
        {
            checkBoxSpace.Checked = !checkBoxSpace.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonJ_Click(object sender, EventArgs e)
        {
            checkBoxJ.Checked = !checkBoxJ.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonK_Click(object sender, EventArgs e)
        {
            checkBoxK.Checked = !checkBoxK.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonL_Click(object sender, EventArgs e)
        {
            checkBoxL.Checked = !checkBoxL.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        private void buttonSem_Click(object sender, EventArgs e)
        {
            checkBoxSem.Checked = !checkBoxSem.Checked;
            updateKeyCount();
            buttonWriteToList.Focus();
        }
        //帮助
        private void MenuItemEditorHelp_Click(object sender, EventArgs e)
        {
            //
        }
        //关于
        private void MenuItemAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("©Ocrosoft Inc. All rights reserved.", "About");
        }
        //取消修改
        private void buttonCancelReWrite_Click(object sender, EventArgs e)
        {
            //继续播放
            musicPlayer.Ctlcontrols.play();
            //隐藏及显示按钮
            buttonRewrite.Visible = false;
            buttonDelete.Visible = false;
            buttonSort.Visible = false;
            buttonCancelReWrite.Visible = false;
            buttonWriteToList.Visible = true;
        }
        //修改（按钮）
        private void buttonRewrite_Click(object sender, EventArgs e)
        {
            //继续播放
            musicPlayer.Ctlcontrols.play();
            //当选择为空时，删除这一项，不进行修改
            if (numericKeyCount.Value == 0)
            {
                listBoxSave.Items.Remove(listBoxSave.SelectedItem);
                listBoxShow.Items.Remove(listBoxShow.SelectedItem);
                //按取消按钮
                buttonCancelReWrite.PerformClick();
                return;
            }
            //修改
            mykey.timeMin = (int)numericMin.Value;
            mykey.timeSec = (int)numericSec.Value;
            mykey.timeMs = (int)numericMs.Value;
            mykey.keyCount = (int)numericKeyCount.Value;
            //转换成显示格式
            if (numericMin.Value <= 9)
                mykey.myKeyShow = "0";
            else mykey.myKeyShow = "";
            mykey.myKeyShow += numericMin.Value.ToString();
            mykey.myKeyShow += ":";
            if (numericSec.Value <= 9)
                mykey.myKeyShow += "0";
            mykey.myKeyShow += numericSec.Value.ToString();
            mykey.myKeyShow += ".";
            if (numericMs.Value == 0)
                mykey.myKeyShow += "000";
            else if (numericMs.Value <= 9)
                mykey.myKeyShow += "00";
            else if (numericMs.Value <= 99)
                mykey.myKeyShow += "0";
            mykey.myKeyShow += numericMs.Value.ToString();
            mykey.myKeyShow += " " + mykey.keyCount;
            if (checkBoxA.Checked == true) { mykey.myKeyShow += " A"; mykey.A = true; }
            else mykey.A = false;
            if (checkBoxS.Checked == true) { mykey.myKeyShow += " S"; mykey.S = true; }
            else mykey.S = false;
            if (checkBoxD.Checked == true) { mykey.myKeyShow += " D"; mykey.D = true; }
            else mykey.D = false;
            if (checkBoxF.Checked == true) { mykey.myKeyShow += " F"; mykey.F = true; }
            else mykey.F = false;
            if (checkBoxSpace.Checked == true) { mykey.myKeyShow += " Space"; mykey.Space = true; }
            else mykey.Space = false;
            if (checkBoxJ.Checked == true) { mykey.myKeyShow += " J"; mykey.J = true; }
            else mykey.J = false;
            if (checkBoxK.Checked == true) { mykey.myKeyShow += " K"; mykey.K = true; }
            else mykey.K = false;
            if (checkBoxL.Checked == true) { mykey.myKeyShow += " L"; mykey.L = true; }
            else mykey.L = false;
            if (checkBoxSem.Checked == true) { mykey.myKeyShow += " ;"; mykey.Sem = true; }
            else mykey.Sem = false;
            //修改listbox
            listBoxSave.Items[listBoxSave.SelectedIndex] = mykey;
            listBoxShow.Items[listBoxShow.SelectedIndex] = mykey.myKeyShow;
            //按取消按钮
            buttonCancelReWrite.PerformClick();
        }
        //修改作者,难度,歌曲名信息.
        private void MenuItemFileInfo_Click(object sender, EventArgs e)
        {
            FileInfo myFileInfo = new FileInfo();
            myFileInfo.fileName.Text = _fileName;
            myFileInfo.maker.Text = _maker;
            myFileInfo.Diff.SelectedIndex = _difficulty;
            myFileInfo.textBox1.Text = _description;
            myFileInfo.ShowDialog();
            if (myFileInfo.DialogResult == DialogResult.OK)
            {
                _fileName = myFileInfo.fileName.Text;
                _maker = myFileInfo.maker.Text;
                _difficulty = myFileInfo.Diff.SelectedIndex;
                _description = myFileInfo.textBox1.Text;
            }
        }
        //即时测试,保存并打开测试
        private void MenuItemTest_Click(object sender, EventArgs e)
        {
            //
        }
        //保存
        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            if (_openmp3 == "")
            {
                MessageBox.Show(this, "未打开mp3文件，请重新打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MenuItemOpen_Click(sender, e);
                return;
            }
            if (_maker == "" || _fileName == "")
            {
                MessageBox.Show(this,"无文件信息，请修改！","提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                MenuItemFileInfo_Click(sender,e);
                return;
            }
            if (_openSong == "")
                if (SaveFileDlg.ShowDialog() == DialogResult.OK)
                    _openSong = SaveFileDlg.FileName;
                else return;
            string savePath = Path.GetTempPath() + "RTMP\\";
            if (Directory.Exists(Path.GetTempPath() + "RTMP"))
                Directory.Delete(Path.GetTempPath() + "RTMP",true);
            Directory.CreateDirectory(savePath);
            File.Copy(_openmp3, savePath + "song.mp3");
            FileStream fileStream = new FileStream(savePath + "song.map", FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            //文件信息
            binaryWriter.Write(_fileName);//曲名
            binaryWriter.Write(_maker);//制作者
            //binaryWriter.Write(1);//模式，已删除
            binaryWriter.Write(_difficulty);//难度
            binaryWriter.Write(_description);//描述
            binaryWriter.Write(listBoxSave.Items.Count);//数目
            //音符信息
            for (int i = 0; i < listBoxSave.Items.Count; i++)
            {
                mykey = (MyKey)listBoxSave.Items[i];
                binaryWriter.Write(mykey.myKeyShow);
                binaryWriter.Write(mykey.timeMin);
                binaryWriter.Write(mykey.timeSec);
                binaryWriter.Write(mykey.timeMs);
                binaryWriter.Write(mykey.keyCount);
                binaryWriter.Write(mykey.A);
                binaryWriter.Write(mykey.S);
                binaryWriter.Write(mykey.D);
                binaryWriter.Write(mykey.F);
                binaryWriter.Write(mykey.Space);
                binaryWriter.Write(mykey.J);
                binaryWriter.Write(mykey.K);
                binaryWriter.Write(mykey.L);
                binaryWriter.Write(mykey.Sem);
            }
            binaryWriter.Close();
            fileStream.Close();
            CreateZipFile(savePath, savePath + _fileName + ".rtm");
            System.IO.File.Copy(savePath + _fileName + ".rtm", _openSong,true);
        }
        //另存为
        private void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            //
        }
        //打开
        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            //打开
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(openFileDlg.FileName);
                if (extension == ".mp3" || extension == ".MP3")
                {
                    _openmp3 = openFileDlg.FileName;
                    musicPlayer.URL = _openmp3;
                    return;
                }
                else
                    _openSong = openFileDlg.FileName;
            }
            else return;
            UnZipFile(_openSong);
            string songPath = Path.GetTempPath() + "ROTMP\\";
            _openmp3 = songPath + "song.mp3";
            FileStream fileStream = new FileStream(songPath + "song.map", FileMode.Open, FileAccess.Read);
            BinaryReader binaryReader = new BinaryReader(fileStream);
            //读取
            _fileName = binaryReader.ReadString();//曲名
            _maker = binaryReader.ReadString();//制作者
            //int pattern = binaryReader.ReadInt32();//模式已删除
            _difficulty = binaryReader.ReadInt32();//难度
            _description = binaryReader.ReadString();//描述
            int n = binaryReader.ReadInt32();//数目
            listBoxSave.Items.Clear();
            listBoxShow.Items.Clear();
            for (int i = 0; i < n; i++)
            {
                mykey.myKeyShow = binaryReader.ReadString();
                mykey.timeMin = binaryReader.ReadInt32();
                mykey.timeSec = binaryReader.ReadInt32();
                mykey.timeMs = binaryReader.ReadInt32();
                mykey.keyCount = binaryReader.ReadInt32();
                mykey.A = binaryReader.ReadBoolean();
                mykey.S = binaryReader.ReadBoolean();
                mykey.D = binaryReader.ReadBoolean();
                mykey.F = binaryReader.ReadBoolean();
                mykey.Space = binaryReader.ReadBoolean();
                mykey.J = binaryReader.ReadBoolean();
                mykey.K = binaryReader.ReadBoolean();
                mykey.L = binaryReader.ReadBoolean();
                mykey.Sem = binaryReader.ReadBoolean();
                listBoxSave.Items.Add(mykey);
                listBoxShow.Items.Add(mykey.myKeyShow);
            }
            musicPlayer.URL = _openmp3;
        }
        //退出
        private void MenuItemExit_Click(object sender, EventArgs e)
        {
            if (_openmp3 == "")
            {
                this.Close();
                return;
            }
            if (MessageBox.Show(this, "是否需要保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                if (_maker == "" || _fileName == "")
                {
                    MessageBox.Show(this, "无文件信息，请修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    MenuItemFileInfo_Click(sender, e);
                }
                MenuItemSave_Click(null, null);
                _exitFlag = true;
            }
            else _exitFlag = true;
            this.Close();
        }
        //点击列表进行修改
        private void listBoxShow_SelectedIndexChanged(object sender, EventArgs e)
        {
            //暂停播放
            musicPlayer.Ctlcontrols.pause();
            //点击list时,读取该项的信息,显示修改按钮
            if (listBoxShow.SelectedIndex == -1) return;//防止无项目时出错
            listBoxSave.SelectedIndex = listBoxShow.SelectedIndex;
            mykey = (MyKey)listBoxSave.SelectedItem;
            numericMin.Value = mykey.timeMin;
            numericSec.Value = mykey.timeSec;
            numericMs.Value = mykey.timeMs;
            numericKeyCount.Value = mykey.keyCount;
            buttonDelete.Visible = true;
            buttonSort.Visible = true;
            buttonWriteToList.Visible = false;
            buttonRewrite.Visible = true;
            buttonCancelReWrite.Visible = true;
            checkBoxA.Checked = mykey.A;
            checkBoxD.Checked = mykey.D;
            checkBoxF.Checked = mykey.F;
            checkBoxJ.Checked = mykey.J;
            checkBoxK.Checked = mykey.K;
            checkBoxL.Checked = mykey.L;
            checkBoxS.Checked = mykey.S;
            checkBoxSem.Checked = mykey.Sem;
            checkBoxSpace.Checked = mykey.Space;
        }
        //计算当前时间
        private void timer1_Tick(object sender, EventArgs e)
        {
                //显示当前时间
                _currentTime = musicPlayer.Ctlcontrols.currentPosition;
                _currentTimeString = musicPlayer.Ctlcontrols.currentPositionString;
                if (_currentTimeString == "")
                {
                    _currentTimeString = "00:00.000";
                    labelShowTime.Text = _currentTimeString;
                    //timerInput.Enabled = false;
                }
                else
                {
                    labelShowTime.Text = _currentTimeString;
                    double ms = (int)((_currentTime - (int)_currentTime) * 1000);
                    labelShowTime.Text = labelShowTime.Text + ":" + ms.ToString();
                    if (ms < 100 && ms >= 10) labelShowTime.Text = labelShowTime.Text + "0";
                    else if (ms < 10) labelShowTime.Text = labelShowTime.Text + "00";
                }
                if ((int)musicPlayer.playState == 3)//正在播放
                {
                    //演奏键入模式的时候更新上方的时间
                    if (_playInput)
                    {
                        numericMin.Value = Convert.ToInt32(_currentTimeString.Substring(0, 2));
                        numericSec.Value = Convert.ToInt32(_currentTimeString.Substring(3, 2));
                        numericMs.Value = (int)((_currentTime - (int)_currentTime) * 1000);
                    }
                }
        }
        //转到当前时间播放
        private void buttonTurnToPlay_Click(object sender, EventArgs e)
        {
            //切换到当前时间
            double second = (double)numericMin.Value * 60 + (double)numericSec.Value + (double)numericMs.Value * 0.001;
            musicPlayer.Ctlcontrols.currentPosition = second;
            //播放
            if (buttonTurnToPlay.Text == "暂停到当前时间")
                musicPlayer.Ctlcontrols.pause();
            else
                musicPlayer.Ctlcontrols.play();
        }
        //卸载钩子
        private void FormEditor_FormClosed(object sender, FormClosedEventArgs e)
        {
            //卸载钩子
            try
            {
                HookStop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("钩子卸载失败！提示信息：" + ex.Message);
            }
        }
        //创建ZIP文件
        private static void CreateZipFile(string filesPath, string zipFilePath)
        {
            if (!Directory.Exists(filesPath))
            {
                Console.WriteLine("Cannot find directory '{0}'", filesPath);
                return;
            }
            try
            {
                string[] filenames = Directory.GetFiles(filesPath);
                using (ZipOutputStream s = new ZipOutputStream(File.Create(zipFilePath)))
                {
                    s.SetLevel(9); // 压缩级别 0-9
                    s.Password = "Rhythm!"; //Zip压缩文件密码
                    byte[] buffer = new byte[4096]; //缓冲区大小
                    foreach (string file in filenames)
                    {
                        ZipEntry entry = new ZipEntry(Path.GetFileName(file));
                        entry.DateTime = DateTime.Now;
                        s.PutNextEntry(entry);
                        using (FileStream fs = File.OpenRead(file))
                        {
                            int sourceBytes;
                            do
                            {
                                sourceBytes = fs.Read(buffer, 0, buffer.Length);
                                s.Write(buffer, 0, sourceBytes);
                            } while (sourceBytes > 0);
                        }
                    }
                    s.Finish();
                    s.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception during processing {0}", ex);
            }
        }
        //解压ZIP文件
        private static void UnZipFile(string zipFilePath)
        {
            string openPath = System.IO.Path.GetTempPath() + "ROTMP\\";
            if (System.IO.Directory.Exists(System.IO.Path.GetTempPath() + "ROTMP"))
                System.IO.Directory.Delete(System.IO.Path.GetTempPath() + "ROTMP", true);
            System.IO.Directory.CreateDirectory(openPath);
            if (!File.Exists(zipFilePath))
            {
                Console.WriteLine("Cannot find file '{0}'", zipFilePath);
                return;
            }
            using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                s.Password = "Rhythm!"; //Zip压缩文件密码
                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    //MessageBox.Show(theEntry.Name);
                    string directoryName = openPath;
                    string fileName = Path.GetFileName(theEntry.Name);
                    // create directory
                    if (directoryName.Length > 0)
                    {
                        Directory.CreateDirectory(directoryName);
                    }
                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(directoryName+theEntry.Name))
                        {

                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
        }
        //新建
        private void MenuItemNew_Click(object sender, EventArgs e)
        {
            _openSong = "";
            _openmp3 = "";
            musicPlayer.URL = "";
            _currentTime = 0;
            _currentTimeString = "";
            _difficulty = 0;
            _fileName = "";
            _maker = "";
            _description = "";
            _useKeyBoard = false;
            _playInput = false;
            listBoxSave.Items.Clear();
            listBoxShow.Items.Clear();
            buttonCancelReWrite.PerformClick();
            //清理勾选,计数清零
            checkBoxA.Checked = false;
            checkBoxS.Checked = false;
            checkBoxD.Checked = false;
            checkBoxF.Checked = false;
            checkBoxSpace.Checked = false;
            checkBoxJ.Checked = false;
            checkBoxK.Checked = false;
            checkBoxL.Checked = false;
            checkBoxSem.Checked = false;
            numericKeyCount.Value = 0;
            numericMin.Value = 0;
            numericSec.Value = 0;
            numericMs.Value = 0;
        }
        //是否用快捷键输入（影响组合键）
        private void MenuItemKeyBoardInput_Click(object sender, EventArgs e)
        {
            _useKeyBoard = !_useKeyBoard;
            MenuItemKeyBoardInput.Checked = !MenuItemKeyBoardInput.Checked;
        }
        //演奏键入模式开关
        private void MenuItemPlayInput_Click(object sender, EventArgs e)
        {
            if (!_playInput)
            {
                //打开键盘键入
                MenuItemKeyBoardInput.Checked = true;
                _useKeyBoard = true;
                _playInput = true;
                MenuItemPlayInput.Checked = true;
                _playPause = false;
                labelTimeRe.Visible = true;
                labelTimeTip.Visible = true;
                labelTimeRe.Text = "3";
                buttonTurnToPlay.Text = "暂停到当前时间";
            }
            else
            {
                timerInput.Enabled = false;
                MenuItemKeyBoardInput.Checked = false;
                _useKeyBoard = false;
                _playInput = false;
                MenuItemPlayInput.Checked = false;
                labelTimeTip.Visible = false;
                labelTimeRe.Visible = false;
                buttonTurnToPlay.Text = "播放到当前时间";
            }
        }
        //演奏键入的解决方法：用Timer，设置100ms的容错
        //可能问题：Tick的时候按下按键，可能会出问题
        private void timerInput_Tick(object sender, EventArgs e)
        {
            //if (musicPlayer.playState == WMPLib.WMPPlayState.wmppsPaused) return;
            //当选择为空时，不进行任何操作
            if (numericKeyCount.Value == 0)return;
            //获取时间信息
            mykey.timeMin = (int)numericMin.Value;
            mykey.timeSec = (int)numericSec.Value;
            mykey.timeMs = (int)numericMs.Value;
            mykey.keyCount = (int)numericKeyCount.Value;
            //转换成显示格式
            if (numericMin.Value <= 9)
                mykey.myKeyShow = "0";
            else mykey.myKeyShow = "";
            mykey.myKeyShow += numericMin.Value.ToString();
            mykey.myKeyShow += ":";
            if (numericSec.Value <= 9)
                mykey.myKeyShow += "0";
            mykey.myKeyShow += numericSec.Value.ToString();
            mykey.myKeyShow += ".";
            if (numericMs.Value == 0)
                mykey.myKeyShow += "000";
            else if (numericMs.Value <= 9)
                mykey.myKeyShow += "00";
            else if (numericMs.Value <= 99)
                mykey.myKeyShow += "0";
            mykey.myKeyShow += numericMs.Value.ToString();
            mykey.myKeyShow += " " + mykey.keyCount;
            //获取音符
            if (checkBoxA.Checked) { mykey.myKeyShow += " A"; mykey.A = true; }
            else mykey.A = false;
            if (checkBoxS.Checked) { mykey.myKeyShow += " S"; mykey.S = true; }
            else mykey.S = false;
            if (checkBoxD.Checked) { mykey.myKeyShow += " D"; mykey.D = true; }
            else mykey.D = false;
            if (checkBoxF.Checked) { mykey.myKeyShow += " F"; mykey.F = true; }
            else mykey.F = false;
            if (checkBoxSpace.Checked) { mykey.myKeyShow += " Space"; mykey.Space = true; }
            else mykey.Space = false;
            if (checkBoxJ.Checked) { mykey.myKeyShow += " J"; mykey.J = true; }
            else mykey.J = false;
            if (checkBoxK.Checked) { mykey.myKeyShow += " K"; mykey.K = true; }
            else mykey.K = false;
            if (checkBoxL.Checked) { mykey.myKeyShow += " L"; mykey.L = true; }
            else mykey.L = false;
            if (checkBoxSem.Checked) { mykey.myKeyShow += " ;"; mykey.Sem = true; }
            else mykey.Sem = false;
            //写入到两个List中
            listBoxSave.Items.Add(mykey);
            listBoxShow.Items.Add(mykey.myKeyShow);
            listBoxShow.SelectedIndex = listBoxShow.Items.Count - 1;
            //清理勾选,计数清零
            checkBoxA.Checked = false;
            checkBoxS.Checked = false;
            checkBoxD.Checked = false;
            checkBoxF.Checked = false;
            checkBoxSpace.Checked = false;
            checkBoxJ.Checked = false;
            checkBoxK.Checked = false;
            checkBoxL.Checked = false;
            checkBoxSem.Checked = false;
            numericKeyCount.Value = 0;
        }
        //演奏输入时暂停/继续
        private void MenuItemPausePlay_Click(object sender, EventArgs e)
        {
            if (_playInput)
            {
                if (_playPause)
                    musicPlayer.Ctlcontrols.play();
                else
                    musicPlayer.Ctlcontrols.pause();
            }
        }
        //倒计时
        private void timerRe_Tick(object sender, EventArgs e)
        {
            if (labelTimeRe.Text == "3")
            {
                labelTimeRe.Text = "2";
            }
            else if (labelTimeRe.Text == "2")
                labelTimeRe.Text = "1";
            else if (labelTimeRe.Text == "1")
                labelTimeRe.Text = "0";
            else if (labelTimeRe.Text == "0")
            {
                labelTimeRe.Visible = false;
                labelTimeTip.Visible = false;
                musicPlayer.Ctlcontrols.play();
                timerInput.Enabled = true;
                timerRe.Enabled = false;
                return;
            }
            //musicPlayer.Ctlcontrols.pause();
            //musicPlayer.Ctlcontrols.currentPosition = 0;
        }
        //点击开始倒计时
        private void labelTimeRe_Click(object sender, EventArgs e)
        {
            if (_openmp3 == "")
            {
                MessageBox.Show(this, "未打开mp3文件，请重新打开！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                MenuItemOpen_Click(sender, e);
                return;
            }
            timerRe.Enabled = true;
        }
        //跳转播放得分菜单项
        private void MenuItemTurnToPlay_Click(object sender, EventArgs e)
        {
            //切换到当前时间
            double second = (double)numericMin.Value * 60 + (double)numericSec.Value + (double)numericMs.Value * 0.001;
            musicPlayer.Ctlcontrols.currentPosition = second;
            //播放
            musicPlayer.Ctlcontrols.play();
        }
        //关闭时提示保存
        private void FormEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_exitFlag == false)
            {
                _exitFlag = true;
                e.Cancel = true;
                MenuItemExit_Click(sender, e);
            }
        }

        private void buttonDelete_Click(object sender, EventArgs e)
        {
            if (listBoxSave.Items.Count <= 0) return;
            if (listBoxSave.SelectedIndex < 0) return;
            listBoxSave.Items.RemoveAt(listBoxSave.SelectedIndex);
            listBoxShow.Items.RemoveAt(listBoxShow.SelectedIndex);
        }

        private static int SortCompare(MyKey p1, MyKey p2)
        {
            try
            {
                if (p1.timeMin == p2.timeMin)
                {
                    if (p1.timeSec == p2.timeSec)
                    {
                        if (p1.timeMs < p2.timeMs)
                            return -1;
                        else
                            return 1;
                    }
                    else
                    {
                        if (p1.timeSec < p2.timeSec)
                            return -1;
                        else
                            return 1;
                    }
                }
                else
                {
                    if (p1.timeMin < p2.timeMin)
                        return -1;
                    else
                        return 1;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("出现错误，ErrorCode：0X2004，ErrorInfo：" + exc);
                return 0;
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            List<MyKey> listkey=new List<MyKey>();
            listkey.Clear();
            for(int i=0;i< listBoxSave.Items.Count;i++)
            {
                MyKey keys = new MyKey();
                listBoxSave.SelectedIndex = i;
                keys = (MyKey)listBoxSave.SelectedItem;
                listkey.Add(keys);
            }
            listkey.Sort(SortCompare);
            listBoxSave.Items.Clear();
            for (int i = 0; i < listkey.Count; i++)
                listBoxSave.Items.Add(listkey[i]);
            listBoxShow.Items.Clear();
            for(int i=0;i<listBoxSave.Items.Count;i++)
            {
                MyKey keys = new MyKey();
                listBoxSave.SelectedIndex = i;
                keys = (MyKey)listBoxSave.SelectedItem;
                listBoxShow.Items.Add(keys.myKeyShow);
            }
            listkey.Clear();
        }
    }
}
