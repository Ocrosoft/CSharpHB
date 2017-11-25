namespace Chess
{
    partial class FormMain
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItemGame = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemStart = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.numricServerPort = new System.Windows.Forms.NumericUpDown();
            this.labelNickName = new System.Windows.Forms.Label();
            this.textBoxNickName = new System.Windows.Forms.TextBox();
            this.textboxServerIp = new System.Windows.Forms.TextBox();
            this.labelServerPort = new System.Windows.Forms.Label();
            this.labelServerIP = new System.Windows.Forms.Label();
            this.labelPlayers = new System.Windows.Forms.Label();
            this.buttonQuit = new System.Windows.Forms.Button();
            this.buttonsurrender = new System.Windows.Forms.Button();
            this.buttonchallenge = new System.Windows.Forms.Button();
            this.buttonLogin = new System.Windows.Forms.Button();
            this.listViewClient = new System.Windows.Forms.ListView();
            this.nickname = new System.Windows.Forms.ColumnHeader();
            this.IP = new System.Windows.Forms.ColumnHeader();
            this.port = new System.Windows.Forms.ColumnHeader();
            this.state = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.buttonRandomName = new System.Windows.Forms.Button();
            this.MenuItemvsc = new System.Windows.Forms.ToolStripMenuItem();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.menuStrip1.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numricServerPort)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(24, 24);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemGame});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 1, 0, 1);
            this.menuStrip1.Size = new System.Drawing.Size(1357, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuItemGame
            // 
            this.MenuItemGame.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemStart,
            this.MenuItemUndo,
            this.MenuItemSave,
            this.MenuItemOpen,
            this.MenuItemvsc});
            this.MenuItemGame.Name = "MenuItemGame";
            this.MenuItemGame.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuItemGame.Size = new System.Drawing.Size(44, 22);
            this.MenuItemGame.Text = "游戏";
            // 
            // MenuItemStart
            // 
            this.MenuItemStart.Enabled = false;
            this.MenuItemStart.Name = "MenuItemStart";
            this.MenuItemStart.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.B)));
            this.MenuItemStart.Size = new System.Drawing.Size(171, 22);
            this.MenuItemStart.Text = "开局";
            this.MenuItemStart.Click += new System.EventHandler(this.MenuItemStart_Click);
            // 
            // MenuItemUndo
            // 
            this.MenuItemUndo.Enabled = false;
            this.MenuItemUndo.Name = "MenuItemUndo";
            this.MenuItemUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.MenuItemUndo.Size = new System.Drawing.Size(171, 22);
            this.MenuItemUndo.Text = "悔棋";
            this.MenuItemUndo.Click += new System.EventHandler(this.MenuItemUndo_Click);
            // 
            // MenuItemSave
            // 
            this.MenuItemSave.Name = "MenuItemSave";
            this.MenuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuItemSave.Size = new System.Drawing.Size(171, 22);
            this.MenuItemSave.Text = "保存残局";
            this.MenuItemSave.Click += new System.EventHandler(this.MenuItemSave_Click);
            // 
            // MenuItemOpen
            // 
            this.MenuItemOpen.Enabled = false;
            this.MenuItemOpen.Name = "MenuItemOpen";
            this.MenuItemOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuItemOpen.Size = new System.Drawing.Size(171, 22);
            this.MenuItemOpen.Text = "打开残局";
            this.MenuItemOpen.Click += new System.EventHandler(this.MenuItemOpen_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.webBrowser1);
            this.splitContainer1.Panel1.Controls.Add(this.pictureBox1);
            this.splitContainer1.Panel1MinSize = 0;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Panel2MinSize = 0;
            this.splitContainer1.Size = new System.Drawing.Size(1357, 936);
            this.splitContainer1.SplitterDistance = 974;
            this.splitContainer1.TabIndex = 1;
            this.splitContainer1.TabStop = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(974, 936);
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseMove);
            this.pictureBox1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDown);
            this.pictureBox1.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer2.IsSplitterFixed = true;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.buttonRandomName);
            this.splitContainer2.Panel1.Controls.Add(this.numricServerPort);
            this.splitContainer2.Panel1.Controls.Add(this.labelNickName);
            this.splitContainer2.Panel1.Controls.Add(this.textBoxNickName);
            this.splitContainer2.Panel1.Controls.Add(this.textboxServerIp);
            this.splitContainer2.Panel1.Controls.Add(this.labelServerPort);
            this.splitContainer2.Panel1.Controls.Add(this.labelServerIP);
            this.splitContainer2.Panel1.Controls.Add(this.labelPlayers);
            this.splitContainer2.Panel1.Controls.Add(this.buttonQuit);
            this.splitContainer2.Panel1.Controls.Add(this.buttonsurrender);
            this.splitContainer2.Panel1.Controls.Add(this.buttonchallenge);
            this.splitContainer2.Panel1.Controls.Add(this.buttonLogin);
            this.splitContainer2.Panel1MinSize = 0;
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.listViewClient);
            this.splitContainer2.Panel2MinSize = 0;
            this.splitContainer2.Size = new System.Drawing.Size(379, 936);
            this.splitContainer2.SplitterDistance = 201;
            this.splitContainer2.TabIndex = 0;
            this.splitContainer2.TabStop = false;
            // 
            // numricServerPort
            // 
            this.numricServerPort.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.numricServerPort.Location = new System.Drawing.Point(141, 49);
            this.numricServerPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numricServerPort.Name = "numricServerPort";
            this.numricServerPort.Size = new System.Drawing.Size(216, 25);
            this.numricServerPort.TabIndex = 1;
            this.numricServerPort.Value = new decimal(new int[] {
            8012,
            0,
            0,
            0});
            // 
            // labelNickName
            // 
            this.labelNickName.AutoSize = true;
            this.labelNickName.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelNickName.Location = new System.Drawing.Point(76, 83);
            this.labelNickName.Name = "labelNickName";
            this.labelNickName.Size = new System.Drawing.Size(58, 21);
            this.labelNickName.TabIndex = 9;
            this.labelNickName.Text = "昵称：";
            // 
            // textBoxNickName
            // 
            this.textBoxNickName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textBoxNickName.Location = new System.Drawing.Point(140, 83);
            this.textBoxNickName.Name = "textBoxNickName";
            this.textBoxNickName.Size = new System.Drawing.Size(217, 25);
            this.textBoxNickName.TabIndex = 2;
            // 
            // textboxServerIp
            // 
            this.textboxServerIp.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.textboxServerIp.Location = new System.Drawing.Point(140, 16);
            this.textboxServerIp.Name = "textboxServerIp";
            this.textboxServerIp.Size = new System.Drawing.Size(217, 25);
            this.textboxServerIp.TabIndex = 0;
            this.textboxServerIp.Text = "115.28.36.39";
            this.textboxServerIp.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBoxServerIP_KeyPress);
            // 
            // labelServerPort
            // 
            this.labelServerPort.AutoSize = true;
            this.labelServerPort.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelServerPort.Location = new System.Drawing.Point(13, 49);
            this.labelServerPort.Name = "labelServerPort";
            this.labelServerPort.Size = new System.Drawing.Size(122, 21);
            this.labelServerPort.TabIndex = 6;
            this.labelServerPort.Text = "服务器端口号：";
            // 
            // labelServerIP
            // 
            this.labelServerIP.AutoSize = true;
            this.labelServerIP.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelServerIP.Location = new System.Drawing.Point(13, 16);
            this.labelServerIP.Name = "labelServerIP";
            this.labelServerIP.Size = new System.Drawing.Size(121, 21);
            this.labelServerIP.TabIndex = 5;
            this.labelServerIP.Text = "服务器IP地址：";
            // 
            // labelPlayers
            // 
            this.labelPlayers.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelPlayers.AutoSize = true;
            this.labelPlayers.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelPlayers.Location = new System.Drawing.Point(-4, 180);
            this.labelPlayers.Name = "labelPlayers";
            this.labelPlayers.Size = new System.Drawing.Size(138, 21);
            this.labelPlayers.TabIndex = 4;
            this.labelPlayers.Text = "游戏大厅用户列表";
            // 
            // buttonQuit
            // 
            this.buttonQuit.Enabled = false;
            this.buttonQuit.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonQuit.Location = new System.Drawing.Point(284, 126);
            this.buttonQuit.Name = "buttonQuit";
            this.buttonQuit.Size = new System.Drawing.Size(73, 33);
            this.buttonQuit.TabIndex = 6;
            this.buttonQuit.Text = "退出大厅";
            this.buttonQuit.UseVisualStyleBackColor = true;
            this.buttonQuit.Click += new System.EventHandler(this.buttonQuit_Click);
            // 
            // buttonsurrender
            // 
            this.buttonsurrender.Enabled = false;
            this.buttonsurrender.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonsurrender.Location = new System.Drawing.Point(195, 126);
            this.buttonsurrender.Name = "buttonsurrender";
            this.buttonsurrender.Size = new System.Drawing.Size(73, 33);
            this.buttonsurrender.TabIndex = 5;
            this.buttonsurrender.Text = "缴械投降";
            this.buttonsurrender.UseVisualStyleBackColor = true;
            // 
            // buttonchallenge
            // 
            this.buttonchallenge.Enabled = false;
            this.buttonchallenge.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonchallenge.Location = new System.Drawing.Point(106, 126);
            this.buttonchallenge.Name = "buttonchallenge";
            this.buttonchallenge.Size = new System.Drawing.Size(73, 33);
            this.buttonchallenge.TabIndex = 4;
            this.buttonchallenge.Text = "下挑战书";
            this.buttonchallenge.UseVisualStyleBackColor = true;
            this.buttonchallenge.Click += new System.EventHandler(this.buttonchallenge_Click);
            // 
            // buttonLogin
            // 
            this.buttonLogin.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonLogin.Location = new System.Drawing.Point(17, 126);
            this.buttonLogin.Name = "buttonLogin";
            this.buttonLogin.Size = new System.Drawing.Size(73, 33);
            this.buttonLogin.TabIndex = 3;
            this.buttonLogin.Text = "登陆大厅";
            this.buttonLogin.UseVisualStyleBackColor = true;
            this.buttonLogin.Click += new System.EventHandler(this.button1_Click);
            // 
            // listViewClient
            // 
            this.listViewClient.BackColor = System.Drawing.Color.WhiteSmoke;
            this.listViewClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.nickname,
            this.IP,
            this.port,
            this.state});
            this.listViewClient.Dock = System.Windows.Forms.DockStyle.Fill;
            this.listViewClient.FullRowSelect = true;
            this.listViewClient.Location = new System.Drawing.Point(0, 0);
            this.listViewClient.Name = "listViewClient";
            this.listViewClient.Size = new System.Drawing.Size(379, 731);
            this.listViewClient.TabIndex = 0;
            this.listViewClient.TabStop = false;
            this.listViewClient.UseCompatibleStateImageBehavior = false;
            this.listViewClient.View = System.Windows.Forms.View.Details;
            // 
            // nickname
            // 
            this.nickname.Text = "昵称";
            this.nickname.Width = 99;
            // 
            // IP
            // 
            this.IP.Text = "IP地址";
            this.IP.Width = 119;
            // 
            // port
            // 
            this.port.Text = "端口";
            this.port.Width = 69;
            // 
            // state
            // 
            this.state.Text = "状态";
            this.state.Width = 58;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "*.chs";
            this.openFileDialog1.Filter = "残局文件|*.chs|所有文件|*.*";
            this.openFileDialog1.FilterIndex = 2;
            this.openFileDialog1.InitialDirectory = "C:\\";
            this.openFileDialog1.Title = "保存残局";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.FileName = "无标题.chs";
            this.saveFileDialog1.Filter = "残局文件|*.chs";
            this.saveFileDialog1.InitialDirectory = "C:\\";
            // 
            // buttonRandomName
            // 
            this.buttonRandomName.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.buttonRandomName.Location = new System.Drawing.Point(17, 78);
            this.buttonRandomName.Name = "buttonRandomName";
            this.buttonRandomName.Size = new System.Drawing.Size(53, 33);
            this.buttonRandomName.TabIndex = 10;
            this.buttonRandomName.TabStop = false;
            this.buttonRandomName.Text = "随机";
            this.buttonRandomName.UseVisualStyleBackColor = true;
            this.buttonRandomName.Click += new System.EventHandler(this.buttonRandomName_Click);
            // 
            // MenuItemvsc
            // 
            this.MenuItemvsc.Name = "MenuItemvsc";
            this.MenuItemvsc.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.P)));
            this.MenuItemvsc.Size = new System.Drawing.Size(171, 22);
            this.MenuItemvsc.Text = "人机";
            this.MenuItemvsc.Click += new System.EventHandler(this.MenuItemvsc_Click);
            // 
            // webBrowser1
            // 
            this.webBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBrowser1.Location = new System.Drawing.Point(0, 0);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(974, 936);
            this.webBrowser1.TabIndex = 1;
            this.webBrowser1.Url = new System.Uri("http://www.acmsaga.top/xiangqi", System.UriKind.Absolute);
            this.webBrowser1.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1357, 960);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "中国象棋";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.PerformLayout();
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numricServerPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemGame;
        private System.Windows.Forms.ToolStripMenuItem MenuItemStart;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ListView listViewClient;
        private System.Windows.Forms.ColumnHeader nickname;
        private System.Windows.Forms.ColumnHeader IP;
        private System.Windows.Forms.ColumnHeader port;
        private System.Windows.Forms.ColumnHeader state;
        private System.Windows.Forms.Button buttonLogin;
        private System.Windows.Forms.Label labelServerIP;
        private System.Windows.Forms.Label labelPlayers;
        private System.Windows.Forms.Button buttonQuit;
        private System.Windows.Forms.Button buttonsurrender;
        private System.Windows.Forms.Button buttonchallenge;
        private System.Windows.Forms.Label labelServerPort;
        private System.Windows.Forms.Label labelNickName;
        private System.Windows.Forms.TextBox textBoxNickName;
        private System.Windows.Forms.TextBox textboxServerIp;
        private System.Windows.Forms.NumericUpDown numricServerPort;
        private System.Windows.Forms.ToolStripMenuItem MenuItemUndo;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpen;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.Button buttonRandomName;
        private System.Windows.Forms.ToolStripMenuItem MenuItemvsc;
        private System.Windows.Forms.WebBrowser webBrowser1;
    }
}

