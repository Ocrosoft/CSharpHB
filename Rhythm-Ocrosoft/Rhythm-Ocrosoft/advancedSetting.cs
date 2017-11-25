using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//Moudle 4,高级设置,按键的下落速度
namespace Rhythm_Ocrosoft
{
    public partial class advancedSetting : Form
    {
        public advancedSetting()
        {
            InitializeComponent();
            Height = (int)(768 * _zoomRate);
            Width = (int)(1024 * _zoomRate);
        }
        private int _speed = 10;
        Font myFont= new Font("Comic Sans MS", (float)(30 * _zoomRate), FontStyle.Bold);
        Font myFontSpeed = new Font("Comic Sans MS", (float)(50 * _zoomRate), FontStyle.Bold);
        SolidBrush fontBrush = new SolidBrush(Color.White);
        Bitmap backgroundImg = new Bitmap("Resources/BackGround-Blue.png");//背景
        Bitmap EndLine = new Bitmap("Resources/EndLine.png");//结束线
        Bitmap add = new Bitmap("Resources/Add.png");
        Bitmap sub = new Bitmap("Resources/Sub.png");
        private static double _zoomRate = FormMain.ZoomRate / 2.0;

        public int Speed
        {
            get
            {
                return _speed;
            }

            set
            {
                _speed = value;
            }
        }

        //显示错误
        private void showError(string error)
        {
            try
            {
                MessageBox.Show(null, error + ",请联系Ocrosoft@Ocrosoft.com", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show(null, "错误显示失败,请联系Ocrosoft@Ocrosoft.com", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //0X4001
        private void draw(Graphics g)
        {
            try
            {
                g.DrawImage(backgroundImg, 0, 0, (int)(1024 * _zoomRate), (int)(768 * _zoomRate));
                g.DrawImage(EndLine, 0, (int)(690 * _zoomRate), (int)(1024 * _zoomRate), (int)(20 * _zoomRate));
                g.DrawImage(sub, (int)(0 * _zoomRate), (int)(300 * _zoomRate), (int)(add.Size.Width * _zoomRate), (int)(add.Size.Height * _zoomRate));
                g.DrawImage(add, (int)(900 * _zoomRate), (int)(300 * _zoomRate), (int)(add.Size.Width * _zoomRate), (int)(add.Size.Height * _zoomRate));
                g.DrawString("CALIBRATE", myFont, fontBrush, (int)(400 * _zoomRate), (int)(200 * _zoomRate));
                g.DrawString("Comfirm", myFont, fontBrush, (int)(440 * _zoomRate), (int)(600 * _zoomRate));
                g.DrawString(Speed<10?"0"+Speed.ToString():Speed.ToString(), myFontSpeed, fontBrush, (int)(480 * _zoomRate), (int)(300 * _zoomRate)); }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4001，ErrorInfo：" + exc);
            }
        }
        //0X4003
        private void AdvancedSetting_Load(object sender, EventArgs e)
        {
            try
            {
                pictureBox1.Invalidate();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4003，ErrorInfo：" + exc);
            }
        }
        //0X4004
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            try
            { draw(e.Graphics); }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4004，ErrorInfo：" + exc);
            }
        }
        //判断一个点是否在圆内0X4005
        private bool judgeMouseInCircle(int mouseX, int MouseY, Point circle, double r)
        {
            try
            {
                circle.X += (int)r;
                circle.Y += (int)r;
                if ((MouseY - circle.Y) * (MouseY - circle.Y) + (mouseX - circle.X) * (mouseX - circle.X) <= r * r)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4005，ErrorInfo：" + exc);
                return false;
            }
        }
        //0X4010
        private bool judgeMouseInRectangle(int mouseX, int MouseY, Point rectangle, int width, int height)
        {
            try
            {
                if (mouseX > rectangle.X && mouseX < rectangle.X + width && MouseY > rectangle.Y && MouseY < rectangle.Y + height)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4010，ErrorInfo：" + exc);
                return false;
            }
        }
        //0X4009
        private void advancedSetting_MouseClick(object sender, MouseEventArgs e)
        {
            try
            { if (judgeMouseInCircle(e.X, e.Y, new Point((int)(900 * _zoomRate), (int)(300 * _zoomRate)), 45 * _zoomRate))
                {
                    Speed++;
                    pictureBox1.Invalidate();
                }
                else if (judgeMouseInCircle(e.X, e.Y, new Point((int)(0 + 50 * _zoomRate), (int)(300 * _zoomRate)), 45 * _zoomRate))
                {
                    Speed--;
                    pictureBox1.Invalidate();
                }
                else if (judgeMouseInRectangle(e.X, e.Y, new Point((int)(440 * _zoomRate), (int)(600 * _zoomRate)), (int)(180 * _zoomRate), (int)(50 * _zoomRate)))
                {
                    Close();
                } }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4009，ErrorInfo：" + exc);
            }
        }
        //0X4011
        private void advancedSetting_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                if (judgeMouseInCircle(e.X,e.Y, new Point((int)(900 * _zoomRate), (int)(300 * _zoomRate)),45*_zoomRate))
                {
                    pictureBox1.Cursor = Cursors.Hand;
                }
                else if(judgeMouseInCircle(e.X, e.Y, new Point((int)(0+50 * _zoomRate), (int)(300 * _zoomRate)), 45 * _zoomRate))
                {
                    pictureBox1.Cursor = Cursors.Hand;
                }
                else if(judgeMouseInRectangle(e.X,e.Y,new Point((int)(440 * _zoomRate), (int)(600 * _zoomRate)),(int)(180*_zoomRate),(int)(50*_zoomRate)))
                {
                    pictureBox1.Cursor = Cursors.Hand;
                }
                else
                {
                    pictureBox1.Cursor = Cursors.Default;
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4011，ErrorInfo：" + exc);
            }
        }
        //0X4012
        private void advancedSetting_FormClosed(object sender, FormClosedEventArgs e)
        {
            try
            { DialogResult = DialogResult.OK; }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X4012，ErrorInfo：" + exc);
            }
        }
    }
}
