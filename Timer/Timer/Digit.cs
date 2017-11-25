using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Resources;
using System.Text;
using System.Windows.Forms;

namespace Timer
{
    public partial class Digit : Form
    {
        private int _nowSecond = 0;
        private int _colbilink = 0;//等于4的时候冒号可视状态改变
        private Boolean _colVisible = true;
        private Boolean _dotShow = true;
        //ResourceManager rm = new ResourceManager(Digit., Assembly.GetExecutingAssembly());
        private int _input;
        public Digit()
        {
            InitializeComponent();
        }

        Image change(int n)
        {
            if (n == 0) return Properties.Resources.zero;
            if (n == 1) return Properties.Resources.one;
            if (n == 2) return Properties.Resources.two;
            if (n == 3) return Properties.Resources.three;
            if (n == 4) return Properties.Resources.four;
            if (n == 5) return Properties.Resources.five;
            if (n == 6) return Properties.Resources.six;
            if (n == 7) return Properties.Resources.seven;
            if (n == 8) return Properties.Resources.eight;
            if (n == 9) return Properties.Resources.nine;
            return Properties.Resources.one;
        }
        private void buttonStart_Click(object sender, EventArgs e)
        {
            this.AcceptButton = buttonStop;
            if (textBox1.Text.Length < 9)
                _input = Convert.ToInt32(textBox1.Text) * 10;
            if (textBox1.Text.Length >= 9)
            {
                MessageBox.Show(this, "别闹了=_=", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                if (_input > 3564000)
                {
                    int hour = _input / 36000;
                    MessageBox.Show(this, "你打算" + (int)((double)hour / 24) + "天不睡觉了吗？", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    if (_input == 0)//计时
                    {
                        pictureBoxHour1.Image = change(0);
                        pictureBoxHour2.Image = change(0);
                        pictureBoxMin1.Image = change(0);
                        pictureBoxMin2.Image = change(0);
                        pictureBoxSec1.Image = change(0);
                        pictureBoxSec2.Image = change(0);
                        pictureBoxMs.Image = change(0);
                    }
                    else
                    {
                        _nowSecond = _input;
                        int hour = _nowSecond / 36000;
                        int minute = (_nowSecond - hour * 36000) / 600;
                        double second = ((double)_nowSecond % 600) / 10.0;
                        if (hour <= 9)
                        {
                            pictureBoxHour1.Image = change(0);
                            pictureBoxHour2.Image = change(hour);
                        }
                        else
                        {
                            pictureBoxHour1.Image = change(hour / 10 % 10);
                            pictureBoxHour2.Image = change(hour % 10);
                        }
                        if (minute <= 9)
                        {
                            pictureBoxMin1.Image = change(0);
                            pictureBoxMin2.Image = change(minute % 10);
                        }
                        else
                        {
                            pictureBoxMin1.Image = change(minute / 10 % 10);
                            pictureBoxMin2.Image = change(minute % 10);
                        }
                        if (second < 10)
                        {
                            pictureBoxSec1.Image = change(0);
                            pictureBoxSec2.Image = change((int)second % 10);
                            pictureBoxMs.Image = change((int)(second * 10 % 10));
                        }
                        else
                        {
                            pictureBoxSec1.Image = change(((int)second / 10 % 10));
                            pictureBoxSec2.Image = change((int)second % 10);
                            pictureBoxMs.Image = change((int)(second * 10 % 10));
                        }
                    }
                    pictureBoxCol1.Image = Properties.Resources.dot2;
                    pictureBoxCol2.Image = Properties.Resources.dot2;
                    buttonStart.Enabled = false;
                    textBox1.Enabled = false;
                    buttonPause.Enabled = true;
                    buttonStop.Enabled = true;
                    buttonPause.Text = "暂停";
                    timer1.Enabled = true;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (_input == 0) _nowSecond++;
            else _nowSecond--;
            int hour = _nowSecond / 36000;
            int minute = (_nowSecond - hour * 36000) / 600;
            double second = ((double)_nowSecond % 600) / 10.0;
            if (hour <= 9)
            {
                pictureBoxHour1.Image = change(0);
                pictureBoxHour2.Image = change(hour);
            }
            else
            {
                pictureBoxHour1.Image = change(hour / 10 % 10);
                pictureBoxHour2.Image = change(hour % 10);
            }
            if (minute <= 9)
            {
                pictureBoxMin1.Image = change(0);
                pictureBoxMin2.Image = change(minute % 10);
            }
            else
            {
                pictureBoxMin1.Image = change(minute / 10 % 10);
                pictureBoxMin2.Image = change(minute % 10);
            }
            if (second < 10)
            {
                pictureBoxSec1.Image = change(0);
                pictureBoxSec2.Image = change((int)second % 10);
                pictureBoxMs.Image = change((int)(second * 10 % 10));
            }
            else
            {
                pictureBoxSec1.Image = change(((int)second / 10 % 10));
                pictureBoxSec2.Image = change((int)second % 10);
                pictureBoxMs.Image = change((int)(second * 10 % 10));
            }
            if (_colbilink == 4)//在同一个timer里处理冒号闪烁可以防止节奏混乱
            {
                if (_dotShow)
                {
                    pictureBoxCol1.Image = Properties.Resources.blank;
                    pictureBoxCol2.Image = Properties.Resources.blank;
                }
                else
                {
                    pictureBoxCol1.Image = Properties.Resources.dot2;
                    pictureBoxCol2.Image = Properties.Resources.dot2;
                }
                _dotShow = !_dotShow;
                _colbilink = 0;
            }
            else _colbilink++;
            if (_nowSecond == 0)
            {
                timer1.Enabled = false;
                MessageBox.Show(this, "倒计时结束", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                buttonStop.PerformClick();
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (buttonPause.Text == "暂停")
            {
                timer1.Enabled = false;
                buttonPause.Text = "继续";
                if (_dotShow)//保存此时冒号的状态
                    _colVisible = true;
                else
                {
                    _colVisible = false;
                    pictureBoxCol1.Image = Properties.Resources.dot2;//显示冒号,否则不美观
                    pictureBoxCol2.Image = Properties.Resources.dot2;
                }
            }
            else
            {
                timer1.Enabled = true;
                buttonPause.Text = "暂停";
                if (!_colVisible)//恢复冒号状态
                {
                    pictureBoxCol1.Image = Properties.Resources.blank;
                    pictureBoxCol2.Image = Properties.Resources.blank;
                }
            }
        }

        private void buttonStop_Click(object sender, EventArgs e)
        {
            this.AcceptButton = buttonStart;
            _nowSecond = 0;
            _colbilink = 0;
            timer1.Enabled = false;
            textBox1.Enabled = true;
            buttonStart.Enabled = true;
            buttonPause.Enabled = false;
            buttonStop.Enabled = false;
            buttonPause.Text = "暂停";
            pictureBoxCol1.Image = Properties.Resources.dot2;
            pictureBoxCol2.Image = Properties.Resources.dot2;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                textBox1.Text = "0";
                textBox1.SelectionStart = textBox1.Text.Length;
            }
            else
            {
                if (textBox1.Text.Length < 9)
                    textBox1.Text = Convert.ToInt32(textBox1.Text).ToString(); }
            if (textBox1.Text.Length == 1)
                textBox1.SelectionStart = textBox1.Text.Length;
            if (textBox1.Text.Length < 9)
            {
                if (Convert.ToInt32(textBox1.Text) == 0)
                {
                    label1.Text = "          秒表";
                }
                else label1.Text = "秒    倒计时";
            }
            if (textBox1.Text.Length < 9)
                _input = Convert.ToInt32(textBox1.Text) * 10;
            _nowSecond = _input;
            if (textBox1.Text.Length >= 9)
            {
                //MessageBox.Show(this, "别闹了=_=", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                pictureBoxHour1.Image = change(0);
                pictureBoxHour2.Image = change(0);
                pictureBoxMin1.Image = change(0);
                pictureBoxMin2.Image = change(0);
                pictureBoxSec1.Image = change(0);
                pictureBoxSec2.Image = change(0);
                pictureBoxMs.Image = change(0);
            }
            else
            {
                if (_input == 0 || _input > 3564000)//计时
                {
                    pictureBoxHour1.Image = change(0);
                    pictureBoxHour2.Image = change(0);
                    pictureBoxMin1.Image = change(0);
                    pictureBoxMin2.Image = change(0);
                    pictureBoxSec1.Image = change(0);
                    pictureBoxSec2.Image = change(0);
                    pictureBoxMs.Image = change(0);
                }
                else
                {
                    _nowSecond = _input;
                    int hour = _nowSecond / 36000;
                    int minute = (_nowSecond - hour * 36000) / 600;
                    double second = ((double)_nowSecond % 600) / 10.0;
                    if (hour <= 9)
                    {
                        pictureBoxHour1.Image = change(0);
                        pictureBoxHour2.Image = change(hour);
                    }
                    else
                    {
                        pictureBoxHour1.Image = change(hour / 10 % 10);
                        pictureBoxHour2.Image = change(hour % 10);
                    }
                    if (minute <= 9)
                    {
                        pictureBoxMin1.Image = change(0);
                        pictureBoxMin2.Image = change(minute % 10);
                    }
                    else
                    {
                        pictureBoxMin1.Image = change(minute / 10 % 10);
                        pictureBoxMin2.Image = change(minute % 10);
                    }
                    if (second < 10)
                    {
                        pictureBoxSec1.Image = change(0);
                        pictureBoxSec2.Image = change((int)second % 10);
                        pictureBoxMs.Image = change((int)(second * 10 % 10));
                    }
                    else
                    {
                        pictureBoxSec1.Image = change(((int)second / 10 % 10));
                        pictureBoxSec2.Image = change((int)second % 10);
                        pictureBoxMs.Image = change((int)(second * 10 % 10));
                    }
                }
            }
        }
    }
}
