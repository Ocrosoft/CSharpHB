namespace ChessServer
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
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.buttonStart = new System.Windows.Forms.Button();
            this.labelIP = new System.Windows.Forms.Label();
            this.labelPort = new System.Windows.Forms.Label();
            this.comboBoxIP = new System.Windows.Forms.ComboBox();
            this.numericUpDownPort = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.labelClientCnt = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.listViewClient = new System.Windows.Forms.ListView();
            this.columnNickname = new System.Windows.Forms.ColumnHeader();
            this.columnIP = new System.Windows.Forms.ColumnHeader();
            this.columnPort = new System.Windows.Forms.ColumnHeader();
            this.columnState = new System.Windows.Forms.ColumnHeader();
            this.listViewPacket = new System.Windows.Forms.ListView();
            this.columnPacket = new System.Windows.Forms.ColumnHeader();
            this.textBox1 = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonStart
            // 
            this.buttonStart.Location = new System.Drawing.Point(291, 18);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(121, 76);
            this.buttonStart.TabIndex = 0;
            this.buttonStart.Text = "开始监听";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // labelIP
            // 
            this.labelIP.AutoSize = true;
            this.labelIP.Location = new System.Drawing.Point(30, 21);
            this.labelIP.Name = "labelIP";
            this.labelIP.Size = new System.Drawing.Size(95, 20);
            this.labelIP.TabIndex = 1;
            this.labelIP.Text = "服务器IP地址:";
            // 
            // labelPort
            // 
            this.labelPort.AutoSize = true;
            this.labelPort.Location = new System.Drawing.Point(30, 72);
            this.labelPort.Name = "labelPort";
            this.labelPort.Size = new System.Drawing.Size(82, 20);
            this.labelPort.TabIndex = 2;
            this.labelPort.Text = "监听端口号:";
            // 
            // comboBoxIP
            // 
            this.comboBoxIP.FormattingEnabled = true;
            this.comboBoxIP.Location = new System.Drawing.Point(131, 18);
            this.comboBoxIP.Name = "comboBoxIP";
            this.comboBoxIP.Size = new System.Drawing.Size(141, 27);
            this.comboBoxIP.TabIndex = 3;
            // 
            // numericUpDownPort
            // 
            this.numericUpDownPort.Location = new System.Drawing.Point(131, 69);
            this.numericUpDownPort.Maximum = new decimal(new int[] {
            65535,
            0,
            0,
            0});
            this.numericUpDownPort.Name = "numericUpDownPort";
            this.numericUpDownPort.Size = new System.Drawing.Size(141, 25);
            this.numericUpDownPort.TabIndex = 4;
            this.numericUpDownPort.Value = new decimal(new int[] {
            888,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 110);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 20);
            this.label1.TabIndex = 2;
            this.label1.Text = "在线客户端列表：";
            // 
            // labelClientCnt
            // 
            this.labelClientCnt.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.labelClientCnt.AutoSize = true;
            this.labelClientCnt.Location = new System.Drawing.Point(311, 110);
            this.labelClientCnt.Name = "labelClientCnt";
            this.labelClientCnt.Size = new System.Drawing.Size(101, 20);
            this.labelClientCnt.TabIndex = 5;
            this.labelClientCnt.Text = "客户端数量：0";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(30, 350);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "数据包列表：";
            // 
            // listViewClient
            // 
            this.listViewClient.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnNickname,
            this.columnIP,
            this.columnPort,
            this.columnState});
            this.listViewClient.Location = new System.Drawing.Point(34, 145);
            this.listViewClient.Name = "listViewClient";
            this.listViewClient.Size = new System.Drawing.Size(378, 202);
            this.listViewClient.TabIndex = 7;
            this.listViewClient.UseCompatibleStateImageBehavior = false;
            this.listViewClient.View = System.Windows.Forms.View.Details;
            // 
            // columnNickname
            // 
            this.columnNickname.Text = "昵称";
            // 
            // columnIP
            // 
            this.columnIP.Text = "IP地址";
            // 
            // columnPort
            // 
            this.columnPort.Text = "端口";
            // 
            // columnState
            // 
            this.columnState.Text = "状态";
            // 
            // listViewPacket
            // 
            this.listViewPacket.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnPacket});
            this.listViewPacket.Location = new System.Drawing.Point(34, 381);
            this.listViewPacket.Name = "listViewPacket";
            this.listViewPacket.Size = new System.Drawing.Size(378, 172);
            this.listViewPacket.TabIndex = 8;
            this.listViewPacket.UseCompatibleStateImageBehavior = false;
            this.listViewPacket.View = System.Windows.Forms.View.Details;
            // 
            // columnPacket
            // 
            this.columnPacket.Text = "数据包";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(12, 202);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox1.Size = new System.Drawing.Size(417, 95);
            this.textBox1.TabIndex = 9;
            this.textBox1.Visible = false;
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(441, 573);
            this.Controls.Add(this.listViewPacket);
            this.Controls.Add(this.listViewClient);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.labelClientCnt);
            this.Controls.Add(this.numericUpDownPort);
            this.Controls.Add(this.comboBoxIP);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelPort);
            this.Controls.Add(this.labelIP);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBox1);
            this.Font = new System.Drawing.Font("微软雅黑", 10F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.Name = "FormMain";
            this.Text = "服务端";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormMain_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDownPort)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Label labelIP;
        private System.Windows.Forms.Label labelPort;
        private System.Windows.Forms.ComboBox comboBoxIP;
        private System.Windows.Forms.NumericUpDown numericUpDownPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label labelClientCnt;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ListView listViewClient;
        private System.Windows.Forms.ListView listViewPacket;
        private System.Windows.Forms.ColumnHeader columnNickname;
        private System.Windows.Forms.ColumnHeader columnIP;
        private System.Windows.Forms.ColumnHeader columnPort;
        private System.Windows.Forms.ColumnHeader columnState;
        private System.Windows.Forms.ColumnHeader columnPacket;
        private System.Windows.Forms.TextBox textBox1;
    }
}

