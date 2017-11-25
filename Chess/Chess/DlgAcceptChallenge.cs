using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;

namespace Chess
{
    public struct ClientParam
    {
        public IPAddress _ip;
        public int _port;
        public string _nickName;
    }
    public partial class DlgAcceptChallenge : Form
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
        public DlgAcceptChallenge()
        {
            InitializeComponent();
        }
    }
}
