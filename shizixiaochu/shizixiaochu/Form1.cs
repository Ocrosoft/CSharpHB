using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Media;
using System.Windows.Forms;

namespace shizixiaochu
{
    public partial class Form1 : Form
    {
        private bool[] pa = new bool[4]; 
        double per = 1.00;
        Color back1 = Color.FromArgb(255, 237, 237, 237);
        Color back2 = Color.FromArgb(255, 247, 247, 247);
        Point mouse;
        List<Color> cr = new List<Color>();
        bool[, ,] used = new bool[265, 265, 265];
        Color[,] Color_map = new Color[30, 30];
        Color[,] Color_RE = new Color[30, 30];
        SoundPlayer xiao = new SoundPlayer("xiao.wav");
        SoundPlayer cuo = new SoundPlayer("cuo.wav");
        SoundPlayer end = new SoundPlayer("jieshu.wav");
        Random random = new Random(System.DateTime.Now.Millisecond);
        public Form1()
        {
            InitializeComponent();
            MessageBox.Show(random.Next(1,10).ToString());
            while (cr.Count != 10)
            {
                int r = random.Next(0, 200), g = random.Next(0, 200), b = random.Next(0, 200);
                if (used[r, g, b]) continue;
                cr.Add(Color.FromArgb(255, r, g, b));
                used[r, g, b] = true;
            }
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (i % 2 != 0)
                    {
                        if (j % 2 != 0)
                        {
                            Color_RE[i, j] = back1;
                        }
                        else
                        {
                            Color_RE[i, j] = back2;
                        }
                    }
                    else
                    {
                        if (j % 2 != 0)
                        {
                            Color_RE[i, j] = back2;
                        }
                        else
                        {
                            Color_RE[i, j] = back1;
                        }
                    }
                }
            }
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    if (random.Next(1, 51) < 30)
                        Color_map[i, j] = cr[random.Next(0, 10)];
                    else
                    {
                        if (i % 2 != 0)
                        {
                            if (j % 2 != 0)
                            {
                                Color_map[i, j] = back1;
                            }
                            else
                            {
                                Color_map[i, j] = back2;
                            }
                        }
                        else
                        {
                            if (j % 2 != 0)
                            {
                                Color_map[i, j] = back2;
                            }
                            else
                            {
                                Color_map[i, j] = back1;
                            }
                        }
                    }
                }
            }
        }
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(Color.Green), new Rectangle(new Point(0, 20 * 30), new Size((int)(25 * 30 * per), 10)));
            for (int i = 0; i < 25; i++)
            {
                for (int j = 0; j < 20; j++)
                {
                    FillRoundRectangle(g, new SolidBrush(Color_map[i, j]), new Rectangle(new Point(i * 30, j * 30), new Size(30, 30)), 4);
                }
            }
        }
        public static void FillRoundRectangle(Graphics g, Brush brush, Rectangle rect, int cornerRadius)
        {
            using (GraphicsPath path = CreateRoundedRectanglePath(rect, cornerRadius))
            {
                g.FillPath(brush, path);
            }
        }
        internal static GraphicsPath CreateRoundedRectanglePath(Rectangle rect, int cornerRadius)
        {
            GraphicsPath roundedRect = new GraphicsPath();
            roundedRect.AddArc(rect.X, rect.Y, cornerRadius * 2, cornerRadius * 2, 180, 90);
            roundedRect.AddLine(rect.X + cornerRadius, rect.Y, rect.Right - cornerRadius * 2, rect.Y);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y, cornerRadius * 2, cornerRadius * 2, 270, 90);
            roundedRect.AddLine(rect.Right, rect.Y + cornerRadius * 2, rect.Right, rect.Y + rect.Height - cornerRadius * 2);
            roundedRect.AddArc(rect.X + rect.Width - cornerRadius * 2, rect.Y + rect.Height - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 0, 90);
            roundedRect.AddLine(rect.Right - cornerRadius * 2, rect.Bottom, rect.X + cornerRadius * 2, rect.Bottom);
            roundedRect.AddArc(rect.X, rect.Bottom - cornerRadius * 2, cornerRadius * 2, cornerRadius * 2, 90, 90);
            roundedRect.AddLine(rect.X, rect.Bottom - cornerRadius * 2, rect.X, rect.Y + cornerRadius * 2);
            roundedRect.CloseFigure();
            return roundedRect;
        } 
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            int i, j;
            bool finish=true;
            for (i = 0; i < 25; i++)
            {
                for (j = 0; j < 20; j++)
                {
                    if (Color_map[i, j] != Color.FromArgb(255, 237, 237, 237) && Color_map[i, j] != Color.FromArgb(255, 247, 247, 247))
                    {
                        finish = false;
                    }
                }
            }
            if (finish)
            {
                timer1.Enabled = false;
                DialogResult dr = MessageBox.Show(this, "游戏结束，是否要开始新的一局？", "游戏结束", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (dr == DialogResult.Yes)
                {
                    per = 1;
                    timer1.Enabled = true;
                }
                else
                {
                    this.Close();
                }
            }
            mouse = e.Location;
            i = e.X / 30;
            j = e.Y / 30;
            if (Color_map[i, j] == back1 || Color_map[i, j] == back2)
                pictureBox1.Cursor = Cursors.Hand;
            else pictureBox1.Cursor = Cursors.Default;
        }
        private void pictureBox1_Click(object sender, EventArgs e)
        {
            int x = mouse.X / 30;
            int y = mouse.Y / 30;
            if (Color_map[x, y] == back1 || Color_map[x, y] == back2)
            {
                Color a = Color.FromArgb(255, 255, 255, 255), b = Color.FromArgb(255, 255, 255, 255), c = Color.FromArgb(255, 255, 255, 255), d = Color.FromArgb(255, 255, 255, 255);//左右上下
                Point pa = new Point(0, 0), pb = new Point(0, 0), pc = new Point(0, 0), pd = new Point(0, 0);
                bool fa, fb, fc, fd, flag = false;
                int i = x, j = y;
                while (i >= 0 && (Color_map[i, y] == back1 || Color_map[i, y] == back2))
                    i--;
                if (i >= 0)
                {
                    fa = true;
                    a = Color_map[i, y];
                    pa = new Point(i, y);
                }
                else fa = false;
                i = x; j = y;
                while (i < 25 && (Color_map[i, y] == back1 || Color_map[i, y] == back2))
                    i++;
                if (i < 25)
                {
                    fb = true;
                    b = Color_map[i, y];
                    pb = new Point(i, y);
                }
                else fb = false;
                i = x; j = y;
                while (j >= 0 && (Color_map[x, j] == back1 || Color_map[x, j] == back2))
                    j--;
                if (j >= 0)
                {
                    fc = true;
                    c = Color_map[x, j];
                    pc = new Point(x, j);
                }
                else fc = false;
                i = x; j = y;
                while (j < 20 && (Color_map[x, j] == back1 || Color_map[x, j] == back2))
                    j++;
                if (j < 20)
                {
                    fd = true;
                    d = Color_map[x, j];
                    pd = new Point(x, j);
                }
                else fd = false;
                if (fa)
                {
                    if (fb && a == b)
                    {
                        Color_map[pa.X, pa.Y] = Color_RE[pa.X, pa.Y];
                        Color_map[pb.X, pb.Y] = Color_RE[pb.X, pb.Y];
                        flag = true;
                    }
                    if (fc && a == c)
                    {
                        Color_map[pa.X, pa.Y] = Color_RE[pa.X, pa.Y];
                        Color_map[pc.X, pc.Y] = Color_RE[pc.X, pc.Y];
                        flag = true;
                    }
                    if (fd && a == d)
                    {
                        Color_map[pa.X, pa.Y] = Color_RE[pa.X, pa.Y];
                        Color_map[pd.X, pd.Y] = Color_RE[pd.X, pd.Y];
                        flag = true;
                    }
                }
                if (fb)
                {
                    if (fc && b == c)
                    {
                        Color_map[pb.X, pb.Y] = Color_RE[pb.X, pb.Y];
                        Color_map[pc.X, pc.Y] = Color_RE[pc.X, pc.Y];
                        flag = true;
                    }
                    if (fd && b == d)
                    {
                        Color_map[pb.X, pb.Y] = Color_RE[pb.X, pb.Y];
                        Color_map[pd.X, pd.Y] = Color_RE[pd.X, pd.Y];
                        flag = true;
                    }
                }
                if (fc)
                {
                    if (fd && c == d)
                    {
                        Color_map[pc.X, pc.Y] = Color_RE[pc.X, pc.Y];
                        Color_map[pd.X, pd.Y] = Color_RE[pd.X, pd.Y];
                        flag = true;
                    }
                }
                if (!flag)
                {
                    per -= 0.005;
                    cuo.Play();
                }
                else xiao.Play();

                pictureBox1.Invalidate();
            }
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            per -= 0.0001;
            if (per <= 0)
            {
                timer1.Enabled = false;
                per = 0;
                end.Play();
               DialogResult dr= MessageBox.Show(this, "游戏结束，是否要再挑战一次？", "游戏结束", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (dr == DialogResult.Yes)
               {
                   for (int i = 0; i < 25; i++)
                   {
                       for (int j = 0; j < 20; j++)
                       {
                           if (random.Next(1, 51) < 30)
                               Color_map[i, j] = cr[random.Next(0, 10)];
                           else
                           {
                               if (i % 2 != 0)
                               {
                                   if (j % 2 != 0)
                                   {
                                       Color_map[i, j] = back1;
                                   }
                                   else
                                   {
                                       Color_map[i, j] = back2;
                                   }
                               }
                               else
                               {
                                   if (j % 2 != 0)
                                   {
                                       Color_map[i, j] = back2;
                                   }
                                   else
                                   {
                                       Color_map[i, j] = back1;
                                   }
                               }
                           }
                       }
                   }
                   per = 1;
                   timer1.Enabled = true;
               }
               else
               {
                   this.Close();
               }
            }
            pictureBox1.Invalidate();
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.NumPad5)
            {
                pa[0] = true;
            }
            else if (e.KeyCode == Keys.NumPad4)
            {
                if (pa[0])
                {
                    if (pa[1])
                        pa[2] = true;
                    else
                        pa[1] = true;
                }
            }
            else if (e.KeyCode == Keys.NumPad6)
            {
                if (pa[2])
                {
                    pa[0] = false;
                    pa[1] = false;
                    pa[2] = false;
                    per += 0.1;
                    if (per > 1)
                        per = 1;
                }
                else
                {
                    pa[0] = false;
                    pa[1] = false;
                    pa[2] = false;
                }
            }
        }
    }
}
