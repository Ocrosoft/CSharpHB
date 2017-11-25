using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Chess
{
    public partial class DlgSendChallenge : Form
    {
        public string _infor
        {
            get
            {
                return labelInfo.Text;
            }
            set
            {
                labelInfo.Text = value;
            }
        }
        public DlgSendChallenge()
        {
            InitializeComponent();
        }
    }
}
