using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace RhythmEdit
{
    public partial class FileInfo : Form
    {
        public FileInfo()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (fileName.Text == "" || maker.Text == "" || (Diff.SelectedItem.ToString() != "简单" && Diff.SelectedItem.ToString() != "中等" && Diff.SelectedItem.ToString() != "困难"))
            {
                MessageBox.Show(this, "信息错误，请重新输入!", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                button1.DialogResult = DialogResult.None;
            }
            else 
            { 
                button1.DialogResult = DialogResult.OK;
                button3.PerformClick();
            }
        }

        private void FileInfo_Load(object sender, EventArgs e)
        {
            Diff.DropDownStyle = ComboBoxStyle.DropDownList;
        }
    }
}
