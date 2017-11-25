using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Rhythm_Ocrosoft
{
    public partial class FormResult : Form
    {
        private static double _zoomRate = FormMain.ZoomRate / 2.0;
        private string _songName;//曲名
        private double rate;//正确率
        private double ra;
        private int _accept;//正确
        private int ac;
        private int _combo;//连击
        private int co;
        private int _keyCount;//总数
        bool fullCombo = false;
        bool allComplete = false;
        Bitmap backgroundImg = new Bitmap("Resources/BackGround-Blue.png");//背景
        Bitmap ReCircle = new Bitmap("Resources/ResultCircle.png");
        Bitmap charminghet = new Bitmap("Resources/Charming.png");
        Bitmap Combohit = new Bitmap("Resources/Combo.png");
        public FormResult()
        {
            InitializeComponent();
            Height = (int)(768 * _zoomRate);
            Width = (int)(1024 * _zoomRate);
        }
        public int Accept
        {
            get
            {
                return _accept;
            }

            set
            {
                _accept = value;
            }
        }
        public int Combo
        {
            get
            {
                return _combo;
            }

            set
            {
                _combo = value;
            }
        }
        public int KeyCount
        {
            get
            {
                return _keyCount;
            }

            set
            {
                _keyCount = value;
            }
        }
        public double Rate
        {
            get
            {
                return rate;
            }

            set
            {
                rate = value;
            }
        }

        public string SongName
        {
            get
            {
                return _songName;
            }

            set
            {
                _songName = value;
            }
        }

        private void FormResult_Load(object sender, EventArgs e)
        {
            co = 0;
            ac = 0;
            ra = 0;
            timerAccept.Enabled = true;
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.DrawImage(backgroundImg, 0, 0, (int)(1024 * _zoomRate), (int)(768 * _zoomRate));
            g.DrawString(SongName, new Font("Comic Sans MS", (float)(40 * _zoomRate)), new SolidBrush(Color.White), new Point((int)((1024 / 2.0 - SongName.Length / 2.0 * 25) * _zoomRate), (int)(10 * _zoomRate)));
            //g.DrawString(_songName, new Font("Comic Sans MS", (float)(40 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(10 * _zoomRate), (int)(340 * _zoomRate)));
            Random rand = new Random(DateTime.Now.Millisecond);
            if(ra==Rate)
            {
                if(ra==(int)ra)
                    g.DrawString((ra.ToString() + ".0000").Substring(0, 5) + "%", new Font("Comic Sans MS", (float)(50 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(400 * _zoomRate), (int)(150 * _zoomRate)));
                else
                    g.DrawString((ra.ToString() + "0000").Substring(0, 5) + "%", new Font("Comic Sans MS", (float)(50 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(400 * _zoomRate), (int)(150 * _zoomRate)));
            }
            else
                g.DrawString(ra.ToString()+"."+rand.Next(10,100).ToString()+"%", new Font("Comic Sans MS", (float)(50 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(400 * _zoomRate), (int)(150 * _zoomRate)));
            g.DrawImage(ReCircle, (int)(200 * _zoomRate), (int)(300 * _zoomRate), (int)(150 * _zoomRate), (int)(150 * _zoomRate));
            g.DrawString(Math.Abs(ac).ToString(), new Font("Comic Sans MS", (float)(22 * _zoomRate)), new SolidBrush(Color.Black), new Point((int)(225 * _zoomRate), (int)(330 * _zoomRate)));
            g.DrawString(_keyCount.ToString(), new Font("Comic Sans MS", (float)(22 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(275 * _zoomRate), (int)(370 * _zoomRate)));
            g.DrawImage(ReCircle, (int)(680 * _zoomRate), (int)(300 * _zoomRate), (int)(150 * _zoomRate), (int)(150 * _zoomRate));
            g.DrawString(Math.Abs(co).ToString(), new Font("Comic Sans MS", (float)(22 * _zoomRate)), new SolidBrush(Color.Black), new Point((int)(705 * _zoomRate), (int)(330 * _zoomRate)));
            g.DrawString(_keyCount.ToString(), new Font("Comic Sans MS", (float)(22 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(755 * _zoomRate), (int)(370 * _zoomRate)));
            g.DrawImage(charminghet, (int)(100 * _zoomRate), (int)(500 * _zoomRate), (int)(389 * _zoomRate), (int)(89 * _zoomRate));
            g.DrawImage(Combohit, (int)(610 * _zoomRate), (int)(500 * _zoomRate), (int)(301 * _zoomRate), (int)(81 * _zoomRate));
            if(allComplete)
                g.DrawString("All Complete", new Font("Comic Sans MS", (float)(40 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(355 * _zoomRate), (int)(650 * _zoomRate)));
            else if(fullCombo)
                g.DrawString("Full Combo", new Font("Comic Sans MS", (float)(40 * _zoomRate)), new SolidBrush(Color.White), new Point((int)(390 * _zoomRate), (int)(650 * _zoomRate)));
        }
        private void timerAccept_Tick(object sender, EventArgs e)
        {
            if (Math.Abs(ac) < Math.Abs(_accept))
                ac--;
            if (Math.Abs(co) < Math.Abs(_combo))
                co--;
            if (ra < Rate)
                ra++;
            else
            {
                ra = Rate;
            }
            if (Math.Abs(ac) == Math.Abs(_accept) && Math.Abs(co) == Math.Abs(_combo) && ra == Rate)
            {
                if (Math.Abs(ac) == _keyCount) allComplete = true;
                if (Math.Abs(_combo) == _keyCount) fullCombo = true;
                timerAccept.Enabled = false;
            }
            pictureBox1.Invalidate();
        }
        private void timerPerfect_Tick(object sender, EventArgs e)
        {
            //
        }
        private void timerRate_Tick(object sender, EventArgs e)
        {
            //
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (Math.Abs(ac) == Math.Abs(_accept) && Math.Abs(co) == Math.Abs(_combo) && ra == Rate)
            {
                button1.PerformClick();
                Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
