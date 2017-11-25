namespace Snow
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.timer = new System.Windows.Forms.Timer(this.components);
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.继续ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.暂停ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.数量ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.自动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.多ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.中ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.少ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.飘动ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.置顶ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer
            // 
            this.timer.Enabled = true;
            this.timer.Interval = 1;
            this.timer.Tick += new System.EventHandler(this.timer_Tick);
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.ContextMenuStrip = this.contextMenuStrip1;
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "雪花";
            this.notifyIcon1.Visible = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.继续ToolStripMenuItem,
            this.暂停ToolStripMenuItem,
            this.数量ToolStripMenuItem,
            this.飘动ToolStripMenuItem,
            this.置顶ToolStripMenuItem,
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 158);
            // 
            // 继续ToolStripMenuItem
            // 
            this.继续ToolStripMenuItem.Enabled = false;
            this.继续ToolStripMenuItem.Name = "继续ToolStripMenuItem";
            this.继续ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.继续ToolStripMenuItem.Text = "继续";
            this.继续ToolStripMenuItem.Click += new System.EventHandler(this.继续ToolStripMenuItem_Click);
            // 
            // 暂停ToolStripMenuItem
            // 
            this.暂停ToolStripMenuItem.Name = "暂停ToolStripMenuItem";
            this.暂停ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.暂停ToolStripMenuItem.Text = "暂停";
            this.暂停ToolStripMenuItem.Click += new System.EventHandler(this.暂停ToolStripMenuItem_Click);
            // 
            // 数量ToolStripMenuItem
            // 
            this.数量ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.自动ToolStripMenuItem,
            this.多ToolStripMenuItem,
            this.中ToolStripMenuItem,
            this.少ToolStripMenuItem});
            this.数量ToolStripMenuItem.Name = "数量ToolStripMenuItem";
            this.数量ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.数量ToolStripMenuItem.Text = "数量";
            // 
            // 自动ToolStripMenuItem
            // 
            this.自动ToolStripMenuItem.Name = "自动ToolStripMenuItem";
            this.自动ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.自动ToolStripMenuItem.Text = "自动";
            this.自动ToolStripMenuItem.Click += new System.EventHandler(this.自动ToolStripMenuItem_Click);
            // 
            // 多ToolStripMenuItem
            // 
            this.多ToolStripMenuItem.Name = "多ToolStripMenuItem";
            this.多ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.多ToolStripMenuItem.Text = "多(0.3s/个)";
            this.多ToolStripMenuItem.Click += new System.EventHandler(this.多ToolStripMenuItem_Click);
            // 
            // 中ToolStripMenuItem
            // 
            this.中ToolStripMenuItem.Checked = true;
            this.中ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.中ToolStripMenuItem.Name = "中ToolStripMenuItem";
            this.中ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.中ToolStripMenuItem.Text = "中(0.9s/个)";
            this.中ToolStripMenuItem.Click += new System.EventHandler(this.中ToolStripMenuItem_Click);
            // 
            // 少ToolStripMenuItem
            // 
            this.少ToolStripMenuItem.Name = "少ToolStripMenuItem";
            this.少ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.少ToolStripMenuItem.Text = "少(1.2s/个)";
            this.少ToolStripMenuItem.Click += new System.EventHandler(this.少ToolStripMenuItem_Click);
            // 
            // 飘动ToolStripMenuItem
            // 
            this.飘动ToolStripMenuItem.Checked = true;
            this.飘动ToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.飘动ToolStripMenuItem.Name = "飘动ToolStripMenuItem";
            this.飘动ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.飘动ToolStripMenuItem.Text = "飘动";
            this.飘动ToolStripMenuItem.Click += new System.EventHandler(this.飘动ToolStripMenuItem_Click);
            // 
            // 置顶ToolStripMenuItem
            // 
            this.置顶ToolStripMenuItem.Name = "置顶ToolStripMenuItem";
            this.置顶ToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.置顶ToolStripMenuItem.Text = "置顶";
            this.置顶ToolStripMenuItem.Click += new System.EventHandler(this.置顶ToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "退出";
            this.toolStripMenuItem1.Click += new System.EventHandler(this.toolStripMenuItem1_Click);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 600;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(685, 415);
            this.ControlBox = false;
            this.DoubleBuffered = true;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem 暂停ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 继续ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 数量ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 多ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 中ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 少ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 置顶ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 飘动ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 自动ToolStripMenuItem;
    }
}

