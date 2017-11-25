using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Runtime.InteropServices;
using System.Reflection;
using System.Drawing.Drawing2D;

namespace DrawBoard
{
    public partial class FormMain : Form
    {
        static Graphics graphics = Graphics.FromHwnd(IntPtr.Zero);
        float dpiX = graphics.DpiX;
        float dpiY = graphics.DpiY;
        private List<Shape> _listShape = new List<Shape>();
        public OperationType _operatopnType = OperationType.Stop;
        private Bitmap _bufferBmp = null;
        private Graphics _bufferGraphics = null;
        public Bitmap _buffsavescr = null;
        public Graphics _buffsavescr2 = null;
        public Boolean _scrSaver = false;
        private int _drawPenWidth = 10;
        private Boolean _drawing = false;
        private Color _drawPenColor = Color.Red;
        private string _fileName = "无标题.dwg";
        //private Boolean _modifiedFlag = false;
        private double _zoomRatio = 1;
        private Boolean _reSize = false;
        private int _change = 0, _startchange = 0;//修改痕迹
        private Size _initialSize;
        Point PointFromRealToDisplay(Point p)
        { return new Point((int)(p.X * _zoomRatio), (int)(p.Y * _zoomRatio)); }
        Point PointFromDisplayToReal(Point p)
        { return new Point((int)(p.X / _zoomRatio), (int)(p.Y / _zoomRatio)); }
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
                if (m.flags == 0 && m.vkCode == (int)(Keys.F2))
                {
                    MenuItemPrtScn_Click(this, null);
                    return 1;
                }
                if (m.flags == 0 && m.vkCode == (int)(Keys.F3))
                {
                    MenuItemSCRPen_Click(this, null);
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
        public FormMain()
        {
            InitializeComponent();
            try
            {
                HookStart();
            }
            catch (Exception ex)
            {
                MessageBox.Show("钩子安装失败！提示信息：" + ex.Message);
            }
        }
        private void FormMain_MouseDown(object sender, MouseEventArgs e)
        {
            if (this.Cursor != Cursors.SizeNS && this.Cursor != Cursors.SizeNWSE && this.Cursor != Cursors.SizeWE)
            {
                if (_operatopnType == OperationType.Line)
                {
                    Line tempLine = new Line();
                    tempLine._P1 = PointFromDisplayToReal(new Point(e.X, e.Y));
                    _listShape.Add(tempLine);
                    _drawing = true;
                }
                else if (_operatopnType == OperationType.Rectangle)
                {
                    Rectangle tempRectangle = new Rectangle();
                    tempRectangle._P1 = PointFromDisplayToReal(new Point(e.X, e.Y));
                    _listShape.Add(tempRectangle);
                    _drawing = true;
                }
                else if (_operatopnType == OperationType.Circle)
                {
                    Circle tempCircle = new Circle();
                    tempCircle._pCenter = PointFromDisplayToReal(new Point(e.X, e.Y));
                    _listShape.Add(tempCircle);
                    _drawing = true;
                }
                else if (_operatopnType == OperationType.Sketch)
                {
                    Sketch tempSketch = new Sketch();
                    tempSketch._pointList.Add(PointFromDisplayToReal(new Point(e.X, e.Y)));
                    _listShape.Add(tempSketch);
                    _drawing = true;
                }
            }
        }

        private void FormMain_Paint(object sender, PaintEventArgs e)
        {
            this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
        }

        public void MenuItemLine_Click(object sender, EventArgs e)
        {
            buttonLine.Focus();
            _operatopnType = OperationType.Line;
            toolStripStatusLabelInfo.Text = "当前操作:绘制直线";
            this.Cursor = Cursors.Cross;
            MenuItemLine.CheckState = CheckState.Checked;
            MenuItemRectangle.CheckState = CheckState.Unchecked;
            MenuItemCircle.CheckState = CheckState.Unchecked;
            MenuItemStop.CheckState = CheckState.Unchecked;
            MenuItemSketch.CheckState = CheckState.Unchecked;
            toolStripLine.CheckState = CheckState.Checked;
            toolStripRectangle.CheckState = CheckState.Unchecked;
            toolStripCircle.CheckState = CheckState.Unchecked;
            toolStripStop.CheckState = CheckState.Unchecked;
            toolStripSketch.CheckState = CheckState.Unchecked;
            toolStripStop.Enabled = true;
            buttonStop.Enabled = true;
            toolStripLine.BackColor = SystemColors.Highlight;
            toolStripRectangle.BackColor = Color.White;
            toolStripCircle.BackColor = Color.White;
            toolStripSketch.BackColor = Color.White;
        }

        public void MenuItemRectangle_Click(object sender, EventArgs e)
        {
            buttonRegtangle.Focus();
            _operatopnType = OperationType.Rectangle;
            toolStripStatusLabelInfo.Text = "当前操作:绘制矩形";
            this.Cursor = Cursors.Cross;
            MenuItemLine.CheckState = CheckState.Unchecked;
            MenuItemRectangle.CheckState = CheckState.Checked;
            MenuItemCircle.CheckState = CheckState.Unchecked;
            MenuItemStop.CheckState = CheckState.Unchecked;
            toolStripLine.CheckState = CheckState.Unchecked;
            MenuItemSketch.CheckState = CheckState.Unchecked;
            toolStripRectangle.CheckState = CheckState.Checked;
            toolStripSketch.CheckState = CheckState.Unchecked;
            toolStripCircle.CheckState = CheckState.Unchecked;
            toolStripStop.CheckState = CheckState.Unchecked;
            toolStripStop.Enabled = true;
            buttonStop.Enabled = true;
            toolStripLine.BackColor = Color.White;
            toolStripRectangle.BackColor = SystemColors.Highlight;
            toolStripCircle.BackColor = Color.White;
            toolStripSketch.BackColor = Color.White;
        }

        public void MenuItemCircle_Click(object sender, EventArgs e)
        {
            buttonCircle.Focus();
            _operatopnType = OperationType.Circle;
            toolStripStatusLabelInfo.Text = "当前操作:绘制圆";
            this.Cursor = Cursors.Cross;
            MenuItemLine.CheckState = CheckState.Unchecked;
            MenuItemRectangle.CheckState = CheckState.Unchecked;
            MenuItemCircle.CheckState = CheckState.Checked;
            MenuItemStop.CheckState = CheckState.Unchecked;
            MenuItemSketch.CheckState = CheckState.Unchecked;
            toolStripLine.CheckState = CheckState.Unchecked;
            toolStripSketch.CheckState = CheckState.Unchecked;
            toolStripRectangle.CheckState = CheckState.Unchecked;
            toolStripCircle.CheckState = CheckState.Checked;
            toolStripStop.CheckState = CheckState.Unchecked;
            toolStripStop.Enabled = true;
            buttonStop.Enabled = true;
            toolStripLine.BackColor = Color.White;
            toolStripRectangle.BackColor = Color.White;
            toolStripSketch.BackColor = Color.White;
            toolStripCircle.BackColor = SystemColors.Highlight;
        }

        public void MenuItemStop_Click(object sender, EventArgs e)
        {
            buttonPut.Focus();
            _operatopnType = OperationType.Stop;//操作
            this.Cursor = Cursors.Default;//光标
            toolStripStatusLabelInfo.Text = "当前操作:停止";
            MenuItemLine.CheckState = CheckState.Unchecked;//菜单选中
            MenuItemRectangle.CheckState = CheckState.Unchecked;
            MenuItemCircle.CheckState = CheckState.Unchecked;
            MenuItemStop.CheckState = CheckState.Checked;
            MenuItemSketch.CheckState = CheckState.Unchecked;
            toolStripLine.CheckState = CheckState.Unchecked;//工具栏选中
            toolStripRectangle.CheckState = CheckState.Unchecked;
            toolStripCircle.CheckState = CheckState.Unchecked;
            toolStripStop.CheckState = CheckState.Checked;
            toolStripSketch.CheckState = CheckState.Unchecked;
            toolStripStop.Enabled = false;
            buttonStop.Enabled = false;
            toolStripLine.BackColor = Color.White;
            toolStripRectangle.BackColor = Color.White;
            toolStripSketch.BackColor = Color.White;
            toolStripCircle.BackColor = Color.White;
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            this._initialSize = new Size(this.panelDraw.Width, this.panelDraw.Height);
            panelDraw.MouseWheel += new MouseEventHandler(panelDraw_MouseWheel);
            panelContainer.MouseWheel += new MouseEventHandler(panelDraw_MouseWheel);
            menuStrip1.Cursor = Cursors.Default;
            toolStrip1.Cursor = Cursors.Default;
            flowLayoutPanel1.Cursor = Cursors.Default;
            statusStrip1.Cursor = Cursors.Default;
            _bufferBmp = new Bitmap(panelDraw.Width, panelDraw.Height);
            _buffsavescr = new Bitmap(panelDraw.Width, panelDraw.Height);
            _bufferGraphics = Graphics.FromImage(_bufferBmp);
            _bufferGraphics.Clear(Color.White);
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _bufferGraphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        }
        private void panelDraw_MouseWheel(object sender, MouseEventArgs e)
        {
            if (ModifierKeys == Keys.Control)
            {
                if (e.Delta > 0) MenuItemBig_Click(sender, e);
                else if (e.Delta < 0) MenuItemSmall_Click(sender, e);
            }
        }
        private void FormMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            _bufferGraphics.Dispose();
            _bufferBmp.Dispose();
            try
            {
                HookStop();
            }
            catch (Exception ex)
            {
                MessageBox.Show("钩子卸载失败！提示信息：" + ex.Message);
            }
        }

        private void FormMain_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                if (this.Cursor == Cursors.SizeWE)
                {
                    panelDraw.Width = e.X;
                    _reSize = true;
                    toolStripStatusMsg.Visible = true;
                    timerMouse.Enabled = true;
                }
                else if (this.Cursor == Cursors.SizeNS)
                {
                    panelDraw.Height = e.Y;
                    _reSize = true;
                    toolStripStatusMsg.Visible = true;
                    timerMouse.Enabled = true;
                }
                else if (this.Cursor == Cursors.SizeNWSE)
                {
                    panelDraw.Width = e.X;
                    panelDraw.Height = e.Y;
                    _reSize = true;
                    toolStripStatusMsg.Visible = true;
                    timerMouse.Enabled = true;
                }
                else
                {
                    if (_drawing)
                    {
                        if (_operatopnType == OperationType.Line)
                        {
                            this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
                            Pen pen = new Pen(this._drawPenColor, (float)(this._drawPenWidth * this._zoomRatio))
                            {
                                DashStyle = DashStyle.Dash
                            };
                            this.panelDraw.CreateGraphics().DrawLine(pen, this.PointFromRealToDisplay(((Line)this._listShape[this._listShape.Count - 1])._P1), new Point(e.X, e.Y));
                        }
                        else if (_operatopnType == OperationType.Rectangle)
                        {
                            this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
                            Point point = this.PointFromRealToDisplay(((Rectangle)this._listShape[this._listShape.Count - 1])._P1);
                            Point point2 = new Point
                            {
                                X = (point.X <= e.X) ? point.X : e.X,
                                Y = (point.Y <= e.Y) ? point.Y : e.Y
                            };
                            Pen pen = new Pen(this._drawPenColor, (float)(this._drawPenWidth * this._zoomRatio))
                            {
                                DashStyle = DashStyle.Dash
                            };
                            this.panelDraw.CreateGraphics().DrawRectangle(pen, point2.X, point2.Y, Math.Abs((int)(e.X - point.X)), Math.Abs((int)(e.Y - point.Y)));
                        }
                        else if (_operatopnType == OperationType.Circle)
                        {
                            this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
                            Point point3 = this.PointFromRealToDisplay(((Circle)this._listShape[this._listShape.Count - 1])._PCenter);
                            float num = (float)Math.Sqrt(Math.Pow((double)(e.X - point3.X), 2.0) + Math.Pow((double)(e.Y - point3.Y), 2.0));
                            Pen pen = new Pen(this._drawPenColor, (float)(this._drawPenWidth * this._zoomRatio))
                            {
                                DashStyle = DashStyle.Dash
                            };
                            this.panelDraw.CreateGraphics().DrawLine(pen, point3.X - 3, point3.Y, point3.X + 3, point3.Y);
                            this.panelDraw.CreateGraphics().DrawLine(pen, point3.X, point3.Y - 3, point3.X, point3.Y + 3);
                            this.panelDraw.CreateGraphics().DrawEllipse(pen, (float)(point3.X - num), (float)(point3.Y - num), (float)(2f * num), (float)(2f * num));
                        }
                        else if (_operatopnType == OperationType.Sketch)
                        {
                            Pen pen = new Pen(this._drawPenColor, (float)(this._drawPenWidth * this._zoomRatio))
                            {
                                StartCap = LineCap.Round,
                                EndCap = LineCap.Round
                            };
                            ((Sketch)this._listShape[this._listShape.Count - 1])._pointList.Add(this.PointFromRealToDisplay(new Point(e.X, e.Y)));
                            int count = ((Sketch)this._listShape[this._listShape.Count - 1])._pointList.Count;
                            this.panelDraw.CreateGraphics().DrawLine(pen, this.PointFromRealToDisplay(((Sketch)this._listShape[this._listShape.Count - 1])._pointList[count - 2]), this.PointFromRealToDisplay(((Sketch)this._listShape[this._listShape.Count - 1])._pointList[count - 1]));
                        }
                    }
                }
            }
            else
            {
                if (Math.Abs(e.X - panelDraw.Width) <= 5 && Math.Abs(e.Y - panelDraw.Height) <= 5)
                {
                    this.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    if (Math.Abs(e.X - panelDraw.Width) <= 5 && e.Y < panelDraw.Height)
                    {
                        this.Cursor = Cursors.SizeWE;
                    }
                    else if (Math.Abs(e.Y - panelDraw.Height) <= 5 && e.X < panelDraw.Width)
                    {
                        this.Cursor = Cursors.SizeNS;
                    }
                    else
                    {
                        if (_operatopnType != OperationType.Stop)
                            if (e.X > panelDraw.Width + 5 || e.Y > panelDraw.Height + 5) this.Cursor = Cursors.Default;
                            else this.Cursor = Cursors.Cross;
                        else this.Cursor = Cursors.Default;
                    }
                }
            }
            toolStripStatusLabelMouse.Text = "鼠标:X=" + e.X + ",Y=" + e.Y;
        }

        public void MenuItemWidth_Click(object sender, EventArgs e)
        {
            buttonWidth.Focus();
            DlgPenWidth myDlgPenWidth = new DlgPenWidth();
            myDlgPenWidth.numericUpDownPenWidth.Text = _drawPenWidth.ToString();
            if (myDlgPenWidth.ShowDialog() == DialogResult.OK)
            {
                _drawPenWidth = Convert.ToInt32(myDlgPenWidth.numericUpDownPenWidth.Value);
            }
        }

        public void MenuItemColor_Click(object sender, EventArgs e)
        {
            buttonColor.Focus();
            colorDialog1.Color = _drawPenColor;
            if (colorDialog1.ShowDialog(this) == DialogResult.OK)
            {
                _drawPenColor = colorDialog1.Color;
            }
        }

        public void MenuItemUndo_Click(object sender, EventArgs e)
        {
            if (_operatopnType == OperationType.Line) buttonLine.Focus();
            else if (_operatopnType == OperationType.Rectangle) buttonRegtangle.Focus();
            else if (_operatopnType == OperationType.Circle) buttonCircle.Focus();
            if (_listShape.Count != 0)
            {
                _listShape.RemoveAt(_listShape.Count - 1);
                _bufferGraphics.Clear(Color.White);
                foreach (Shape shape in this._listShape)
                {
                    shape.Draw(this._bufferGraphics, this._zoomRatio);
                }
                panelDraw.CreateGraphics().DrawImage(_bufferBmp, 0, 0);

                if (_listShape.Count == 0) { toolStripUndo.Enabled = false; buttonUndo.Enabled = false; }
                _change--;
                if (_change < _startchange) _startchange = -1;
                if (_change == _startchange) this.Text = _fileName + " - DrawBoard";
                else this.Text = "*" + _fileName + " - DrawBoard";
            }
        }

        private void pxToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _drawPenWidth = 1;
        }

        private void pxToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _drawPenWidth = 2;
        }

        private void pxToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            _drawPenWidth = 3;
        }

        private void pxToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            _drawPenWidth = 4;
        }

        private void pxToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            _drawPenWidth = 5;
        }

        private void pxToolStripMenuItem5_Click(object sender, EventArgs e)
        {
            _drawPenWidth = 6;
        }

        private void ToolStripMenuItemRed_Click(object sender, EventArgs e)
        {
            _drawPenColor = Color.Red;
        }

        private void ToolStripMenuItemYellow_Click(object sender, EventArgs e)
        {
            _drawPenColor = Color.Yellow;
        }

        private void ToolStripMenuItemGreen_Click(object sender, EventArgs e)
        {
            _drawPenColor = Color.Green;
        }

        private void ToolStripMenuItemBlue_Click(object sender, EventArgs e)
        {
            _drawPenColor = Color.Blue;
        }

        private void ToolStripMenuItemBlack_Click(object sender, EventArgs e)
        {
            _drawPenColor = Color.Black;
        }

        public void MenuItemSave_Click(object sender, EventArgs e)
        {
            saveFileDialog1.Title = "保存";
            if (_fileName == "无标题.dwg")
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                    _fileName = saveFileDialog1.FileName;
                else return;
            FileStream fileStream = new FileStream(_fileName, FileMode.Create);
            BinaryWriter binaryWriter = new BinaryWriter(fileStream);
            binaryWriter.Write(_listShape.Count);
            foreach (Shape tempShape in _listShape)
            {
                binaryWriter.Write(tempShape.GetType().ToString());
                tempShape.Write(binaryWriter);
            }
            binaryWriter.Close();
            fileStream.Close();
            //_modifiedFlag = false;
            this.Text = _fileName + " - DrawBoard";
        }

        private void MenuItemNew_Click(object sender, EventArgs e)
        {
            if (this.Text.Substring(0, 1) == "*")
            {
                if (MessageBox.Show("图形已经改变，是否需要保存？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    MenuItemSave_Click(sender, e);
            }
            _scrSaver = false;
            _startchange = 0;
            _change = 0;
            _listShape.Clear();
            _bufferGraphics.Clear(Color.White);
            panelDraw.Width = _bufferBmp.Width;
            panelDraw.Height = _bufferBmp.Height;
            _zoomRatio = 1;
            panelDraw.CreateGraphics().DrawImage(_bufferBmp, 0, 0);
            buttonStop.PerformClick();
            //_operatopnType = OperationType.Stop;
            _drawPenWidth = 10;
            _drawPenColor = Color.Red;
            toolStripStatusLabelInfo.Text = "当前操作:停止";
            this.Cursor = Cursors.Default;
            toolStripUndo.Enabled = false;
            toolStripStop.Enabled = false;
            buttonStop.Enabled = false;
            buttonUndo.Enabled = false;
            _fileName = "无标题.dwg";
            //_modifiedFlag = false;
            this.Text = "无标题.dwg - DrawBoard";
        }

        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                MenuItemNew_Click(sender, e);
                _fileName = openFileDialog1.FileName;
                FileStream fileStream = new FileStream(_fileName, FileMode.Open, FileAccess.Read);
                BinaryReader binaryreader = new BinaryReader(fileStream);
                int shapeCount = binaryreader.ReadInt32();
                for (int i = 0; i < shapeCount; i++)
                {
                    string ShapeType = binaryreader.ReadString();
                    if (ShapeType == "DrawBoard.Line")
                    {
                        Line shape = new Line();
                        shape.Read(binaryreader);
                        _listShape.Add(shape);
                    }
                    else if (ShapeType == "DrawBoard.Rectangle")
                    {
                        Rectangle shape = new Rectangle();
                        shape.Read(binaryreader);
                        _listShape.Add(shape);
                    }
                    else if (ShapeType == "DrawBoard.Circle")
                    {
                        Circle shape = new Circle();
                        shape.Read(binaryreader);
                        _listShape.Add(shape);
                    }
                    else
                    {
                        MessageBox.Show("图元类型错误。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                binaryreader.Close();
                fileStream.Close();
                foreach (Shape tempShape in _listShape)
                    tempShape.Draw(this._bufferGraphics, this._zoomRatio);
                panelDraw.CreateGraphics().DrawImage(_bufferBmp, 0, 0);
                //_modifiedFlag = false;
                _change = _listShape.Count;
                _startchange = _listShape.Count;
                if (_change != 0)
                {
                    buttonUndo.Enabled = true;
                    MenuItemUndo.Enabled = true;
                    toolStripUndo.Enabled = true;
                }
                this.Text = _fileName + " - DrawBoard";
            }
        }

        private void MenuItemClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (Text.Substring(0, 1) == "*")
            {
                DialogResult tempres = MessageBox.Show("图形已经改变，是否需要保存？", "提示", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                if (tempres == DialogResult.Yes)
                {
                    MenuItemSave_Click(sender, e);
                }
                else if (tempres == DialogResult.Cancel) e.Cancel = true;
            }
        }

        public void toolStripSketch_Click(object sender, EventArgs e)
        {
            buttonSketch.Focus();
            _operatopnType = OperationType.Sketch;
            toolStripStatusLabelInfo.Text = "当前操作:徒手画";
            this.Cursor = Cursors.Cross;
            MenuItemLine.CheckState = CheckState.Unchecked;
            MenuItemRectangle.CheckState = CheckState.Unchecked;
            MenuItemCircle.CheckState = CheckState.Unchecked;
            MenuItemStop.CheckState = CheckState.Unchecked;
            MenuItemSketch.CheckState = CheckState.Checked;
            toolStripLine.CheckState = CheckState.Unchecked;
            toolStripRectangle.CheckState = CheckState.Unchecked;
            toolStripCircle.CheckState = CheckState.Unchecked;
            toolStripStop.CheckState = CheckState.Unchecked;
            toolStripSketch.CheckState = CheckState.Checked;
            toolStripStop.Enabled = true;
            buttonStop.Enabled = true;
            toolStripLine.BackColor = Color.White;
            toolStripRectangle.BackColor = Color.White;
            toolStripCircle.BackColor = Color.White;
            toolStripSketch.BackColor = SystemColors.Highlight;
        }

        public void MenuItemSaveAs_Click(object sender, EventArgs e)
        {
            _fileName = "无标题.dwg";
            saveFileDialog1.Title = "另存为";
            string extension;
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                _fileName = saveFileDialog1.FileName;
                extension = System.IO.Path.GetExtension(saveFileDialog1.FileName);
            }
            else return;
            if (extension == ".jpg")
                _bufferBmp.Save(saveFileDialog1.FileName, ImageFormat.Jpeg);
            else if (extension == ".png")
                _bufferBmp.Save(saveFileDialog1.FileName, ImageFormat.Png);
            else if (extension == ".gif")
                _bufferBmp.Save(saveFileDialog1.FileName, ImageFormat.Gif);
            else if (extension == ".bmp")
                _bufferBmp.Save(saveFileDialog1.FileName, ImageFormat.Bmp);
            else if (extension == "dwg")
            {
                FileStream fileStream = new FileStream(_fileName, FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(_listShape.Count);
                foreach (Shape tempShape in _listShape)
                {
                    binaryWriter.Write(tempShape.GetType().ToString());
                    tempShape.Write(binaryWriter);
                }
                binaryWriter.Close();
                fileStream.Close();
                //_modifiedFlag = false;
                this.Text = _fileName + " - DrawBoard";
            }
            else MessageBox.Show("暂不支持该格式。", extension, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void MenuItemBig_Click(object sender, EventArgs e)
        {
            this._zoomRatio *= 1.1;
            this.panelDraw.Width = (int)(this._initialSize.Width * this._zoomRatio);
            this.panelDraw.Height = (int)(this._initialSize.Height * this._zoomRatio);
            this._bufferBmp.Dispose();
            this._bufferBmp = new Bitmap(this.panelDraw.Width, this.panelDraw.Height);
            this._bufferGraphics = Graphics.FromImage(this._bufferBmp);
            this._bufferGraphics.Clear(Color.White);
            this._bufferGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Shape shape in this._listShape)
            {
                shape.Draw(this._bufferGraphics, this._zoomRatio);
            }
            this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);

        }

        private void MenuItemBmpSaize_Click(object sender, EventArgs e)
        {
            DlgBmpSize size = new DlgBmpSize();
            size.numericUpDownWidth.Value=this._initialSize.Width;
            size.numericUpDownHeight.Value = this._initialSize.Height;
            if (size.ShowDialog(this) == DialogResult.OK)
            {
                this._initialSize.Width = (int)size.numericUpDownWidth.Value;
                this._initialSize.Height = (int)size.numericUpDownHeight.Value;
                this._bufferBmp.Dispose();
                this._bufferBmp = new Bitmap((int)(this._initialSize.Width * this._zoomRatio), (int)(this._initialSize.Height * this._zoomRatio));
                this._bufferGraphics = Graphics.FromImage(this._bufferBmp);
                this._bufferGraphics.Clear(Color.White);
                this._bufferGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                foreach (Shape shape in this._listShape)
                {
                    shape.Draw(this._bufferGraphics, this._zoomRatio);
                }
                this.panelDraw.Width = (int)(this._initialSize.Width * this._zoomRatio);
                this.panelDraw.Height = (int)(this._initialSize.Height * this._zoomRatio);
                this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
            }

        }

        private void MenuItemPrtScn_Click(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
            Thread.Sleep(200);
            int width = (int)(Screen.PrimaryScreen.Bounds.Width * 1.0);//缩放150%则为1.5
            int height = (int)(Screen.PrimaryScreen.Bounds.Height * 1.0);
            _scrSaver = true;
            _bufferBmp.Dispose();
            _buffsavescr.Dispose();
            _bufferBmp = new Bitmap(width, height);
            _buffsavescr = new Bitmap(width, height);
            _bufferGraphics = Graphics.FromImage(_bufferBmp);
            _buffsavescr2 = Graphics.FromImage(_buffsavescr);
            _bufferGraphics.Clear(Color.White);
            _buffsavescr2.Clear(Color.White);
            _bufferGraphics.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
            _buffsavescr2.CopyFromScreen(0, 0, 0, 0, new Size(width, height));
            foreach (Shape tempShape in _listShape) tempShape.Draw(this._bufferGraphics, this._zoomRatio); ;
            panelDraw.Width = (int)(_bufferBmp.Width * _zoomRatio);
            panelDraw.Height = (int)(_bufferBmp.Height * _zoomRatio);
            System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, (int)(_bufferBmp.Width * _zoomRatio), (int)(_bufferBmp.Height * _zoomRatio));
            System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, _bufferBmp.Width, _bufferBmp.Height);
            panelDraw.CreateGraphics().DrawImage(_bufferBmp, destRect, srcRect, GraphicsUnit.Pixel);
            this.WindowState = FormWindowState.Maximized;
        }

        private void MenuItemSCRPen_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.None)
            {
                _zoomRatio = 1;
                flowLayoutPanel1.Visible = false;
                menuStrip1.Visible = false;
                toolStrip1.Visible = false;
                statusStrip1.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                MenuItemPrtScn_Click(null, null);
                toolStripSketch_Click(null, null);
                DlgDrawTool myDlgDrawTool = new DlgDrawTool();
                myDlgDrawTool._formMain = this;
                myDlgDrawTool.Show();
            }
        }

        private void MenuItemFullSCR_Click(object sender, EventArgs e)
        {
            if (this.FormBorderStyle != FormBorderStyle.None)
            {
                _zoomRatio = 1;
                flowLayoutPanel1.Visible = false;
                menuStrip1.Visible = false;
                toolStrip1.Visible = false;
                statusStrip1.Visible = false;
                this.FormBorderStyle = FormBorderStyle.None;
                this.WindowState = FormWindowState.Minimized;
                Thread.Sleep(200);
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;
                _scrSaver = true;
                _bufferBmp.Dispose();
                _bufferBmp = new Bitmap(width, height);
                _bufferGraphics = Graphics.FromImage(_bufferBmp);
                _bufferGraphics.Clear(Color.White);
                foreach (Shape tempShape in _listShape) tempShape.Draw(this._bufferGraphics, this._zoomRatio); ;
                panelDraw.Width = (int)(_bufferBmp.Width * _zoomRatio);
                panelDraw.Height = (int)(_bufferBmp.Height * _zoomRatio);
                System.Drawing.Rectangle destRect = new System.Drawing.Rectangle(0, 0, (int)(_bufferBmp.Width * _zoomRatio), (int)(_bufferBmp.Height * _zoomRatio));
                System.Drawing.Rectangle srcRect = new System.Drawing.Rectangle(0, 0, _bufferBmp.Width, _bufferBmp.Height);
                panelDraw.CreateGraphics().DrawImage(_bufferBmp, destRect, srcRect, GraphicsUnit.Pixel);
                this.WindowState = FormWindowState.Maximized;
                toolStripSketch_Click(null, null);
                DlgDrawTool myDlgDrawTool = new DlgDrawTool();
                myDlgDrawTool._formMain = this;
                myDlgDrawTool.Show();
            }
        }

        private void panelContainer_MouseUp(object sender, MouseEventArgs e)
        {
            System.Drawing.Rectangle destRect;
            System.Drawing.Rectangle srcRect;
            if (_reSize)
            {
                //resize
                if ((int)(panelDraw.Width / _zoomRatio) != _bufferBmp.Width || (int)(panelDraw.Height / _zoomRatio) != _bufferBmp.Height)
                {
                    //int width = (int)(panelDraw.Width / _zoomRatio);
                    //int height = (int)(panelDraw.Height / _zoomRatio);
                    this._initialSize.Width = panelDraw.Width;
                    this._initialSize.Height = panelDraw.Height;
                    this._bufferBmp.Dispose();
                    this._bufferBmp = new Bitmap((int)(this._initialSize.Width * this._zoomRatio), (int)(this._initialSize.Height * this._zoomRatio));
                    this._bufferGraphics = Graphics.FromImage(this._bufferBmp);
                    this._bufferGraphics.Clear(Color.White);
                    this._bufferGraphics.SmoothingMode = SmoothingMode.AntiAlias;
                    foreach (Shape shape in this._listShape)
                    {
                        shape.Draw(this._bufferGraphics, this._zoomRatio);
                    }
                    //this.panelDraw.Width = (int)(this._initialSize.Width * this._zoomRatio);
                    //this.panelDraw.Height = (int)(this._initialSize.Height * this._zoomRatio);
                    this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
                }
                _reSize = false;
            }
            if (this.Cursor != Cursors.SizeNS && this.Cursor != Cursors.SizeNWSE && this.Cursor != Cursors.SizeWE && _drawing == true)
            {
                if (_operatopnType == OperationType.Line)
                {
                    _listShape[_listShape.Count - 1]._PenWidth = _drawPenWidth;
                    _listShape[_listShape.Count - 1]._PenColor = _drawPenColor;
                    ((Line)_listShape[_listShape.Count - 1])._P2 = PointFromDisplayToReal(new Point(e.X, e.Y));
                    _listShape[_listShape.Count - 1].Draw(this._bufferGraphics, this._zoomRatio);
                    _change++;
                    this.Text = "*" + _fileName + " - DrawBoard";
                    _drawing = false;
                }
                else if (_operatopnType == OperationType.Rectangle)
                {
                    _listShape[_listShape.Count - 1]._PenWidth = _drawPenWidth;
                    _listShape[_listShape.Count - 1]._PenColor = _drawPenColor;
                    ((Rectangle)_listShape[_listShape.Count - 1])._P2 = PointFromDisplayToReal(new Point(e.X, e.Y));
                    _listShape[_listShape.Count - 1].Draw(this._bufferGraphics, this._zoomRatio);
                    _change++;
                    this.Text = "*" + _fileName + " - DrawBoard";
                    _drawing = false;
                }
                else if (_operatopnType == OperationType.Circle)
                {
                    _listShape[_listShape.Count - 1]._PenWidth = _drawPenWidth;
                    _listShape[_listShape.Count - 1]._PenColor = _drawPenColor;
                    Point center = ((Circle)_listShape[_listShape.Count - 1])._PCenter;
                    ((Circle)_listShape[_listShape.Count - 1])._R = (float)Math.Sqrt(Math.Pow(e.X / _zoomRatio - center.X, 2) + Math.Pow(e.Y / _zoomRatio - center.Y, 2));
                    _listShape[_listShape.Count - 1].Draw(this._bufferGraphics, this._zoomRatio);
                    _change++;
                    this.Text = "*" + _fileName + " - DrawBoard";
                    _drawing = false;
                }
                else if (_operatopnType == OperationType.Sketch)
                {
                    this._listShape[this._listShape.Count - 1]._PenWidth = this._drawPenWidth;
                    this._listShape[this._listShape.Count - 1]._PenColor = this._drawPenColor;
                    ((Sketch)this._listShape[this._listShape.Count - 1])._pointList.Add(this.PointFromDisplayToReal(new Point(e.X, e.Y)));
                    this._listShape[this._listShape.Count - 1].Draw(this._bufferGraphics, this._zoomRatio);
                    _change++;
                    this.Text = "*" + _fileName + " - DrawBoard";
                    _drawing = false;
                }
                this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);
                Pen myPen = new Pen(_drawPenColor, _drawPenWidth);
                if (_listShape.Count != 0) { toolStripUndo.Enabled = true; buttonUndo.Enabled = true; }
            }
        }

        private void toolStripStatusLabel1_Click(object sender, EventArgs e)
        {

        }

        private void timerMouse_Tick(object sender, EventArgs e)
        {
            toolStripStatusMsg.Visible = false;
            timerMouse.Enabled = false;
        }

        private void MenuItemSmall_Click(object sender, EventArgs e)
        {
            this._zoomRatio *= 0.9;
            this.panelDraw.Width = (int)(this._initialSize.Width * this._zoomRatio);
            this.panelDraw.Height = (int)(this._initialSize.Height * this._zoomRatio);
            this._bufferBmp.Dispose();
            this._bufferBmp = new Bitmap(this.panelDraw.Width, this.panelDraw.Height);
            this._bufferGraphics = Graphics.FromImage(this._bufferBmp);
            this._bufferGraphics.Clear(Color.White);
            this._bufferGraphics.SmoothingMode = SmoothingMode.AntiAlias;
            foreach (Shape shape in this._listShape)
            {
                shape.Draw(this._bufferGraphics, this._zoomRatio);
            }
            this.panelDraw.CreateGraphics().DrawImage(this._bufferBmp, 0, 0);

        }
    }
}
