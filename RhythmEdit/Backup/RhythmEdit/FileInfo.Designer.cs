namespace RhythmEdit
{
    partial class FileInfo
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
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.makerLabel = new System.Windows.Forms.Label();
            this.fileNameLabel = new System.Windows.Forms.Label();
            this.diffLabel = new System.Windows.Forms.Label();
            this.fileName = new System.Windows.Forms.TextBox();
            this.maker = new System.Windows.Forms.TextBox();
            this.Diff = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button1.Location = new System.Drawing.Point(32, 152);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 44);
            this.button1.TabIndex = 0;
            this.button1.Text = "确定";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.button2.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.Location = new System.Drawing.Point(189, 152);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 44);
            this.button2.TabIndex = 1;
            this.button2.Text = "取消";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // makerLabel
            // 
            this.makerLabel.AutoSize = true;
            this.makerLabel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.makerLabel.Location = new System.Drawing.Point(27, 61);
            this.makerLabel.Name = "makerLabel";
            this.makerLabel.Size = new System.Drawing.Size(69, 25);
            this.makerLabel.TabIndex = 2;
            this.makerLabel.Text = "作者：";
            // 
            // fileNameLabel
            // 
            this.fileNameLabel.AutoSize = true;
            this.fileNameLabel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileNameLabel.Location = new System.Drawing.Point(27, 20);
            this.fileNameLabel.Name = "fileNameLabel";
            this.fileNameLabel.Size = new System.Drawing.Size(69, 25);
            this.fileNameLabel.TabIndex = 3;
            this.fileNameLabel.Text = "曲名：";
            // 
            // diffLabel
            // 
            this.diffLabel.AutoSize = true;
            this.diffLabel.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.diffLabel.Location = new System.Drawing.Point(27, 102);
            this.diffLabel.Name = "diffLabel";
            this.diffLabel.Size = new System.Drawing.Size(69, 25);
            this.diffLabel.TabIndex = 4;
            this.diffLabel.Text = "难度：";
            // 
            // fileName
            // 
            this.fileName.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.fileName.Location = new System.Drawing.Point(101, 16);
            this.fileName.Name = "fileName";
            this.fileName.Size = new System.Drawing.Size(209, 29);
            this.fileName.TabIndex = 5;
            // 
            // maker
            // 
            this.maker.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.maker.Location = new System.Drawing.Point(101, 57);
            this.maker.Name = "maker";
            this.maker.Size = new System.Drawing.Size(209, 29);
            this.maker.TabIndex = 6;
            // 
            // Diff
            // 
            this.Diff.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Diff.FormattingEnabled = true;
            this.Diff.Items.AddRange(new object[] {
            "简单",
            "中等",
            "困难"});
            this.Diff.Location = new System.Drawing.Point(101, 98);
            this.Diff.MaxDropDownItems = 3;
            this.Diff.Name = "Diff";
            this.Diff.Size = new System.Drawing.Size(209, 29);
            this.Diff.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.button3.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.Location = new System.Drawing.Point(32, 152);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(121, 44);
            this.button3.TabIndex = 8;
            this.button3.Text = "确定";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // FileInfo
            // 
            this.AcceptButton = this.button1;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.button2;
            this.ClientSize = new System.Drawing.Size(354, 210);
            this.Controls.Add(this.Diff);
            this.Controls.Add(this.maker);
            this.Controls.Add(this.fileName);
            this.Controls.Add(this.diffLabel);
            this.Controls.Add(this.fileNameLabel);
            this.Controls.Add(this.makerLabel);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.button3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FileInfo";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "文件信息";
            this.Load += new System.EventHandler(this.FileInfo_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label makerLabel;
        private System.Windows.Forms.Label fileNameLabel;
        private System.Windows.Forms.Label diffLabel;
        public System.Windows.Forms.TextBox fileName;
        public System.Windows.Forms.TextBox maker;
        public System.Windows.Forms.ComboBox Diff;
        private System.Windows.Forms.Button button3;
    }
}