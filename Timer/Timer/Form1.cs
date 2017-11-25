using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Timer
{
    public partial class Form1 : Form
    {
        private int _nowSecond = 0;
        private int _colbilink = 0;//等于4的时候冒号可视状态改变
        private Boolean _colVisible = true;
        private int _input;
        public Form1()
        {
            InitializeComponent();
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
                        labelHour.Text = "00";
                        labelMin.Text = "00";
                        labelSec.Text = "00.0";
                    }
                    else
                    {
                        _nowSecond = _input;
                        int hour = _nowSecond / 36000;
                        int minute = (_nowSecond - hour * 36000) / 600;
                        double second = ((double)_nowSecond % 600) / 10.0;
                        if (hour <= 9)
                            labelHour.Text = "0" + hour.ToString();
                        else labelHour.Text = hour.ToString();
                        if (minute <= 9)
                            labelMin.Text = "0" + minute.ToString();
                        else labelMin.Text = minute.ToString();
                        if (second < 10)
                        {
                            if ((int)second == second)
                                labelSec.Text = "0" + second.ToString() + ".0";
                            else labelSec.Text = "0" + second.ToString();
                        }
                        else
                        {
                            if ((int)second == second)
                                labelSec.Text = second.ToString() + ".0";
                            else labelSec.Text = second.ToString();
                        }
                    }
                    labelCol1.Visible = true;
                    labelCol2.Visible = true;
                    buttonStart.Enabled = false;
                    textBox1.Enabled = false;
                    buttonPause.Enabled = true;
                    buttonStop.Enabled = true;
                    buttonPause.Text = "暂停";
                    timer1.Enabled = true;
                }
            }
        }

        private void buttonPause_Click(object sender, EventArgs e)
        {
            if (buttonPause.Text == "暂停")
            {
                timer1.Enabled = false;
                buttonPause.Text = "继续";
                if (labelCol1.Visible)//保存此时冒号的状态
                    _colVisible = true;
                else
                {
                    _colVisible = false;
                    labelCol1.Visible = true;//显示冒号,否则不美观
                    labelCol2.Visible = true;
                }
            }
            else
            {
                timer1.Enabled = true;
                buttonPause.Text = "暂停";
                if(!_colVisible)//恢复冒号状态
                {
                    labelCol1.Visible = false ;
                    labelCol2.Visible = false;
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
                labelHour.Text = "0" + hour.ToString();
            else labelHour.Text = hour.ToString();
            if (minute <= 9)
                labelMin.Text = "0" + minute.ToString();
            else labelMin.Text = minute.ToString();
            if (second < 10)
            {
                if ((int)second == second)
                    labelSec.Text = "0" + second.ToString() + ".0";
                else labelSec.Text = "0" + second.ToString();
            }
            else
            {
                if ((int)second == second)//处理没有毫秒时的显示
                    labelSec.Text =second.ToString() + ".0";
                else labelSec.Text = second.ToString();
            }
            if (_colbilink == 4)//在同一个timer里处理冒号闪烁可以防止节奏混乱
            {
                if (labelCol1.Visible)
                {
                    labelCol1.Visible = false;
                    labelCol2.Visible = false;
                }
                else
                {
                    labelCol1.Visible = true;
                    labelCol2.Visible = true;
                }
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
            labelCol1.Visible = true;
            labelCol2.Visible = true;
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
                textBox1.Text = Convert.ToInt32(textBox1.Text).ToString();
            }
            if(textBox1.Text.Length==1)
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
            if (textBox1.Text.Length >= 9)
            {
                //MessageBox.Show(this, "别闹了=_=", "警告", MessageBoxButtons.OK, MessageBoxIcon.Error);
                labelHour.Text = "00";
                labelMin.Text = "00";
                labelSec.Text = "00.0";
            }
            else
            {
                if (_input == 0 || _input > 3564000)//计时
                {
                    labelHour.Text = "00";
                    labelMin.Text = "00";
                    labelSec.Text = "00.0";
                }
                else
                {
                    _nowSecond = _input;
                    int hour = _nowSecond / 36000;
                    int minute = (_nowSecond - hour * 36000) / 600;
                    double second = ((double)_nowSecond % 600) / 10.0;
                    if (hour <= 9)
                        labelHour.Text = "0" + hour.ToString();
                    else labelHour.Text = hour.ToString();
                    if (minute <= 9)
                        labelMin.Text = "0" + minute.ToString();
                    else labelMin.Text = minute.ToString();
                    if (second < 10)
                    {
                        if ((int)second == second)
                            labelSec.Text = "0" + second.ToString() + ".0";
                        else labelSec.Text = "0" + second.ToString();
                    }
                    else
                    {
                        if ((int)second == second)
                            labelSec.Text = second.ToString() + ".0";
                        else labelSec.Text = second.ToString();
                    }
                }
            }
        }
        Digit mydigit = new Digit();
        private void label1_DoubleClick(object sender, EventArgs e)
        {
            if (mydigit.IsDisposed)
            {
                mydigit = new Digit();
            }
            mydigit.Show();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            MessageBox.Show(this, "Copyright©ACMSaga", "关于");
        }
    }
}
