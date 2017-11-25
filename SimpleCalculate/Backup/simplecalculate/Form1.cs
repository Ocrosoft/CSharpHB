using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace simplecalculate
{
    public partial class Form1 : Form
    {
        private decimal _num1 = 0, _num2 = 0, _result = 0;
        private string _operator = "#", _lastoperator = "#";
        private bool _calfi = true, _firstoperator = false, _secalfi = true;
        private int _flag = 0;//16,10,2

        private void button19_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (label2.Text == "") label2.Text = "0";
                if (Convert.ToDouble(label2.Text) != 0)
                {
                    if (_calfi == true)
                    {
                        label2.Text = "0";
                        _calfi = false;
                        _firstoperator = true;
                    }
                    if (_secalfi == true)
                    {
                        label2.Text = "0";
                        _secalfi = false;
                    }
                    if (label2.Text == "0") label2.Text = "0";
                    else label2.Text = label2.Text + "0";
                }
            }
            button21.Focus();
        }

        private void button16_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "1";
                else label2.Text = label2.Text + "1";
            }
            button21.Focus();
        }

        private void button15_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "2";
                else label2.Text = label2.Text + "2";
            }
            button21.Focus();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "3";
                else label2.Text = label2.Text + "3";
            }
            button21.Focus();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "4";
                else label2.Text = label2.Text + "4";
            }
            button21.Focus();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "5";
                else label2.Text = label2.Text + "5";
            }
            button21.Focus();
        }

        private void button10_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "6";
                else label2.Text = label2.Text + "6";
            }
            button21.Focus();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (label2.Text.Length < 32)
                {
                    if (_calfi == true)
                    {
                        label2.Text = "0";
                        _calfi = false;
                        _firstoperator = true;
                    }
                    if (_secalfi == true)
                    {
                        label2.Text = "0";
                        _secalfi = false;
                    }
                    if (label2.Text == "0") label2.Text = "7";
                    else label2.Text = label2.Text + "7";
                }
                button21.Focus();
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19||label2.Text.IndexOf("E")!=-1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "8";
                else label2.Text = label2.Text + "8";
            }
            button21.Focus();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (label2.Text.Length < 19 || label2.Text.IndexOf("E") != -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                if (label2.Text == "0") label2.Text = "9";
                else label2.Text = label2.Text + "9";
            }
            button21.Focus();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            label2.Text = "0";
            button21.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            label1.Text = "";
            label2.Text = "0";
            _firstoperator = false;
            _calfi = true;
            _operator = "#";
            _secalfi = true;
            _num1 = 0; _num2 = 0;
            button21.Focus();
        }

        private void button18_Click(object sender, EventArgs e)
        {
            if (label2.Text.IndexOf(".") == -1)
            {
                if (_calfi == true)
                {
                    label2.Text = "0";
                    _calfi = false;
                    _firstoperator = true;
                }
                if (_secalfi == true)
                {
                    label2.Text = "0";
                    _secalfi = false;
                }
                label2.Text = label2.Text + ".";
            }
            button21.Focus();
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            if (_flag == 2)
            {
                if (label2.Text != "")
                {
                    _num1 = Convert.ToDecimal(label2.Text);
                    label1.Text = "negate(" + _num1 + ")=";
                    label2.Text = Convert.ToString(-_num1);
                }
            }
            else MessageBox.Show("该操作合法，但由于不理解如何操作，不允许此行为。\n带来不便，敬请谅解。");
            button21.Focus();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (label2.Text != "非数字" || label2.Text != "暂不允许异号连算")
            {
                if (label2.Text.Length <= 1) label2.Text = "0";
                else label2.Text = label2.Text.Substring(0, label2.Text.Length - 1);
                button21.Focus();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            if (label2.Text != "" )
            {
                if (_operator != "/" && _operator != "#") label2.Text = "暂不允许异号连算";
                else
                {
                    if (_calfi == false && _firstoperator == false)
                    {
                        label1.Text = label1.Text + label2.Text + " ÷ ";
                        _num2 = Convert.ToDecimal(label2.Text);
                        _num1 = _num1 / _num2;
                        label2.Text = Convert.ToString(_num1);
                        _secalfi = true;
                    }
                    else
                    {
                        label1.Text = label2.Text + " ÷ ";
                        _operator = "/";
                        if (_flag == 3)
                            _num1 = Convert.ToInt32(label2.Text, 2);
                        else _num1 = Convert.ToDecimal(label2.Text);
                        _num2 = _num1;
                        label2.Text = "";
                        _firstoperator = false;
                    }
                }
            }
            else
            {
                if (label1.Text != "")
                {
                    label1.Text = label1.Text.Substring(0, label1.Text.Length - 2);
                    label1.Text = label1.Text + "÷ ";
                    _operator = "/";
                }
            }
            button21.Focus();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            if (label2.Text != "")
            {
                if (_operator != "*" && _operator != "#") label2.Text = "暂不允许异号连算";
                else
                {
                    if (_calfi == false && _firstoperator == false)
                    {
                        label1.Text = label1.Text + label2.Text + " × ";
                        _num2 = Convert.ToDecimal(label2.Text);
                        _num1 = _num1 * _num2;
                        label2.Text = Convert.ToString(_num1);
                        _secalfi = true;
                    }
                    else
                    {
                        label1.Text = label2.Text + " × ";
                        _operator = "*";
                        if (_flag == 3)
                            _num1 = Convert.ToInt32(label2.Text, 2);
                        else _num1 = Convert.ToDecimal(label2.Text);
                        _num2 = _num1;
                        label2.Text = "";
                        _firstoperator = false;
                    }
                }
            }
            else
            {
                if (label1.Text != "")
                {
                    label1.Text = label1.Text.Substring(0, label1.Text.Length - 2);
                    label1.Text = label1.Text + "× ";
                    _operator = "*";
                }
            }
            button21.Focus();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            //button21.Focus();
            //label1.Text = this.Width.ToString();
            if (Width <= 450)
            {
                button3.Font = button35.Font;
                button20.Font = button35.Font;
            }
            else if (Width <= 500)
            {
                button3.Font = button37.Font;
                button20.Font = button37.Font;
            }
            else
            {
                button3.Font = button25.Font;
                button20.Font = button25.Font;
            }
            int width = Size.Width - 15;//边框
            int height = Size.Height - 38;

            //21->30<->435->888
            int _fontsize = (Width - 435) * (30 - 20) / (888 - 322) + 22;
            int _fontsize1 = (Width - 435) * (18 - 9) / (888 - 322) + 9;
            if (_fontsize >= 30)
            {
                label11.Font = new Font("微软雅黑", 30);
                label1.Font = new Font("微软雅黑", 18);
            }
            else
            {
                label11.Font = new Font("微软雅黑", _fontsize);
                label1.Font = new Font("微软雅黑", _fontsize1);
            }
            label1.Size = new Size(width, height / 10);
            label1.Location = new Point(0, 0);
            label2.Size = new Size(width, height / 7);
            label2.Location = new Point(0, label1.Size.Height);
            label11.Size = label2.Size;
            label11.Location = label2.Location;
            button22.Size = new Size(width, label2.Height / 3);
            button22.Location = new Point(0, label2.Location.Y + label2.Height);
            button23.Size = new Size(width, label2.Height / 3);
            button23.Location = new Point(0, button22.Location.Y + button22.Height);
            button24.Size = new Size(width, label2.Height / 3);
            button24.Location = new Point(0, button23.Location.Y + button23.Height);
            //label20.Location = new Point(0, button24.Location.Y + button24.Height);

            int buttonheight = (height - (label1.Height + label2.Height + button22.Height * 3)) / 5;
            button36.Size = new Size(width / 3, buttonheight);
            button36.Location = new Point(0, button24.Location.Y + button24.Height);
            button1.Size = new Size(width / 6, buttonheight);
            button1.Location = new Point(button36.Size.Width + button36.Location.X, button36.Location.Y);
            button2.Size = new Size(width / 6, buttonheight);
            button2.Location = new Point(button1.Size.Width + button1.Location.X, button1.Location.Y);
            button3.Size = new Size(width / 6, buttonheight);
            button3.Location = new Point(button2.Location.X + button2.Size.Width, button1.Location.Y);
            int lastwidth = width - button1.Width * 5;
            button4.Size = new Size(lastwidth, buttonheight);
            button4.Location = new Point(button3.Location.X + button3.Size.Width, button1.Location.Y);

            button27.Size = new Size(width / 6, buttonheight);
            label3.Size = button27.Size;
            button27.Location = new Point(0, button4.Location.Y + button4.Height);
            label3.Location = button27.Location;
            label3.Location = new Point(label3.Location.X, label3.Location.Y - 1);
            button28.Size = new Size(width / 6, buttonheight);
            label4.Size = button28.Size;
            button28.Location = new Point(button27.Size.Width + button27.Location.X, button27.Location.Y);
            label4.Location = button28.Location;
            label4.Location = new Point(label4.Location.X, label4.Location.Y - 1);
            button8.Size = new Size(width / 6, buttonheight);
            label17.Size = button27.Size;
            button8.Location = new Point(button28.Location.X + button28.Size.Width, button28.Location.Y);
            label17.Location = button8.Location;
            label17.Location = new Point(label17.Location.X, label17.Location.Y - 1);
            button7.Size = new Size(width / 6, buttonheight);
            label18.Size = button27.Size;
            button7.Location = new Point(button8.Location.X + button8.Size.Width, button8.Location.Y);
            label18.Location = button7.Location;
            label18.Location = new Point(label18.Location.X, label18.Location.Y - 1);
            button6.Size = new Size(width / 6, buttonheight);
            label19.Size = button6.Size;
            button6.Location = new Point(button7.Location.X + button7.Size.Width, button8.Location.Y);
            label19.Location = button6.Location;
            label19.Location = new Point(label19.Location.X, label19.Location.Y - 1);
            button5.Size = new Size(lastwidth, buttonheight);
            button5.Location = new Point(button6.Location.X + button6.Size.Width, button8.Location.Y);

            button29.Size = new Size(width / 6, buttonheight);
            label5.Size = button29.Size;
            button29.Location = new Point(0, button5.Location.Y + button5.Height);
            label5.Location = button29.Location;
            label5.Location = new Point(label5.Location.X, label5.Location.Y - 1);
            button30.Size = new Size(width / 6, buttonheight);
            label6.Size = button30.Size;
            button30.Location = new Point(button29.Location.X + button29.Width, button29.Location.Y);
            label6.Location = button30.Location;
            label6.Location = new Point(label6.Location.X, label6.Location.Y - 1);
            button12.Size = new Size(width / 6, buttonheight);
            label14.Size = button12.Size;
            button12.Location = new Point(button30.Location.X + button30.Width, button29.Location.Y);
            label14.Location = button12.Location;
            label14.Location = new Point(label14.Location.X, label14.Location.Y - 1);
            button11.Size = new Size(width / 6, buttonheight);
            label15.Size = button12.Size;
            button11.Location = new Point(button12.Location.X + button12.Size.Width, button12.Location.Y);
            label15.Location = button11.Location;
            label15.Location = new Point(label15.Location.X, label15.Location.Y - 1);
            button10.Size = new Size(width / 6, buttonheight);
            label16.Size = button10.Size;
            button10.Location = new Point(button11.Location.X + button11.Size.Width, button12.Location.Y);
            label16.Location = button10.Location;
            label16.Location = new Point(label16.Location.X, label16.Location.Y - 1);
            button9.Size = new Size(lastwidth, buttonheight);
            button9.Location = new Point(button10.Location.X + button10.Size.Width, button12.Location.Y);

            button31.Size = new Size(width / 6, buttonheight);
            label7.Size = button31.Size;
            button31.Location = new Point(0, button9.Location.Y + button9.Height);
            label7.Location = button31.Location;
            label7.Location = new Point(label7.Location.X, label7.Location.Y - 1);
            button32.Size = new Size(width / 6, buttonheight);
            label8.Size = button32.Size;
            button32.Location = new Point(button31.Location.X + button31.Width, button31.Location.Y);
            label8.Location = button32.Location;
            label8.Location = new Point(label8.Location.X, label8.Location.Y - 1);
            button16.Size = new Size(width / 6, buttonheight);
            button16.Location = new Point(button32.Location.X + button32.Width, button32.Location.Y);
            button15.Size = new Size(width / 6, buttonheight);
            label12.Size = button15.Size;
            button15.Location = new Point(button16.Location.X + button16.Size.Width, button16.Location.Y);
            label12.Location = button15.Location;
            label12.Location = new Point(label12.Location.X, label12.Location.Y - 1);
            button14.Size = new Size(width / 6, buttonheight);
            label13.Size = button14.Size;
            button14.Location = new Point(button15.Location.X + button15.Size.Width, button16.Location.Y);
            label13.Location = button14.Location;
            label13.Location = new Point(label13.Location.X, label13.Location.Y - 1);
            button13.Size = new Size(lastwidth, buttonheight);
            button13.Location = new Point(button14.Location.X + button14.Size.Width, button16.Location.Y);

            int lastheight = (height - (label1.Height + label2.Height + button22.Height * 3)) - button12.Height - button16.Height - button1.Height - button8.Height;
            button33.Size = new Size(width / 6, lastheight);
            label9.Size = button33.Size;
            button33.Location = new Point(0, button13.Location.Y + button13.Height);
            label9.Location = button33.Location;
            label9.Location = new Point(label9.Location.X, label9.Location.Y - 1);
            button34.Size = new Size(width / 6, lastheight);
            label10.Size = button34.Size;
            button34.Location = new Point(button33.Location.X + button33.Width, button33.Location.Y);
            label10.Location = button34.Location;
            label10.Location = new Point(label10.Location.X, label10.Location.Y - 1);
            button20.Size = new Size(width / 6, lastheight);
            button20.Location = new Point(button34.Location.X + button34.Width, button34.Location.Y);
            button19.Size = new Size(width / 6, lastheight);
            button19.Location = new Point(button20.Location.X + button20.Size.Width, button20.Location.Y);
            button18.Size = new Size(width / 6, lastheight);
            label21.Size = button18.Size;
            button18.Location = new Point(button19.Location.X + button19.Size.Width, button20.Location.Y);
            label21.Location = new Point(button18.Location.X, button18.Location.Y - 1);
            button17.Size = new Size(lastwidth, lastheight);
            button17.Location = new Point(button18.Location.X + button18.Size.Width, button20.Location.Y);
            //button21.Focus();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            //button21.Focus();
            this.Size = this.MinimumSize;
            button23.PerformClick();
            if (Width <= 400)
            {
                button3.Font = button26.Font;
                button20.Font = button26.Font;
            }
            else if (Width <= 450)
            {
                button3.Font = button35.Font;
                button20.Font = button35.Font;
            }
            else if (Width <= 500)
            {
                button3.Font = button37.Font;
                button20.Font = button37.Font;
            }
            else
            {
                button3.Font = button25.Font;
                button20.Font = button25.Font;
            }
            int width = Size.Width - 15;//边框
            int height = Size.Height - 38;

            //18->30<->322->888
            int _fontsize = (Width - 322) * (30 - 17) / (888 - 322) + 17;
            int _fontsize1 = (Width - 322) * (18 - 9) / (888 - 322) + 9;
            if (_fontsize >= 30)
            {
                label11.Font = new Font("微软雅黑", 30);
                label1.Font = new Font("微软雅黑", 18);
            }
            else
            {
                label11.Font = new Font("微软雅黑", _fontsize);
                label1.Font = new Font("微软雅黑", _fontsize1);
            }
            label1.Size = new Size(width, height / 10);
            label1.Location = new Point(0, 0);
            label2.Size = new Size(width, height / 7);
            label2.Location = new Point(0, label1.Size.Height);
            label11.Size = label2.Size;
            label11.Location = label2.Location;
            button22.Size = new Size(width, label2.Height / 3);
            button22.Location = new Point(0, label2.Location.Y + label2.Height);
            button23.Size = new Size(width, label2.Height / 3);
            button23.Location = new Point(0, button22.Location.Y + button22.Height);
            button24.Size = new Size(width, label2.Height / 3);
            button24.Location = new Point(0, button23.Location.Y + button23.Height);
            //label20.Size = new Size(9999, 9999);
            //label20.Location = new Point(0, button24.Location.Y + button24.Height);

            int buttonheight = (height - (label1.Height + label2.Height + button22.Height * 3)) / 5;
            button36.Size = new Size(width / 3, buttonheight);
            button36.Location = new Point(0, button24.Location.Y + button24.Height);
            button1.Size = new Size(width / 6, buttonheight);
            button1.Location = new Point(button36.Size.Width + button36.Location.X, button36.Location.Y);
            button2.Size = new Size(width / 6, buttonheight);
            button2.Location = new Point(button1.Size.Width + button1.Location.X, button1.Location.Y);
            button3.Size = new Size(width / 6, buttonheight);
            button3.Location = new Point(button2.Location.X + button2.Size.Width, button1.Location.Y);
            int lastwidth = width - button1.Width * 5;
            button4.Size = new Size(lastwidth, buttonheight);
            button4.Location = new Point(button3.Location.X + button3.Size.Width, button1.Location.Y);

            button27.Size = new Size(width / 6, buttonheight);
            label3.Size = button27.Size;
            button27.Location = new Point(0, button4.Location.Y + button4.Height);
            label3.Location = button27.Location;
            label3.Location = new Point(label3.Location.X, label3.Location.Y - 1);
            button28.Size = new Size(width / 6, buttonheight);
            label4.Size = button28.Size;
            button28.Location = new Point(button27.Size.Width + button27.Location.X, button27.Location.Y);
            label4.Location = button28.Location;
            label4.Location = new Point(label4.Location.X, label4.Location.Y - 1);
            button8.Size = new Size(width / 6, buttonheight);
            label17.Size = button27.Size;
            button8.Location = new Point(button28.Location.X + button28.Size.Width, button28.Location.Y);
            label17.Location = button8.Location;
            label17.Location = new Point(label17.Location.X, label17.Location.Y - 1);
            button7.Size = new Size(width / 6, buttonheight);
            label18.Size = button27.Size;
            button7.Location = new Point(button8.Location.X + button8.Size.Width, button8.Location.Y);
            label18.Location = button7.Location;
            label18.Location = new Point(label18.Location.X, label18.Location.Y - 1);
            button6.Size = new Size(width / 6, buttonheight);
            label19.Size = button6.Size;
            button6.Location = new Point(button7.Location.X + button7.Size.Width, button8.Location.Y);
            label19.Location = button6.Location;
            label19.Location = new Point(label19.Location.X, label19.Location.Y - 1);
            button5.Size = new Size(lastwidth, buttonheight);
            button5.Location = new Point(button6.Location.X + button6.Size.Width, button8.Location.Y);

            button29.Size = new Size(width / 6, buttonheight);
            label5.Size = button29.Size;
            button29.Location = new Point(0, button5.Location.Y + button5.Height);
            label5.Location = button29.Location;
            label5.Location = new Point(label5.Location.X, label5.Location.Y - 1);
            button30.Size = new Size(width / 6, buttonheight);
            label6.Size = button30.Size;
            button30.Location = new Point(button29.Location.X + button29.Width, button29.Location.Y);
            label6.Location = button30.Location;
            label6.Location = new Point(label6.Location.X, label6.Location.Y - 1);
            button12.Size = new Size(width / 6, buttonheight);
            label14.Size = button12.Size;
            button12.Location = new Point(button30.Location.X + button30.Width, button29.Location.Y);
            label14.Location = button12.Location;
            label14.Location = new Point(label14.Location.X, label14.Location.Y - 1);
            button11.Size = new Size(width / 6, buttonheight);
            label15.Size = button12.Size;
            button11.Location = new Point(button12.Location.X + button12.Size.Width, button12.Location.Y);
            label15.Location = button11.Location;
            label15.Location = new Point(label15.Location.X, label15.Location.Y - 1);
            button10.Size = new Size(width / 6, buttonheight);
            label16.Size = button10.Size;
            button10.Location = new Point(button11.Location.X + button11.Size.Width, button12.Location.Y);
            label16.Location = button10.Location;
            label16.Location = new Point(label16.Location.X, label16.Location.Y - 1);
            button9.Size = new Size(lastwidth, buttonheight);
            button9.Location = new Point(button10.Location.X + button10.Size.Width, button12.Location.Y);

            button31.Size = new Size(width / 6, buttonheight);
            label7.Size = button31.Size;
            button31.Location = new Point(0, button9.Location.Y + button9.Height);
            label7.Location = button31.Location;
            label7.Location = new Point(label7.Location.X, label7.Location.Y - 1);
            button32.Size = new Size(width / 6, buttonheight);
            label8.Size = button32.Size;
            button32.Location = new Point(button31.Location.X + button31.Width, button31.Location.Y);
            label8.Location = button32.Location;
            label8.Location = new Point(label8.Location.X, label8.Location.Y - 1);
            button16.Size = new Size(width / 6, buttonheight);
            button16.Location = new Point(button32.Location.X + button32.Width, button32.Location.Y);
            button15.Size = new Size(width / 6, buttonheight);
            label12.Size = button15.Size;
            button15.Location = new Point(button16.Location.X + button16.Size.Width, button16.Location.Y);
            label12.Location = button15.Location;
            label12.Location = new Point(label12.Location.X , label12.Location.Y - 1);
            button14.Size = new Size(width / 6, buttonheight);
            label13.Size = button14.Size;
            button14.Location = new Point(button15.Location.X + button15.Size.Width, button16.Location.Y);
            label13.Location = button14.Location;
            label13.Location = new Point(label13.Location.X , label13.Location.Y - 1);
            button13.Size = new Size(lastwidth, buttonheight);
            button13.Location = new Point(button14.Location.X + button14.Size.Width, button16.Location.Y);

            int lastheight = (height - (label1.Height + label2.Height + button22.Height * 3)) - button12.Height - button16.Height - button1.Height - button8.Height;
            button33.Size = new Size(width / 6, lastheight);
            label9.Size = button33.Size;
            button33.Location = new Point(0, button13.Location.Y + button13.Height);
            label9.Location = button33.Location;
            label9.Location = new Point(label9.Location.X, label9.Location.Y - 1);
            button34.Size = new Size(width / 6, lastheight);
            label10.Size = button34.Size;
            button34.Location = new Point(button33.Location.X + button33.Width, button33.Location.Y);
            label10.Location = button34.Location;
            label10.Location = new Point(label10.Location.X, label10.Location.Y - 1);
            button20.Size = new Size(width / 6, lastheight);
            button20.Location = new Point(button34.Location.X + button34.Width, button34.Location.Y);
            button19.Size = new Size(width / 6, lastheight);
            button19.Location = new Point(button20.Location.X + button20.Size.Width, button20.Location.Y);
            button18.Size = new Size(width / 6, lastheight);
            label21.Size = button18.Size;
            button18.Location = new Point(button19.Location.X + button19.Size.Width, button20.Location.Y);
            label21.Location = new Point(button18.Location.X, button18.Location.Y - 1);
            button17.Size = new Size(lastwidth, lastheight);
            button17.Location = new Point(button18.Location.X + button18.Size.Width, button20.Location.Y);
            //button21.Focus();
        }

        private void Form1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == '0') button19.PerformClick();
            else if (e.KeyChar == '1') button16.PerformClick();
            else if (e.KeyChar == '2'&& _flag==2) button15.PerformClick();
            else if (e.KeyChar == '3' && _flag == 2) button14.PerformClick();
            else if (e.KeyChar == '4' && _flag == 2) button12.PerformClick();
            else if (e.KeyChar == '5' && _flag == 2) button11.PerformClick();
            else if (e.KeyChar == '6' && _flag == 2) button10.PerformClick();
            else if (e.KeyChar == '7' && _flag == 2) button8.PerformClick();
            else if (e.KeyChar == '8' && _flag == 2) button7.PerformClick();
            else if (e.KeyChar == '9' && _flag == 2) button6.PerformClick();
            else if (e.KeyChar == '+') button13.PerformClick();
            else if (e.KeyChar == '*') button5.PerformClick();
            else if (e.KeyChar == '-') button9.PerformClick();
            else if (e.KeyChar == '/') button4.PerformClick();
            else if (e.KeyChar == '%') button36.PerformClick();
            else if (ModifierKeys == Keys.Shift && (e.KeyChar == 'c' || e.KeyChar == 'C')) button2.PerformClick();
            else if (ModifierKeys == Keys.Shift && (e.KeyChar == 'e' || e.KeyChar == 'E')) button1.PerformClick();
            else if (e.KeyChar == '.' && _flag == 2) button18.PerformClick();
            else if (e.KeyChar == 'n'|| e.KeyChar == 'N') button20.PerformClick();//nega
            else if (e.KeyChar == '=') button17.PerformClick();
            else if ((e.KeyChar == 'a'|| e.KeyChar == 'A')&&_flag==1) button18.PerformClick();
            else if ((e.KeyChar == 'b' || e.KeyChar == 'B')&&_flag==1) button20.PerformClick();
            else if ((e.KeyChar == 'c' || e.KeyChar == 'C')&&_flag==1) button17.PerformClick();
            else if ((e.KeyChar == 'd' || e.KeyChar == 'D')&&_flag==1) button18.PerformClick();
            else if ((e.KeyChar == 'e' || e.KeyChar == 'E')&&_flag==1) button20.PerformClick();
            else if ((e.KeyChar == 'f' || e.KeyChar == 'F')&&_flag==1) button17.PerformClick();
            else if (e.KeyChar == 8) button3.PerformClick();
        }

        private void label2_Click(object sender, EventArgs e)
        {
            button21.Focus();
        }

        private void label1_Click(object sender, EventArgs e)
        {
            button21.Focus();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            button17.PerformClick();
        }

        private void button22_Click(object sender, EventArgs e)
        {
            button22.ForeColor = button25.ForeColor;
            button23.ForeColor = button26.ForeColor;
            button24.ForeColor = button26.ForeColor;
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label21.Visible = false;
            _flag = 1;
            button21.Focus();
            MessageBox.Show("Not available yet!");
        }

        private void button23_Click(object sender, EventArgs e)
        {
            if (_flag != 2)
            {
                button22.ForeColor = button26.ForeColor;
                button23.ForeColor = button25.ForeColor;
                button24.ForeColor = button26.ForeColor;
                label12.Visible = false;
                label13.Visible = false;
                label14.Visible = false;
                label15.Visible = false;
                label16.Visible = false;
                label17.Visible = false;
                label18.Visible = false;
                label19.Visible = false;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label21.Visible = false;
                _flag = 2;
                button21.Focus();
                    label1.Text = _num1.ToString();
                    if (_operator != "#") label1.Text = label1.Text + " " + _operator + " ";
                    label2.Text = Convert.ToString((Convert.ToInt32(label2.Text, 2)));
            }
        }

        private void button36_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            _num1 = Convert.ToDecimal(label2.Text);
            label2.Text = "";
            label1.Text = _num1.ToString() + " Mod ";
            _operator = "%";
            button21.Focus();
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            button21.Focus();
        }

        private void Label2_TextChanged(object sender, EventArgs e)
        {
            if (label2.Text == "非数字" || label2.Text.Length > 30)
            {
                label11.Text = "ERROR";
            }
            else if(label2.Text=="暂不允许异号连算")
                label11.Text = "暂不允许异号连算";
            else
            {
                if (_flag == 2)
                {
                    if (label2.Text.IndexOf('.') == -1 && label2.Text.IndexOf('E') == -1)
                    {
                        string _str = label2.Text, _str2 = "";
                        int len = _str.Length;
                        while (_str.Length > 0)
                        {
                            if (_str.Length == 1)
                            {
                                if ((len - _str.Length) % 3 == 0 && len - _str.Length != 0) _str2 = "," + _str2;
                                _str2 = _str.Substring(_str.Length - 1, 1) + _str2;
                                //_str = "";
                                break;
                            }
                            else
                            {
                                if ((len - _str.Length) % 3 == 0 && len - _str.Length != 0) _str2 = "," + _str2;
                                _str2 = _str.Substring(_str.Length - 1, 1) + _str2;
                                _str = _str.Substring(0, _str.Length - 1);
                            }
                        }
                        if (_str2.IndexOf("-") == 0 && _str2.IndexOf(",") == 1)
                        { _str2 = _str2.Substring(2, _str2.Length - 2); _str2 = "-" + _str2; }
                        label11.Text = _str2;
                    }
                    else
                    {
                        label11.Text = label2.Text;
                    }
                }
                else label11.Text = label2.Text;
                //dec
                if (_flag == 2)
                {
                    if (label2.Text.IndexOf("E") != -1)
                    {
                        button22.Text = "HEX ERROR";
                        button23.Text = "DEC " + label2.Text;
                        button24.Text = "BIN ERROR";
                    }
                    else
                    {
                        if (label2.Text == "") label2.Text = "0";
                        if (Convert.ToDouble(label2.Text) > 9223372036854775807 || label2.Text.IndexOf(".") >= 0)
                        {
                            button22.Text = "HEX ERROR";
                            button23.Text = "DEC " + label2.Text;
                            button24.Text = "BIN ERROR";
                        }
                        else
                        {
                            long d;
                            if (label2.Text != "")
                                d = Convert.ToInt64(label2.Text);//9 is to big,can't solve now
                            else d = 0;
                            button22.Text = "HEX " + Convert.ToString(d, 16).ToUpper();
                            button23.Text = "DEC " + Convert.ToString(d);
                            if (label2.Text.IndexOf("-") != -1) button24.Text = "BIN ERROR";
                            else button24.Text = "BIN " + Convert.ToString(d, 2);
                        }
                    }
                }
                //hex
                else if (_flag == 1)
                {
                    button22.Text = "HEX " + label2.Text;
                }
                //bin
                else if (_flag == 3)
                {
                    button24.Text = "BIN " + label2.Text;
                    long d;
                    if (label2.Text != "") d = Convert.ToInt64(label2.Text);
                    else label2.Text = "0";
                    button23.Text = "DEC " + Convert.ToInt32(label2.Text, 2);
                    d = Convert.ToInt32(label2.Text, 2);
                    button22.Text = "HEX " + Convert.ToString(d, 16).ToUpper();
                }
            }
        }

        private void button27_Click(object sender, EventArgs e)
        {
            //label2.Text = label2.Text + 'A';
            //MessageBox.Show("now is not allowed hex!");
        }

        private void button24_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            if (_flag != 3)
            {
                button22.ForeColor = button26.ForeColor;
                button23.ForeColor = button26.ForeColor;
                button24.ForeColor = button25.ForeColor;
                label12.Visible = true;
                label13.Visible = true;
                label14.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;
                label18.Visible = true;
                label19.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                label5.Visible = true;
                label6.Visible = true;
                label7.Visible = true;
                label8.Visible = true;
                label21.Visible = true;
                _flag = 3;
                button21.Focus();
                if (label2.Text == "非数字") button2.PerformClick();
                if (_num1 != (int)_num1 || label2.Text.IndexOf(".") != -1 || Convert.ToDouble(label2.Text) > Int32.MaxValue)
                {
                    if (_num1 == (int)_num1)
                    {
                        label1.Text = Convert.ToString((int)_num1, 2);
                        if (_operator != "#") label1.Text = label1.Text + " " + _operator + " ";
                    }
                    else { _num1 = 0; label1.Text = ""; }
                    label2.Text = "0";
                    //MessageBox.Show("not available!");
                }
                else
                {
                        label1.Text = Convert.ToString((int)_num1, 2);
                        if (_operator != "#") label1.Text = label1.Text + " " + _operator + " ";
                        label2.Text = Convert.ToString(((Convert.ToInt32(label2.Text))), 2);
                }
            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            if (label2.Text != "")
            {
                if (_operator != "-" && _operator != "#") 
                { 
                    label2.Text = "暂不允许异号连算"; 
                }
                else
                {
                    if (_calfi == false && _firstoperator == false)
                    {
                        label1.Text = label1.Text + label2.Text + " - ";
                        _num2 = Convert.ToDecimal(label2.Text);
                        _num1 = _num1 - _num2;
                        label2.Text = Convert.ToString(_num1);
                        _secalfi = true;
                    }
                    else
                    {
                        label1.Text = label2.Text + " - ";
                        _operator = "-";
                        if (_flag == 3)
                            _num1 = Convert.ToInt32(label2.Text, 2);
                        else _num1 = Convert.ToDecimal(label2.Text);
                        _num2 = _num1;
                        label2.Text = "";
                        _firstoperator = false;
                    }
                }
            }
            else
            {
                if (label1.Text != "")
                {
                    label1.Text = label1.Text.Substring(0, label1.Text.Length - 2);
                    label1.Text = label1.Text + "- ";
                    _operator = "-";
                }
            }
        }

        private void button13_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            if (label2.Text != "")
            {
                if (_operator != "+"&&_operator!="#") label2.Text = "暂不允许异号连算";
                else
                {
                    if (_calfi == false && _firstoperator == false)
                    {
                        label1.Text = label1.Text + label2.Text + " + ";
                        _num2 = Convert.ToDecimal(label2.Text);
                        _num1 = _num1 + _num2;
                        label2.Text = Convert.ToString(_num1);
                        _secalfi = true;
                    }
                    else
                    {
                        label1.Text = label2.Text + " + ";
                        _operator = "+";
                        if (_flag == 3)
                            _num1 = Convert.ToInt32(label2.Text, 2);
                        else _num1 = Convert.ToDecimal(label2.Text);
                        _num2 = _num1;
                        label2.Text = "";
                        _firstoperator = false;
                    }
                }
            }
            else
            {
                if (label1.Text != "")
                {
                    label1.Text = label1.Text.Substring(0, label1.Text.Length - 2);
                    label1.Text = label1.Text + "+ ";
                    _operator = "+";
                }
            }
        }

        private void button17_Click(object sender, EventArgs e)
        {
            if (label2.Text == "暂不允许异号连算") return;
            _calfi = true;
            if (label2.Text == "")
            {
                if (_operator == "%")
                {
                    _result = 0;
                    label1.Text = label1.Text + _num2.ToString() + " = ";
                }
                else
                {
                    _num2 = _num1;
                    if (_operator == "+") _result = _num1 + _num2;
                    else if (_operator == "-") _result = _num1 - _num2;
                    else if (_operator == "*") _result = _num1 * _num2;
                    else if (_operator == "/") _result = _num1 / _num2;
                    else if (_operator == "#") _result = _num1;
                    if (_operator == "#")
                    {
                        label1.Text = "";
                        label1.Text = Convert.ToString(_num1) + " = ";
                    }
                    else { label1.Text = label1.Text + Convert.ToString(_num2) + " ="; }
                }
                label2.Text = _result.ToString();
            }
            else
            {
                if (label1.Text == "") { }
                else
                {
                    if (_operator == "#")
                    {
                        label1.Text = "";
                        if (label2.Text != "") _num1 = Convert.ToDecimal(label2.Text);
                        _result = _num1;
                        if (_lastoperator == "/")
                        {
                            _result = _num1 / _num2;
                            label1.Text = Convert.ToString(_num1) + " / " + Convert.ToString(_num2) + " = ";
                        }
                        else if (_lastoperator == "*")
                        {
                            _result = _num1 * _num2;
                            label1.Text = Convert.ToString(_num1) + " * " + Convert.ToString(_num2) + " = ";
                        }
                        else if (_lastoperator == "+")
                        {
                            _result = _num1 + _num2;
                            label1.Text = Convert.ToString(_num1) + " + " + Convert.ToString(_num2) + " = ";
                        }
                        else if (_lastoperator == "-")
                        {
                            _result = _num1 - _num2;
                            label1.Text = Convert.ToString(_num1) + " - " + Convert.ToString(_num2) + " = ";
                        }
                        label2.Text = _result.ToString();
                    }
                    else
                    {
                        if (_flag == 3) _num2 = Convert.ToInt32(label2.Text,2);
                        else _num2 = Convert.ToDecimal(label2.Text);
                        if (_operator == "+") _result = _num1 + _num2;
                        else if (_operator == "-") _result = _num1 - _num2;
                        else if (_operator == "*") _result = _num1 * _num2;
                        else if (_operator == "/") _result = _num1 / _num2;
                        else if (_operator == "%") _result = _num1 % _num2;
                        if (_flag == 3) label1.Text = label1.Text + Convert.ToString((int)_num2,2) + " =";
                        else label1.Text = label1.Text + Convert.ToString(_num2) + " =";
                        if (_flag == 3)
                        {
                            if (_result != (int)_result) label2.Text = Convert.ToString((int)_result,2);
                            else label2.Text = Convert.ToString((int)_result, 2);
                        }
                        else if (_flag == 2) label2.Text = _result.ToString();
                    }
                }
                if (_operator != "#") _lastoperator = _operator;
                _operator = "#";
                button21.Focus();
            }
        }

        public Form1()
        {
            InitializeComponent();
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void label13_Click(object sender, EventArgs e)
        {

        }
    }
}
