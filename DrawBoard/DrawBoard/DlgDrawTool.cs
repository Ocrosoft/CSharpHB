using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace DrawBoard
{
    public partial class DlgDrawTool : Form
    {
        [DllImport("user32.dll")]
        public static extern bool GetCursorPos(out Point pt);
        private int _tiekao = 0;
        public FormMain _formMain;
        public DlgDrawTool()
        {
            InitializeComponent();
        }

        private void buttonExit_Click(object sender, EventArgs e)
        {
            _formMain.flowLayoutPanel1.Visible = true;
            _formMain.menuStrip1.Visible = true;
            _formMain.toolStrip1.Visible = true;
            _formMain.statusStrip1.Visible = true;
            _formMain.FormBorderStyle = FormBorderStyle.Sizable;
            this.Close();
        }

        private void buttonLine_Click(object sender, EventArgs e)
        {
            _formMain.MenuItemLine_Click(sender, e);
            buttonLine.Focus();
            buttonStop.Enabled = true;
        }

        private void buttonColor_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            _formMain.MenuItemColor_Click(sender, e);
            this.TopMost = true;
            if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
            else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
            else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
        }

        private void buttonRegtangle_Click(object sender, EventArgs e)
        {
            _formMain.MenuItemRectangle_Click(sender, e);
            buttonRegtangle.Focus();
            buttonStop.Enabled = true;
        }

        private void buttonCircle_Click(object sender, EventArgs e)
        {
            _formMain.MenuItemCircle_Click(sender, e);
            buttonCircle.Focus();
            buttonStop.Enabled = true;
        }

        private void buttonSketch_Click(object sender, EventArgs e)
        {
            _formMain.toolStripSketch_Click(sender, e);
            buttonSketch.Focus();
            buttonStop.Enabled = true;
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            _formMain.MenuItemStop_Click(sender, e);
            buttonfocus.Focus();
            buttonStop.Enabled = false;
        }

        private void buttonUndo_Click(object sender, EventArgs e)
        {
            //会导致截图失效
            _formMain.MenuItemUndo_Click(sender, e);
            //enable问题再说
            if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
            else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
            else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
        }

        private void buttonWidth_Click(object sender, EventArgs e)
        {
            this.TopMost = false;
            _formMain.MenuItemWidth_Click(sender, e);
            this.TopMost = true;
            if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
            else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
            else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
        }

        private void buttonSave_Click(object sender, EventArgs e)
        {
            _formMain.MenuItemSave_Click(sender, e);
            if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
            else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
            else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
        }

        private void buttonSaveAs_Click(object sender, EventArgs e)
        {
            _formMain.MenuItemSaveAs_Click(sender, e);
            if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
            else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
            else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
        }

        private void DlgDrawTool_FormClosed(object sender, FormClosingEventArgs e)
        {
            _formMain.flowLayoutPanel1.Visible = true;
            _formMain.menuStrip1.Visible = true;
            _formMain.toolStrip1.Visible = true;
            _formMain.statusStrip1.Visible = true;
            _formMain.FormBorderStyle = FormBorderStyle.Sizable;
        }

        private void DlgDrawTool_Move(object sender, EventArgs e)
        {
            _tiekao = 0;
            int screenRight = Screen.PrimaryScreen.Bounds.Right;
            int formRight = this.Left + this.Size.Width;
            if (Math.Abs(screenRight - formRight) <= 10)
            { this.Left = screenRight - this.Size.Width; _tiekao = 3; }
            if (Math.Abs(this.Left) <= 10)
            { this.Left = -1; _tiekao = 1; }
            int screenBottom = Screen.PrimaryScreen.Bounds.Bottom;
            int formBottom = this.Top + this.Size.Height;
            if (Math.Abs(screenBottom - formBottom) <= 10)
            { this.Top = this.Top - this.Size.Height; _tiekao = 4; }
            if (Math.Abs(this.Top) <= 10)
            { this.Top = -1; _tiekao = 2; }
            /*
            if (Math.Abs(screenRight - formRight) > 10 && Math.Abs(this.Left) > 10 && Math.Abs(screenBottom - formBottom) > 10 && Math.Abs(this.Top) > 10)
                _tiekao = false;
            */
        }

        private void DlgDrawTool_MouseEnter(object sender, EventArgs e)
        {
            int screenRight = Screen.PrimaryScreen.Bounds.Right;
            int formRight = this.Left + this.Size.Width;
            if (_tiekao == 1)
                this.Left = -1;
            else if (_tiekao == 2)
                this.Top = -1;
            else if (_tiekao == 3)
                this.Left = screenRight - this.Size.Width;
            else if (_tiekao == 4)
                this.Top = this.Top - this.Size.Height;
        }

        private void DlgDrawTool_MouseLeave(object sender, EventArgs e){}

        private void timer1_Tick(object sender, EventArgs e)
        {
            Point p;
            GetCursorPos(out p);
            if (p.X > Location.X && p.X < Location.X + Width && p.Y > Location.Y && p.Y < Location.Y + Height) return;
            int screenRight = Screen.PrimaryScreen.Bounds.Right;
            int formRight = this.Left + this.Size.Width;
            if (_tiekao == 1)
            { Left = -Width + 10; _tiekao = 1; }
            else if(_tiekao==2)
            { Top = -Height +10; _tiekao = 2;  }
            else if(_tiekao==3)
            { Left = screenRight - 10;_tiekao = 3; }
            //下面不贴靠
        }

        private void buttoncheck_Click(object sender, EventArgs e)
        {
            if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
            else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
            else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
        }
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
        private int KeyboardHookProc(int nCode, Int32 wParam, IntPtr lParam)
        {
            if (nCode >= 0)
            {
                KeyMSG m = (KeyMSG)Marshal.PtrToStructure(lParam, typeof(KeyMSG));
                if (m.flags !=0)
                {
                    if (_formMain._operatopnType == OperationType.Line) buttonLine.Focus();
                    else if (_formMain._operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
                    else if (_formMain._operatopnType == OperationType.Sketch) buttonSketch.Focus();
                    else if (_formMain._operatopnType == OperationType.Circle) buttonCircle.Focus();
                    else if (_formMain._operatopnType == OperationType.Stop) buttonfocus.Focus();
                    _formMain.Focus();
                    m.flags++;
                    return 1;
                }
                return 0;
            }
            return CallNextHookEx(hKeyboarfHook, nCode, wParam, lParam);
        }
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

        private void DlgDrawTool_Load(object sender, EventArgs e)
        {
            //InitializeComponent();
            try
            {
                HookStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("钩子安装失败！提示信息：" + ex.Message);
            }
        }
    }
}
