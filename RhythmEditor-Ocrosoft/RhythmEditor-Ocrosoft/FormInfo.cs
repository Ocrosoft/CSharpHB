using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RhythmEditor_Ocrosoft
{
    public partial class FormInfo : Form
    {
        public FormInfo()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text == ""
                || textBoxAuthor.Text == ""
                || textBoxMaker.Text == "")
            {
                buttonOK.DialogResult = DialogResult.None;
                MessageBox.Show(this, "Please fill all of textboxes!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                buttonOK.DialogResult = DialogResult.OK;
        }

        private void buttonOKSurface_Click(object sender, EventArgs e)
        {
            if (textBoxTitle.Text == ""
                || textBoxAuthor.Text == ""
                || textBoxMaker.Text == "")
            {
                MessageBox.Show(this, "Please fill all of textboxes!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
                buttonOK.PerformClick();
        }

        private void FormInfo_Load(object sender, EventArgs e)
        {
            toolTip1.SetToolTip(checkBoxEn1, "Will be shown when player is playing if title is English Only.");
            toolTip1.SetToolTip(checkBoxEn2, "No use.");
            toolTip1.SetToolTip(checkBoxEn3, "Will be shown when player is playing if maker is English Only.");
        }
    }
}
