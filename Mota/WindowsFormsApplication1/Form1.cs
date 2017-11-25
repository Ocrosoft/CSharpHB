using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        enum chaoxiang { shang, xia, zuo, you }

        private chaoxiang _chaoxiang = chaoxiang.shang;
        private int suoxujinbi = 20;//商店购买商品所需金币
        private Point weizhi = new Point(6, 11);//6,11
        private int shengming = 498;//498
        private int gongji = 1000;//10
        private int fangyu = 1000;//10
        private int jingbi = 10;//0
        private int huangkey = 0;//0
        private int lankey = 0;//0
        private int hongkey = 1;//0
        private int cengshu = 1;//当前层
        private bool change = false;
        private Map[] map = new Map[20];

        private bool zhandou = false;
        private string[] zhizhehua = new string[100];//智者的信息
        private string[] shangrenhua = new string[100];//商人的购买信息
        private bool[] shangrenyimai = new bool[100];//标记是否买了
        private string[] shangrenhua2 = new string[100];//商人购买后提供的信息。
        private shangpin[] shangpins = new shangpin[100];//商人商品信息
        private string[] names = new string[100];//物品,怪物名
        private string[] description = new string[100];//物品,怪物描述
        private int[] eventsCnt = new int[100];//第i层有eventsCnt[i]个事件
        private Event[,] events = new Event[100, 100];//每层最多100个事件
        struct Event
        {
            public bool actived;//是否触发过
            public int messagef;//是否显示对话框
            public string message;//对话框内容
            public int type;//事件类型
            public int louceng;//触发楼层
            public int pointCnt;//触发点统计
            public List<Point> li;//触发点
            public List<int> ID;//触发点所需ID
            public int pointCnt2;//目标点统计
            public List<Point> li2;//目标点
            public List<int> ID2;//目标点所需ID
            public int mubiaolouceng;//目标楼层，用于跳转，注意飞行道具不使用事件，不过这里没有飞行道具
        }
        struct shangpin
        {
            public int cnt1;//商品种类
            public int cnt2;//交换物种类
            public List<int> ID1;//商品ID
            public List<int> ID2;//交换物ID
            public List<int> shuliang1;//商品数量
            public List<int> shuliang2;//交换物数量
        }
        struct Map
        {
            public int[] mp;
        }
        struct monsterinfo
        {
            public monsterinfo(int xueliang1, int gongji1, int fangyu1, int jingqian1)
            {
                xueliang = xueliang1;
                gongji = gongji1;
                fangyu = fangyu1;
                jingqian = jingqian1;
            }
            public int xueliang;
            public int gongji;
            public int fangyu;
            public int jingqian;
        }
        monsterinfo[] _monsinfo = new monsterinfo[100];
        Bitmap dimian = new Bitmap(@"Image\dimian.png");
        Bitmap weiyunshang = new Bitmap(@"Image\weiyunshang.png");
        Bitmap weiyunxia = new Bitmap(@"Image\weiyunxia.png");
        Bitmap weiyunzuo = new Bitmap(@"Image\weiyunzuo.png");
        Bitmap weiyunyou = new Bitmap(@"Image\weiyunyou.png");
        Bitmap dajia = new Bitmap(@"Image\dajia.png");
        Bitmap zhuantou = new Bitmap(@"Image\zhuan.png");
        Bitmap shanglouti = new Bitmap(@"Image\shanglu.png");
        Bitmap lvshi = new Bitmap(@"Image\lvshi.png");
        Bitmap hongshi = new Bitmap(@"Image\hongshi.png");
        Bitmap hongyao = new Bitmap(@"Image\hongyao.png");
        Bitmap lanyao = new Bitmap(@"Image\lanyao.png");
        Bitmap hongbaoshi = new Bitmap(@"Image\hongbaoshi.png");
        Bitmap lanbaoshi = new Bitmap(@"Image\lanbaoshi.png");
        Bitmap huangyaoshi = new Bitmap(@"Image\huangyaoshi.png");
        Bitmap lanyaoshi = new Bitmap(@"Image\lanyaoshi.png");
        Bitmap hongyaoshi = new Bitmap(@"Image\hongyaoshi.png");
        Bitmap bianfu = new Bitmap(@"Image\bianfu.png");
        Bitmap fashi = new Bitmap(@"Image\fashi.png");
        Bitmap kulou = new Bitmap(@"Image\kulou.png");
        Bitmap kuloubin = new Bitmap(@"Image\kuloubin.png");
        Bitmap huangmen = new Bitmap(@"Image\huangmen.png");
        Bitmap lanmen = new Bitmap(@"Image\lanmen.png");
        Bitmap gong = new Bitmap(@"Image\gongji.png");
        Bitmap fang = new Bitmap(@"Image\fangyu.png");
        Bitmap xinximianban = new Bitmap(@"Image\xinximianban.png");
        Bitmap money = new Bitmap(@"Image\money.png");
        Bitmap xueliang = new Bitmap(@"Image\shengming.png");
        Bitmap hongsemen = new Bitmap(@"Image\hongmen.png");
        Bitmap misemen = new Bitmap(@"Image\misemen.png");
        Bitmap zhizhe = new Bitmap(@"Image\zhizhe.png");
        Bitmap shangren = new Bitmap(@"Image\shangren.png");
        Bitmap shangdian = new Bitmap(@"Image\shangdian.png");
        Bitmap huangshouwei = new Bitmap(@"Image\huangshouwei.png");
        Bitmap lanshouwei = new Bitmap(@"Image\lanshouwei.png");
        Bitmap jianyumen = new Bitmap(@"Image\jianyumen.png");
        Bitmap xialouti = new Bitmap(@"Image\xialu.png");
        Bitmap xiaotou = new Bitmap(@"Image\xiaotou.png");
        Bitmap yincangmen = new Bitmap(@"Image\yincangzhuan.png");
        Bitmap tiejian = new Bitmap(@"Image\tiejian.png");
        Bitmap tiedun = new Bitmap(@"Image\tiedun.png");
        Bitmap kulouduizhang = new Bitmap(@"Image\kulouduizhang.png");
        Bitmap mowang = new Bitmap(@"Image\mowang.png");


        public Form1()
        {
            InitializeComponent();
            label1.Parent = pictureBox1;
            label3.Parent = pictureBox1;
            label1.Text = "";
            label3.Text = "";
            StreamReader sr;
            string line;
            int linecnt = 0;
            for (int i = 1; i <= 10; i++)//读取X张地图
            {
                sr = new StreamReader(@"map\map" + i.ToString() + ".txt", Encoding.Default);
                map[i].mp = new int[200];
                linecnt = 0;
                while ((line = sr.ReadLine()) != null)
                {
                    int j = 0;
                    int len = line.Split(' ').Length;
                    while (j < len)
                        map[i].mp[linecnt * 13 + j] = int.Parse(line.Split(' ')[j++]);
                    linecnt++;
                }
            }
            //读取智者的话
            sr = new StreamReader(@"map\zhizhe.txt", Encoding.Default);
            while ((line = sr.ReadLine()) != null)
            {
                zhizhehua[int.Parse(line)] = sr.ReadLine();
            }
            //读取商人信息
            sr = new StreamReader(@"map\shangren.txt", Encoding.Default);
            while ((line = sr.ReadLine()) != null)
            {
                shangrenhua[int.Parse(line)] = sr.ReadLine();
                shangrenhua2[int.Parse(line)] = sr.ReadLine();
            }
            //读取名称、描述
            sr = new StreamReader(@"map\description.txt", Encoding.Default);
            while ((line = sr.ReadLine()) != null)
            {
                names[int.Parse(line)] = sr.ReadLine();
                description[int.Parse(line)] = sr.ReadLine();
            }
            //读取商品信息
            sr = new StreamReader(@"map\shangpin.txt", Encoding.Default);
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] == '/') continue;
                shangpin sp = new shangpin();
                sp.ID1 = new List<int>();
                sp.ID2 = new List<int>();
                sp.shuliang1 = new List<int>();
                sp.shuliang2 = new List<int>();
                int srcs = int.Parse(line);
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                sp.cnt1 = int.Parse(line);
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                for (int i = 0; i < sp.cnt1; i++)
                {
                    sp.ID1.Add(int.Parse(line.Split(' ')[i]));
                }
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                for (int i = 0; i < sp.cnt1; i++)
                {
                    sp.shuliang1.Add(int.Parse(line.Split(' ')[i]));
                }
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                sp.cnt2 = int.Parse(line);
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                for (int i = 0; i < sp.cnt2; i++)
                {
                    sp.ID2.Add(int.Parse(line.Split(' ')[i]));
                }
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                for (int i = 0; i < sp.cnt2; i++)
                {
                    sp.shuliang2.Add(int.Parse(line.Split(' ')[i]));
                }
                shangpins[srcs] = sp;
            }
            //读取事件
            sr = new StreamReader(@"map\event.txt", Encoding.Default);
            while ((line = sr.ReadLine()) != null)
            {
                if (line[0] == '/') continue;
                Event ev = new Event();
                ev.ID = new List<int>();
                ev.ID2 = new List<int>();
                ev.li = new List<Point>();
                ev.li2 = new List<Point>();
                ev.actived = false;
                ev.type = int.Parse(line.Split(' ')[0]);
                ev.louceng = int.Parse(line.Split(' ')[1]);
                do
                {
                    line = sr.ReadLine();
                } while (line[0] == '/');
                Point p;
                switch (ev.type)
                {
                    case 1:
                        ev.pointCnt = int.Parse(line.Split(' ')[0]);
                        for (int i = 1; i <= ev.pointCnt; i++)
                        {
                            p = new Point(int.Parse(line.Split(' ')[i * 2 - 1]), int.Parse(line.Split(' ')[i * 2]));
                            ev.li.Add(p);
                        }
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        for (int i = 0; i < ev.pointCnt; i++)
                        {
                            ev.ID.Add(int.Parse(line.Split(' ')[i]));
                        }
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        ev.pointCnt2 = int.Parse(line.Split(' ')[0]);
                        for (int i = 1; i <= ev.pointCnt2; i++)
                        {
                            p = new Point(int.Parse(line.Split(' ')[i * 2 - 1]), int.Parse(line.Split(' ')[i * 2]));
                            ev.li2.Add(p);
                        }
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        for (int i = 0; i < ev.pointCnt2; i++)
                        {
                            ev.ID2.Add(int.Parse(line.Split(' ')[i]));
                        }
                        events[ev.louceng, eventsCnt[ev.louceng]] = ev;
                        eventsCnt[ev.louceng]++;
                        break;
                    case 2:
                        ev.pointCnt = 1;
                        p = new Point(int.Parse(line.Split(' ')[0]), int.Parse(line.Split(' ')[1]));
                        ev.li.Add(p);
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        if (int.Parse(line) == 1 || int.Parse(line) == 2)
                        {
                            ev.messagef = int.Parse(line);
                            do
                            {
                                line = sr.ReadLine();
                            } while (line[0] == '/');
                            ev.message = line;
                        }
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        ev.pointCnt2 = int.Parse(line.Split(' ')[0]);
                        for (int i = 1; i <= ev.pointCnt2; i++)
                        {
                            p = new Point(int.Parse(line.Split(' ')[i * 2 - 1]), int.Parse(line.Split(' ')[i * 2]));
                            ev.li2.Add(p);
                        }
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        for (int i = 0; i < ev.pointCnt2; i++)
                        {
                            ev.ID2.Add(int.Parse(line.Split(' ')[i]));
                        }
                        events[ev.louceng, eventsCnt[ev.louceng]] = ev;
                        eventsCnt[ev.louceng]++;
                        break;
                    case 3:
                        ev.pointCnt = 1;
                        p = new Point(int.Parse(line.Split(' ')[0]), int.Parse(line.Split(' ')[1]));
                        ev.li.Add(p);
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        if (int.Parse(line) == 1 || int.Parse(line)==2)
                        {
                            ev.messagef = int.Parse(line);//显示对话框
                            do
                            {
                                line = sr.ReadLine();
                            } while (line[0] == '/');
                            ev.message = line;//读取对话框内容
                        }
                        do
                        {
                            line = sr.ReadLine();
                        } while (line[0] == '/');
                        ev.pointCnt2 = 1;
                        ev.mubiaolouceng = int.Parse(line.Split(' ')[0]);
                        p = new Point(int.Parse(line.Split(' ')[1]), int.Parse(line.Split(' ')[2]));
                        ev.li2.Add(p);//需要移动到的点
                        events[ev.louceng, eventsCnt[ev.louceng]] = ev;
                        eventsCnt[ev.louceng]++;
                        break;
                }
            }
            _monsinfo[3] = new monsterinfo(35, 18, 1, 1);
            _monsinfo[4] = new monsterinfo(45, 20, 2, 2);
            _monsinfo[12] = new monsterinfo(35, 38, 3, 3);
            _monsinfo[13] = new monsterinfo(60, 32, 8, 5);
            _monsinfo[14] = new monsterinfo(50, 42, 6, 6);
            _monsinfo[15] = new monsterinfo(55, 52, 12, 8);
            _monsinfo[24] = new monsterinfo(100, 180, 110, 50);
            _monsinfo[32] = new monsterinfo(100, 65, 15, 30);
        }

        private void Drawweiyun(Graphics g)
        {
            if (zhandou == true)
            {
                g.DrawImage(dajia, weizhi.X * 26, weizhi.Y * 26);
                return;
            }
            if (_chaoxiang == chaoxiang.shang)
                g.DrawImage(weiyunshang, weizhi.X * 26, weizhi.Y * 26);
            else if (_chaoxiang == chaoxiang.xia)
                g.DrawImage(weiyunxia, weizhi.X * 26, weizhi.Y * 26);
            else if (_chaoxiang == chaoxiang.zuo)
                g.DrawImage(weiyunzuo, new Point(weizhi.X * 26 + 5, weizhi.Y * 26));
            else
                g.DrawImage(weiyunyou, new Point(weizhi.X * 26 + 6, weizhi.Y * 26));
        }
        private void monsInfo(Graphics g, Bitmap bmp, int monsID)
        {
            label1.Text = "";
            int d;
            g.DrawImage(bmp, 16 * 26 + 4 + (52 - bmp.Width * 2) / 2, 6 * 26 + 8 + (52 - bmp.Height * 2) / 2, bmp.Width * 2, bmp.Height * 2);
            g.DrawString("攻击：" + _monsinfo[monsID].gongji.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 14 * 26, 9 * 26 - 2);
            g.DrawString("防御：" + _monsinfo[monsID].fangyu.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 14 * 26, 10 * 26 - 2);
            g.DrawString("血量：" + _monsinfo[monsID].xueliang.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 14 * 26, 11 * 26 - 2);
            g.DrawString("金币：" + _monsinfo[monsID].jingqian.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 14 * 26, 12 * 26 - 2);
            g.DrawString("损失血量：", new Font("微软雅黑", 10), new SolidBrush(Color.Black), 18 * 26, 9 * 26 - 2);
            d = damage(monsID);
            if (d < 0) g.DrawString("无法攻击", new Font("微软雅黑", 12), new SolidBrush(Color.Black), 18 * 26 - 4, 10 * 26 + 6);
            else if (d < 10) g.DrawString("  " + d.ToString(), new Font("微软雅黑", 20), new SolidBrush(Color.Black), 18 * 26, 10 * 26 + 6);
            else if (d < 100) g.DrawString(" " + d.ToString(), new Font("微软雅黑", 20), new SolidBrush(Color.Black), 18 * 26, 10 * 26 + 6);
            else g.DrawString(d.ToString(), new Font("微软雅黑", 20), new SolidBrush(Color.Black), 18 * 26, 10 * 26 + 6);
        }
        private void NPCItemsInfo(Graphics g, Bitmap bmp,int ID)
        {
            g.DrawImage(bmp, 16 * 26 + 4 + (52 - bmp.Width * 2) / 2, 6 * 26 + 8 + (52 - bmp.Height * 2) / 2, bmp.Width * 2, bmp.Height * 2);
            label1.Text = description[ID];
            label3.Text = names[ID];
        }
        private void MyDraw(Graphics g, Bitmap bmp, Point p)
        {
            g.DrawImage(bmp, p.X + (26 - bmp.Width) / 2, p.Y + (26 - bmp.Height) / 2, bmp.Width, bmp.Height);
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            for (int i = 0; i < 13; i++)
                for (int j = 0; j < 13; j++)
                    g.DrawImage(dimian, i * 26, j * 26);
            for (int i = 13; i < 21; i++)
                for (int j = 0; j < 13; j++)
                    g.DrawImage(xinximianban, i * 26, j * 26);
            //怪物信息
            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(13 * 26, 6 * 26 - 4, 8 * 26, 8 * 26));
            g.DrawImage(dimian, 16 * 26 + 4, 6 * 26 + 8, 52, 52);
            Point mon = weizhi;
            if (_chaoxiang == chaoxiang.shang) mon = new Point(mon.X, mon.Y - 1);
            else if (_chaoxiang == chaoxiang.xia) mon = new Point(mon.X, mon.Y + 1);
            else if (_chaoxiang == chaoxiang.zuo) mon = new Point(mon.X - 1, mon.Y);
            else if (_chaoxiang == chaoxiang.you) mon = new Point(mon.X + 1, mon.Y);

            switch (map[cengshu].mp[mon.Y * 13 + mon.X])
            {
                case 2:
                    NPCItemsInfo(g, shanglouti,2);
                    break;
                case 3:
                    NPCItemsInfo(g, lvshi, 3);
                    break;
                case 4:
                    NPCItemsInfo(g, hongshi, 4);
                    //monsInfo(g, hongshi, 4);
                    break;
                case 5:
                    NPCItemsInfo(g, hongyao, 5);
                    break;
                case 6:
                    NPCItemsInfo(g, lanyao, 6);
                    break;
                case 7:
                    NPCItemsInfo(g, hongbaoshi, 7);
                    break;
                case 8:
                    NPCItemsInfo(g, lanbaoshi, 8);
                    break;
                case 9:
                    NPCItemsInfo(g, huangyaoshi, 9);
                    break;
                case 10:
                    NPCItemsInfo(g, lanyaoshi, 10);
                    break;
                case 11:
                    NPCItemsInfo(g, hongyaoshi, 11);
                    break;
                case 12:
                    NPCItemsInfo(g, bianfu, 12);
                    //monsInfo(g, bianfu, 12);
                    break;
                case 13:
                    NPCItemsInfo(g, fashi, 13);
                    //monsInfo(g, fashi, 13);
                    break;
                case 14:
                    NPCItemsInfo(g, kulou, 14);
                    //monsInfo(g, kulou, 14);
                    break;
                case 15:
                    NPCItemsInfo(g, kuloubin, 15);
                    //monsInfo(g, kuloubin, 15);
                    break;
                case 16:
                    NPCItemsInfo(g, huangmen, 16);
                    break;
                case 17:
                    NPCItemsInfo(g, lanmen, 17);
                    break;
                case 18:
                    NPCItemsInfo(g, hongsemen, 18);
                    break;
                case 19:
                    NPCItemsInfo(g, misemen, 19);
                    break;
                case 20:
                    NPCItemsInfo(g, zhizhe, 20);
                    break;
                case 21:
                    NPCItemsInfo(g, shangren, 21);
                    break;
                case 22:
                    NPCItemsInfo(g, shangdian, 22);
                    break;
                case 23:
                    NPCItemsInfo(g, huangshouwei, 23);
                    //monsInfo(g, huangshouwei, 23);
                    break;
                case 24:
                    NPCItemsInfo(g, lanshouwei, 24);
                    //monsInfo(g, lanshouwei, 24);
                    break;
                case 25:
                    NPCItemsInfo(g, jianyumen, 25);
                    break;
                case 26:
                    NPCItemsInfo(g, xialouti, 26);
                    break;
                case 27:
                    NPCItemsInfo(g, xiaotou, 27);
                    break;
                case 28:
                    NPCItemsInfo(g, yincangmen, 28);
                    break;
                case 29:
                    NPCItemsInfo(g, shangdian, 29);
                    break;
                case 30:
                    NPCItemsInfo(g, tiejian, 30);
                    break;
                case 31:
                    NPCItemsInfo(g, tiedun, 31);
                    break;
                case 32:
                    NPCItemsInfo(g, kulouduizhang, 32);
                    //monsInfo(g, kulouduizhang, 32);
                    break;
                case 33:
                    NPCItemsInfo(g, mowang, 33);
                    //monsInfo(g, mowang, 33);
                    break;
                default:
                    label1.Text = "";
                    label3.Text = "";
                    break;
            }
            g.DrawString("第" + cengshu.ToString() + "层", new Font("微软雅黑", 20), new SolidBrush(Color.Black), 15 * 26 + 14, 4 * 26 + 4);
            g.FillRectangle(new SolidBrush(Color.Gray), new Rectangle(13 * 26, 0 - 4, 8 * 26, 4 * 26));
            g.DrawString("人物信息", new Font("微软雅黑", 10), new SolidBrush(Color.Black), 16 * 26 - 4, 2);
            g.DrawImage(gong, 14 * 26, 1 * 26);
            g.DrawImage(fang, 18 * 26, 1 * 26 + 1);
            g.DrawString(gongji.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 15 * 26, 1 * 26 - 2);
            g.DrawString(fangyu.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 19 * 26, 1 * 26 - 2);
            g.DrawImage(huangyaoshi, 14 * 26, 2 * 26, 15, 15);
            g.DrawImage(lanyaoshi, 16 * 26, 2 * 26, 15, 15);
            g.DrawImage(hongyaoshi, 18 * 26, 2 * 26, 15, 15);
            g.DrawString(huangkey.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 15 * 26, 2 * 26 - 2);
            g.DrawString(lankey.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 17 * 26, 2 * 26 - 2);
            g.DrawString(hongkey.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 19 * 26, 2 * 26 - 2);
            g.DrawImage(xueliang, 14 * 26, 3 * 26, 15, 15);
            g.DrawImage(money, 18 * 26, 3 * 26, 15, 15);
            g.DrawString(Math.Abs(Math.Abs(shengming)).ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 15 * 26, 3 * 26 - 2);
            g.DrawString(jingbi.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.Black), 19 * 26, 3 * 26 - 2);
            for (int i = 0; i < 13; i++)
                for (int j = 0; j < 13; j++)
                {
                    int code = map[cengshu].mp[j * 13 + i];
                    Point p = new Point(i * 26, j * 26);
                    switch (code)
                    {
                        case 0:
                            MyDraw(g, zhuantou, p);
                            break;
                        case 2:
                            MyDraw(g, shanglouti, p);
                            break;
                        case 3:
                            MyDraw(g, lvshi, p);
                            break;
                        case 4:
                            MyDraw(g, hongshi, p);
                            break;
                        case 5:
                            MyDraw(g, hongyao, p);
                            break;
                        case 6:
                            MyDraw(g, lanyao, p);
                            break;
                        case 7:
                            MyDraw(g, hongbaoshi, p);
                            break;
                        case 8:
                            MyDraw(g, lanbaoshi, p);
                            break;
                        case 9:
                            MyDraw(g, huangyaoshi, p);
                            break;
                        case 10:
                            MyDraw(g, lanyaoshi, p);
                            break;
                        case 11:
                            MyDraw(g, hongyaoshi, p);
                            break;
                        case 12:
                            MyDraw(g, bianfu, p);
                            break;
                        case 13:
                            MyDraw(g, fashi, p);
                            break;
                        case 14:
                            MyDraw(g, kulou, p);
                            break;
                        case 15:
                            MyDraw(g, kuloubin, p);
                            break;
                        case 16:
                            MyDraw(g, huangmen, p);
                            break;
                        case 17:
                            MyDraw(g, lanmen, p);
                            break;
                        case 18:
                            MyDraw(g, hongsemen, p);
                            break;
                        case 19:
                            MyDraw(g, misemen, p);
                            break;
                        case 20:
                            MyDraw(g, zhizhe, p);
                            break;
                        case 21:
                            MyDraw(g, shangren, p);
                            break;
                        case 22:
                            g.DrawImage(shangdian, p);
                            //MyDraw(g, shangdian, p);
                            break;
                        case 23:
                            MyDraw(g, huangshouwei, p);
                            break;
                        case 24:
                            MyDraw(g, lanshouwei, p);
                            break;
                        case 25:
                            MyDraw(g, jianyumen, p);
                            break;
                        case 26:
                            MyDraw(g, xialouti, p);
                            break;
                        case 27:
                            MyDraw(g, xiaotou, p);
                            break;
                        case 28:
                            MyDraw(g, yincangmen, p);
                            break;
                        case 29:
                            break;
                        case 30:
                            MyDraw(g, tiejian, p);
                            break;
                        case 31:
                            MyDraw(g, tiedun, p);
                            break;
                        case 32:
                            MyDraw(g, kulouduizhang, p);
                            break;
                        case 33:
                            MyDraw(g, mowang, p);
                            break;
                    }
                    if (code == 0)
                        g.DrawImage(zhuantou, i * 26, j * 26);
                }
            Drawweiyun(g);
        }
        int damage(int monsID)
        {
            //秒杀
            if (gongji >= _monsinfo[monsID].xueliang + _monsinfo[monsID].fangyu)
                return 0;
            //怪物防御大于攻击，如果都无法对对方造成伤害，则不可攻击
            if (gongji <= _monsinfo[monsID].fangyu)
                return -1;
            //防御大于等于怪物攻击
            if (fangyu >= _monsinfo[monsID].gongji)
                return 0;
            //被秒杀，自己先攻击，所以都能秒杀对方的时候，怪物死
            if (_monsinfo[monsID].gongji >= Math.Abs(Math.Abs(shengming)) + fangyu)
                return -1;
            return (_monsinfo[monsID].xueliang / (gongji - _monsinfo[monsID].fangyu)) * (_monsinfo[monsID].gongji - fangyu);
        }
        private bool go(string dir)
        {
            int i = weizhi.X, j = weizhi.Y;
            if (dir == "shang") j -= 1;
            else if (dir == "xia") j += 1;
            else if (dir == "zuo") i -= 1;
            else if (dir == "you") i += 1;
            //Point p = new Point(i, j);
            switch (map[cengshu].mp[j * 13 + i])
            {
                case 0:
                    return false;
                case 2:
                    change = true;
                    cengshu++;
                    if (map[cengshu].mp[(j + 1) * 13 + i] == 1)
                    {
                        weizhi = new Point(i, j + 1);
                        _chaoxiang = chaoxiang.xia;
                    }
                    else if (map[cengshu].mp[j * 13 + i + 1] == 1)
                    {
                        weizhi = new Point(i + 1, j);
                        _chaoxiang = chaoxiang.you;
                    }
                    else if (map[cengshu].mp[j * 13 + i - 1] == 1)
                    {
                        weizhi = new Point(i - 1, j);
                        _chaoxiang = chaoxiang.zuo;
                    }
                    else if (map[cengshu].mp[(j - 1) * 13 + i] == 1)
                    {
                        weizhi = new Point(i, j - 1);
                        _chaoxiang = chaoxiang.shang;
                    }
                    pictureBox1.Invalidate();
                    //weizhi = new Point(weizhi.X, weizhi.Y);
                    return false;
                case 3:
                    if (gongji <= _monsinfo[3].fangyu)
                        return false;
                    if (shengming <= damage(3))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(3);
                        jingbi += _monsinfo[3].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 4:
                    if (gongji <= _monsinfo[4].fangyu)
                        return false;
                    if (Math.Abs(Math.Abs(shengming)) <= damage(4))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(4);
                        jingbi += _monsinfo[4].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 5:
                    shengming += 50;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 6:
                    shengming += 200;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 7:
                    gongji++;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 8:
                    fangyu++;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 9:
                    huangkey++;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 10:
                    lankey++;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 11:
                    hongkey++;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 12:
                    if (gongji <= _monsinfo[12].fangyu)
                        return false;
                    if (Math.Abs(Math.Abs(shengming)) <= damage(12))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(12);
                        jingbi += _monsinfo[12].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 13:
                    if (gongji <= _monsinfo[13].fangyu)
                        return false;
                    if (shengming <= damage(13))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(13);
                        jingbi += _monsinfo[13].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 14:
                    if (gongji <= _monsinfo[14].fangyu)
                        return false;
                    if (shengming<= damage(14))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(14);
                        jingbi += _monsinfo[14].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 15:
                    if (gongji <= _monsinfo[15].fangyu)
                        return false;
                    if (shengming <= damage(15))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(15);
                        jingbi += _monsinfo[15].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 16:
                    if (huangkey > 0)
                    {
                        huangkey--;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    else
                        return false;
                    break;
                case 17:
                    if (lankey > 0)
                    {
                        lankey--;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    else return false;
                    break;
                case 18:
                    if (hongkey > 0)
                    {
                        hongkey--;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    else return false;
                    break;
                case 19://米色门为触发门
                    return false;
                case 20://智者显示提示
                    MessageBox.Show(this, zhizhehua[cengshu], "智者", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                case 21://商人显示商店
                    if (shangrenyimai[cengshu])
                    {
                        MessageBox.Show(this, shangrenhua2[cengshu], "商人", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        DialogResult dlgr = MessageBox.Show(this, shangrenhua[cengshu], "商人", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                        if (dlgr == DialogResult.Yes)//购买
                        {
                            shangpin sp = shangpins[cengshu];
                            bool goumaiflag = true;//能够购买
                            for (int ii = 0; ii < sp.cnt2; ii++)
                            {
                                if (sp.ID2[ii] == 0)
                                {
                                    if (jingbi < sp.shuliang2[ii])
                                    {
                                        goumaiflag = false;
                                        break;
                                    }
                                }
                                else
                                {
                                    switch (sp.ID2[ii])
                                    {
                                        case 9://回收黄钥匙
                                            if (huangkey == 0)
                                            {
                                                goumaiflag = false;
                                                break;
                                            }
                                            break;
                                    }
                                }
                            }
                            if (goumaiflag)
                            {
                                shangrenyimai[cengshu] = true;
                                for (int jj = 0; jj < sp.cnt1; jj++)
                                {
                                    if (sp.ID1[jj] == 0)
                                    {
                                        jingbi += sp.shuliang1[jj];
                                    }
                                    else
                                    {
                                        switch (sp.ID1[jj])
                                        {
                                            case 9:
                                                huangkey += sp.shuliang1[jj];
                                                break;
                                            case 10:
                                                lankey += sp.shuliang1[jj];
                                                break;
                                            case 11:
                                                hongkey += sp.shuliang1[jj];
                                                break;
                                        }
                                    }
                                }
                                for (int ii = 0; ii < sp.cnt2; ii++)
                                {
                                    if (sp.ID2[ii] == 0)
                                    {
                                        jingbi -= sp.shuliang2[ii];
                                    }
                                    else
                                    {
                                        switch (sp.ID2[ii])
                                        {
                                            case 9://回收黄钥匙
                                                huangkey--;
                                                break;
                                        }
                                    }
                                }
                                MessageBox.Show(this, shangrenhua2[cengshu], "商人", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else
                            {
                                MessageBox.Show(this, "对不起，你无法购买！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    return false;
                case 22://商店显示
                    label2.Text = "花费" + suoxujinbi.ToString() + "金币来提升以下一项能力：";
                    button1.Text = "攻击力+2";
                    button2.Text = "防御力+4";
                    button3.Text = "生命+100";
                    zhandou = true;
                    label2.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    label2.Visible = true;
                    return false;
                case 23:
                    if (gongji <= _monsinfo[23].fangyu)
                        return false;
                    if (Math.Abs(Math.Abs(shengming)) <= damage(23))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(23);
                        jingbi += _monsinfo[23].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 24:
                    if (gongji <= _monsinfo[24].fangyu)
                        return false;
                    if (shengming <= damage(24))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(24);
                        jingbi += _monsinfo[24].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 25:
                    return false;
                case 26:
                    change = true;
                    cengshu--;
                    if (map[cengshu].mp[(j + 1) * 13 + i] == 1)
                    {
                        weizhi = new Point(i, j + 1);
                        _chaoxiang = chaoxiang.xia;
                    }
                    else if (map[cengshu].mp[j * 13 + i + 1] == 1)
                    {
                        weizhi = new Point(i + 1, j);
                        _chaoxiang = chaoxiang.you;
                    }
                    else if (map[cengshu].mp[j * 13 + i - 1] == 1)
                    {
                        weizhi = new Point(i - 1, j);
                        _chaoxiang = chaoxiang.zuo;
                    }
                    else if (map[cengshu].mp[(j - 1) * 13 + i] == 1)
                    {
                        weizhi = new Point(i, j - 1);
                        _chaoxiang = chaoxiang.shang;
                    }
                    pictureBox1.Invalidate();
                    return false;
                case 27://显示消息
                    if (cengshu == 2 && weizhi == new Point(1, 8))
                    {
                        MessageBox.Show(this, "我们终于逃出来了。你的剑和盾被警卫拿走了，你必须先找到武器。我知道铁剑在5楼，铁盾在9楼，你最好先渠道它们。我现在有事要做没法帮你，再见。", "小偷", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else if(cengshu==2)
                    {
                        MessageBox.Show(this, "你清醒了吗？你到监狱时还处在昏迷中，魔法警卫把你扔到我这个房间。但你很幸运，我刚完成逃跑的暗道你就醒了，我们一起越狱吧。", "小偷", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        map[2].mp[7 * 13 + 3] = 1;
                        map[2].mp[9 * 13 + 1] = 27;
                        map[2].mp[7 * 13 + 2] = 1;
                    }
                    else if (cengshu == 10)
                    {
                        MessageBox.Show(this, "恭喜你通关了魔塔10层！！！", "小偷", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    return false;
                case 28:
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 29:
                    label2.Text = "花费" + suoxujinbi.ToString() + "金币来提升以下一项能力：";
                    button1.Text = "攻击力+2";
                    button2.Text = "防御力+4";
                    button3.Text = "生命+100";
                    zhandou = true;
                    label2.Enabled = true;
                    button1.Enabled = true;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    label2.Visible = true;
                    return false;
                case 30:
                    gongji += 10;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 31:
                    fangyu += 10;
                    map[cengshu].mp[j * 13 + i] = 1;
                    break;
                case 32:
                    if (gongji <= _monsinfo[32].fangyu)
                        return false;
                    if (shengming <= damage(32))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming -= damage(32);
                        jingbi += _monsinfo[32].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
                case 33:
                    if (gongji <= _monsinfo[33].fangyu)
                        return false;
                    if (Math.Abs(Math.Abs(shengming)) <= damage(33))
                        return false;
                    else
                    {
                        battle.Enabled = true;
                        zhandou = true;
                        shengming-= damage(33);
                        jingbi += _monsinfo[33].jingqian;
                        map[cengshu].mp[j * 13 + i] = 1;
                    }
                    break;
            }
            return true;
        }
        private bool dontmove(string dir)
        {
            int i = weizhi.X, j = weizhi.Y;
            if (dir == "shang") j -= 1;
            else if (dir == "xia") j += 1;
            else if (dir == "zuo") i -= 1;
            else if (dir == "you") i += 1;
            if (map[cengshu].mp[j * 13 + i] == 1)
                return false;
            else return true;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (zhandou) return;
            if (e.KeyCode == Keys.Up)
            {
                if (dontmove("shang") && _chaoxiang != chaoxiang.shang)
                {
                    _chaoxiang = chaoxiang.shang;
                    pictureBox1.Invalidate();
                    return;
                }
                if (go("shang"))
                {
                    if (weizhi.Y == 1) { }
                    else weizhi = new Point(weizhi.X, weizhi.Y - 1);
                }
                if (!change) _chaoxiang = chaoxiang.shang;
                change = false;
            }
            else if (e.KeyCode == Keys.Down)
            {
                if (dontmove("xia") && _chaoxiang != chaoxiang.xia)
                {
                    _chaoxiang = chaoxiang.xia;
                    pictureBox1.Invalidate();
                    return;
                }
                if (go("xia"))
                {
                    if (weizhi.Y == 11) { }
                    else weizhi = new Point(weizhi.X, weizhi.Y + 1);
                }
                if (!change) _chaoxiang = chaoxiang.xia;
                change = false;
            }
            else if (e.KeyCode == Keys.Left)
            {
                if (dontmove("zuo") && _chaoxiang != chaoxiang.zuo)
                {
                    _chaoxiang = chaoxiang.zuo;
                    pictureBox1.Invalidate();
                    return;
                }
                if (go("zuo"))
                {
                    if (weizhi.X == 1) { }
                    else weizhi = new Point(weizhi.X - 1, weizhi.Y);
                }
                if (!change) _chaoxiang = chaoxiang.zuo;
                change = false;
            }
            else if (e.KeyCode == Keys.Right)
            {
                if (dontmove("you") && _chaoxiang != chaoxiang.you)
                {
                    _chaoxiang = chaoxiang.you;
                    pictureBox1.Invalidate();
                    return;
                }
                if (go("you"))
                {
                    if (weizhi.X == 11) { }
                    else weizhi = new Point(weizhi.X + 1, weizhi.Y);
                }
                if (!change) _chaoxiang = chaoxiang.you;
                change = false;
            }
            //MessageBox.Show(weizhi.X.ToString()+ weizhi.Y.ToString());
            pictureBox1.Invalidate();
            if (!zhandou) JudgeEvent();
        }
        private void JudgeEvent()
        {
            if (eventsCnt[cengshu] != 0)
            {
                for (int i = 0; i < eventsCnt[cengshu]; i++)
                {
                    Event ev = events[cengshu, i];
                    if (ev.actived) continue;
                    switch (ev.type)
                    {
                        case 1:
                            bool flag = true;
                            for (int j = 0; j < ev.pointCnt; j++)
                            {
                                if (map[cengshu].mp[ev.li[j].Y * 13 + ev.li[j].X] != ev.ID[j])
                                {
                                    flag = false;
                                    break;
                                }
                            }
                            if (flag)
                            {
                                for (int j = 0; j < ev.pointCnt2; j++)
                                {
                                    map[cengshu].mp[ev.li2[j].Y * 13 + ev.li2[j].X] = ev.ID2[j];
                                }
                                events[cengshu, i].actived = true;
                            }
                            break;
                        case 2:
                            if (weizhi == ev.li[0])
                            {
                                if (ev.messagef==1)
                                {
                                    MessageBox.Show(this, ev.message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    pictureBox1.Invalidate();
                                }
                                for (int j = 0; j < ev.pointCnt2; j++)
                                {
                                    map[cengshu].mp[ev.li2[j].Y * 13 + ev.li2[j].X] = ev.ID2[j];
                                }
                                if (ev.messagef == 2)
                                {
                                    pictureBox1.Invalidate();
                                    MessageBox.Show(this, ev.message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                events[cengshu, i].actived = true;
                            }
                            break;
                        case 3:
                            if (weizhi == ev.li[0])
                            {
                                if (ev.messagef==1)
                                {
                                    MessageBox.Show(this, ev.message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    pictureBox1.Invalidate();
                                }
                                weizhi = ev.li2[0];
                                cengshu = ev.mubiaolouceng;
                                if (ev.messagef == 2)
                                {
                                    pictureBox1.Invalidate();
                                    MessageBox.Show(this, ev.message, "信息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                                events[cengshu, i].actived = true;
                            }
                            break;
                    }

                }
            }
        }
        private void battle_Tick(object sender, EventArgs e)
        {
            zhandou = false;
            battle.Enabled = false;
            pictureBox1.Invalidate();
            JudgeEvent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (jingbi >= suoxujinbi)
            {
                gongji += 2;
                button4.Text = "结束购买";
                suoxujinbi *= 2;
                label2.Text = "花费" + suoxujinbi.ToString() + "金币来提升以下一项能力：";
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show(this, "你的金币不足！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (jingbi >= suoxujinbi)
            {
                fangyu += 4;
                button4.Text = "结束购买";
                suoxujinbi *= 2;
                label2.Text = "花费" + suoxujinbi.ToString() + "金币来提升以下一项能力：";
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show(this, "你的金币不足！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (jingbi >= suoxujinbi)
            {
                shengming += 100;
                button4.Text = "结束购买";
                suoxujinbi *= 2;
                label2.Text = "花费" + suoxujinbi.ToString() + "金币来提升以下一项能力：";
                pictureBox1.Invalidate();
            }
            else
            {
                MessageBox.Show(this, "你的金币不足！", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            label2.Enabled = false;
            label2.Visible = false;
            button1.Visible = false;
            button2.Visible = false;
            button3.Visible = false;
            button4.Visible = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            zhandou = false;
            pictureBox1.Invalidate();
        }
    }
}