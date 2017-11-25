using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Snow
{
    public partial class Form1 : Form
    {
        private const uint WS_EX_LAYERED = 0x80000;
        private const int WS_EX_TRANSPARENT = 0x20;
        private const int GWL_STYLE = (-16);
        private const int GWL_EXSTYLE = (-20);
        private const int LWA_ALPHA = 0x2;
        Size OutTaskBarSize = new Size(SystemInformation.WorkingArea.Width, SystemInformation.WorkingArea.Height);
        Size ScreenSize = new Size(Screen.PrimaryScreen.Bounds.Width, Screen.PrimaryScreen.Bounds.Height);
        Size TaskBarSize;
        [DllImport("user32", EntryPoint = "SetWindowLong")]
        private static extern uint SetWindowLong(IntPtr hwnd,int nIndex,uint dwNewLong);
        [DllImport("user32", EntryPoint = "GetWindowLong")]
        private static extern uint GetWindowLong(IntPtr hwnd, int nIndex);
        [DllImport("user32", EntryPoint = "SetLayeredWindowAttributes")]
        private static extern int SetLayeredWindowAttributes(IntPtr hwnd,int crKey,int bAlpha,int dwFlags);
        public void CanPenetrate()
        {
            uint oldGWLEx = SetWindowLong(Handle, GWL_EXSTYLE, WS_EX_TRANSPARENT | WS_EX_LAYERED);
        }
        private static readonly Random rand = new Random();
        private readonly List<SnowFlake> SnowFlakes = new List<SnowFlake>();
        private class SnowFlake
        {
            //public float Rotation;//旋转
            //public float RotVelocity;
            //public float Scale;//缩放
            public float X;
            public float XVelocity;
            public float Y;
            public float YVelocity;
            public int image;
        }
        Image screenImage;
        private Image[] bmp = new Image[4];
        public Form1()
        {
            InitializeComponent();
            bmp[0] = Image.FromFile(@"Resources\1.png");
            bmp[1] = Image.FromFile(@"Resources\2.png");
            bmp[2] = Image.FromFile(@"Resources\3.png");
            bmp[3] = Image.FromFile(@"Resources\4.png");
            TaskBarSize = new Size((ScreenSize.Width - (ScreenSize.Width - OutTaskBarSize.Width)), (ScreenSize.Height - OutTaskBarSize.Height));
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            CanPenetrate();
            screenImage = new Bitmap(Screen.PrimaryScreen.Bounds.Width + 10, Screen.PrimaryScreen.Bounds.Height + 10);
        }
        private void SetBackground(Image img)
        {
            try
            {
                Bitmap bitmap = (Bitmap)img;
                if (bitmap.PixelFormat != PixelFormat.Format32bppArgb)
                {
                    throw new ApplicationException();
                }
                IntPtr hObject = IntPtr.Zero;
                IntPtr zero = IntPtr.Zero;
                IntPtr hDC = Win32.GetDC(IntPtr.Zero);
                IntPtr ptr2 = Win32.CreateCompatibleDC(hDC);
                try
                {
                    hObject = bitmap.GetHbitmap(Color.FromArgb(0));
                    zero = Win32.SelectObject(ptr2, hObject);
                    Win32.Size size2 = new Win32.Size(bitmap.Width, bitmap.Height);
                    Win32.Size psize = size2;
                    Win32.Point point3 = new Win32.Point(0, 0);
                    Win32.Point pprSrc = point3;
                    point3 = new Win32.Point(Left,Top);
                    Win32.Point pptDst = point3;
                    Win32.BLENDFUNCTION pblend = new Win32.BLENDFUNCTION();
                    pblend.BlendOp = 0;
                    pblend.BlendFlags = 0;
                    pblend.SourceConstantAlpha = 0xff;
                    pblend.AlphaFormat = 1;
                    Win32.UpdateLayeredWindow(Handle, hDC, ref pptDst, ref psize, ptr2, ref pprSrc, 0, ref pblend, 2);
                }
                catch (Exception exception1)
                {
                    Exception exception = exception1;
                    throw exception;
                }
                finally
                {
                    Win32.ReleaseDC(IntPtr.Zero, hDC);
                    if (hObject != IntPtr.Zero)
                    {
                        Win32.SelectObject(ptr2, zero);
                        Win32.DeleteObject(hObject);
                    }
                    Win32.DeleteDC(ptr2);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle |= 0x80000;
                return createParams;
            }
        }
        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Dispose();
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            Graphics g = Graphics.FromImage(screenImage);
            g.SmoothingMode = SmoothingMode.HighSpeed;
            g.PixelOffsetMode = PixelOffsetMode.HighQuality;
            g.Clear(Color.Transparent);
            for (int i = 0; i < SnowFlakes.Count; i++)
            {
                SnowFlake s1 = SnowFlakes[i];
                if (s1.X < -10 || s1.X > Width-10) SnowFlakes.RemoveAt(i);
                if (s1.Y >= Height - 15 - TaskBarSize.Height && s1.Y < Height - 8)//落到任务栏上
                {
                    s1.Y += 0.8f;
                    g.ResetTransform();
                    g.TranslateTransform(-16, -16, MatrixOrder.Append); //pan
                    //g.ScaleTransform(s1.Scale, s1.Scale, MatrixOrder.Append); //scale
                    //g.RotateTransform(s1.Rotation, MatrixOrder.Append); //rotate
                    g.TranslateTransform(s1.X, s1.Y, MatrixOrder.Append); //pan
                    g.DrawImage(bmp[s1.image], 0, 0); //draw
                    continue;
                }
                s1.Y += s1.YVelocity;
                if (飘动ToolStripMenuItem.Checked)
                {
                    s1.X += s1.XVelocity;
                    //s1.Rotation += s1.RotVelocity;
                    s1.XVelocity += ((float)rand.NextDouble() - 0.5f) * 0.7f;
                    s1.XVelocity = Math.Max(s1.XVelocity, -2f);
                    s1.XVelocity = Math.Min(s1.XVelocity, +2f);
                }

                if (s1.Y > Height) SnowFlakes.RemoveAt(i);
                else
                {
                    g.ResetTransform();
                    g.TranslateTransform(-16, -16, MatrixOrder.Append); //pan
                    //g.ScaleTransform(s1.Scale, s1.Scale, MatrixOrder.Append); //scale
                    //g.RotateTransform(s1.Rotation, MatrixOrder.Append); //rotate
                    g.TranslateTransform(s1.X, s1.Y, MatrixOrder.Append); //pan
                    g.DrawImage(bmp[s1.image], 0, 0);//draw
                }
            }
            g.Dispose();
            SetBackground(screenImage);
        }
        private void timer1_Tick(object sender, EventArgs e)
        {
            SnowFlake s = new SnowFlake();
            Random rd = new Random();
            switch(rd.Next(1,6))
            {
                case 1:
                    timer1.Interval = 300;
                    break;
                case 2:
                    timer1.Interval = 900;
                    break;
                case 3:
                    timer1.Interval = 1200;
                    break;
            }
            s.X = rand.Next(-20, Width + 20);
            s.Y = -10f;
            s.XVelocity = (float)(rand.NextDouble() - 0.5f) * 2f;
            s.YVelocity = (float)(rand.NextDouble() * 3) + 1f;
            //s.Rotation = rand.Next(0, 359);
            //s.RotVelocity = rand.Next(-3, 3) * 2;
            s.image = rd.Next(1, 4);
            //if (s.RotVelocity == 0)
            //{
            //    s.RotVelocity = 3;
            //}
            //s.Scale = (float)(rand.NextDouble() / 2) + 0.75f;
            SnowFlakes.Add(s);
        }
        private void 暂停ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = false;
            timer1.Enabled = false;
            暂停ToolStripMenuItem.Enabled = false;
            继续ToolStripMenuItem.Enabled = true;
        }
        private void 继续ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer.Enabled = true;
            timer1.Enabled = true;
            暂停ToolStripMenuItem.Enabled = true;
            继续ToolStripMenuItem.Enabled = false;
        }
        private void 多ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 300;
            自动ToolStripMenuItem.Checked = false;
            多ToolStripMenuItem.Checked = true;
            中ToolStripMenuItem.Checked = false;
            少ToolStripMenuItem.Checked = false;
        }
        private void 中ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 900;
            自动ToolStripMenuItem.Checked = false;
            多ToolStripMenuItem.Checked = false;
            中ToolStripMenuItem.Checked = true;
            少ToolStripMenuItem.Checked = false;
        }
        private void 少ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Interval = 1200;
            自动ToolStripMenuItem.Checked = false;
            多ToolStripMenuItem.Checked = false;
            中ToolStripMenuItem.Checked = false;
            少ToolStripMenuItem.Checked = true;
        }
        private void 置顶ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(置顶ToolStripMenuItem.Checked)
            {
                置顶ToolStripMenuItem.Checked = false;
                TopMost = false;
            }
            else
            {
                置顶ToolStripMenuItem.Checked = true;
                TopMost = true;
            }
        }
        private void 飘动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (飘动ToolStripMenuItem.Checked)
            {
                飘动ToolStripMenuItem.Checked = false;
            }
            else
            {
                飘动ToolStripMenuItem.Checked = true;
            }
        }
        private void 自动ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            自动ToolStripMenuItem.Checked = true;
            多ToolStripMenuItem.Checked = false;
            中ToolStripMenuItem.Checked = false;
            少ToolStripMenuItem.Checked = false;
        }
    }
}