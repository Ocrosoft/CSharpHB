namespace DrawBoard
{
    partial class DlgDrawTool
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DlgDrawTool));
            this.buttonLine = new System.Windows.Forms.Button();
            this.buttonRegtangle = new System.Windows.Forms.Button();
            this.buttonCircle = new System.Windows.Forms.Button();
            this.buttonSketch = new System.Windows.Forms.Button();
            this.buttonWidth = new System.Windows.Forms.Button();
            this.buttonColor = new System.Windows.Forms.Button();
            this.buttonExit = new System.Windows.Forms.Button();
            this.buttonSaveAs = new System.Windows.Forms.Button();
            this.buttonSave = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.buttonUndo = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.buttonfocus = new System.Windows.Forms.Button();
            this.buttoncheck = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // buttonLine
            // 
            this.buttonLine.Image = global::DrawBoard.Properties.Resources.Line_16;
            this.buttonLine.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonLine.Location = new System.Drawing.Point(21, 12);
            this.buttonLine.Name = "buttonLine";
            this.buttonLine.Size = new System.Drawing.Size(139, 40);
            this.buttonLine.TabIndex = 5;
            this.buttonLine.Text = "直线";
            this.buttonLine.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonLine.UseVisualStyleBackColor = true;
            this.buttonLine.Click += new System.EventHandler(this.buttonLine_Click);
            this.buttonLine.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonRegtangle
            // 
            this.buttonRegtangle.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonRegtangle.Image = global::DrawBoard.Properties.Resources.Regtangle__16;
            this.buttonRegtangle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonRegtangle.Location = new System.Drawing.Point(21, 58);
            this.buttonRegtangle.Name = "buttonRegtangle";
            this.buttonRegtangle.Size = new System.Drawing.Size(139, 40);
            this.buttonRegtangle.TabIndex = 6;
            this.buttonRegtangle.Text = "矩形";
            this.buttonRegtangle.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonRegtangle.UseVisualStyleBackColor = true;
            this.buttonRegtangle.Click += new System.EventHandler(this.buttonRegtangle_Click);
            this.buttonRegtangle.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonCircle
            // 
            this.buttonCircle.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonCircle.Image = global::DrawBoard.Properties.Resources.Circle_16;
            this.buttonCircle.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonCircle.Location = new System.Drawing.Point(21, 104);
            this.buttonCircle.Name = "buttonCircle";
            this.buttonCircle.Size = new System.Drawing.Size(139, 40);
            this.buttonCircle.TabIndex = 7;
            this.buttonCircle.Text = "                        圆";
            this.buttonCircle.UseVisualStyleBackColor = true;
            this.buttonCircle.Click += new System.EventHandler(this.buttonCircle_Click);
            this.buttonCircle.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonSketch
            // 
            this.buttonSketch.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonSketch.Image = global::DrawBoard.Properties.Resources.Sketch_16;
            this.buttonSketch.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSketch.Location = new System.Drawing.Point(21, 150);
            this.buttonSketch.Name = "buttonSketch";
            this.buttonSketch.Size = new System.Drawing.Size(139, 40);
            this.buttonSketch.TabIndex = 8;
            this.buttonSketch.Text = "徒手画";
            this.buttonSketch.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSketch.UseVisualStyleBackColor = true;
            this.buttonSketch.Click += new System.EventHandler(this.buttonSketch_Click);
            this.buttonSketch.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonWidth
            // 
            this.buttonWidth.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonWidth.Image = global::DrawBoard.Properties.Resources.Width_16;
            this.buttonWidth.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonWidth.Location = new System.Drawing.Point(21, 288);
            this.buttonWidth.Name = "buttonWidth";
            this.buttonWidth.Size = new System.Drawing.Size(139, 40);
            this.buttonWidth.TabIndex = 11;
            this.buttonWidth.Text = "线宽";
            this.buttonWidth.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonWidth.UseVisualStyleBackColor = true;
            this.buttonWidth.Click += new System.EventHandler(this.buttonWidth_Click);
            this.buttonWidth.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonColor
            // 
            this.buttonColor.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonColor.Image = global::DrawBoard.Properties.Resources.Color_16;
            this.buttonColor.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonColor.Location = new System.Drawing.Point(21, 334);
            this.buttonColor.Name = "buttonColor";
            this.buttonColor.Size = new System.Drawing.Size(139, 40);
            this.buttonColor.TabIndex = 12;
            this.buttonColor.Text = "颜色";
            this.buttonColor.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonColor.UseVisualStyleBackColor = true;
            this.buttonColor.Click += new System.EventHandler(this.buttonColor_Click);
            this.buttonColor.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonExit
            // 
            this.buttonExit.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonExit.Image = global::DrawBoard.Properties.Resources.Close_16;
            this.buttonExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonExit.Location = new System.Drawing.Point(21, 472);
            this.buttonExit.Name = "buttonExit";
            this.buttonExit.Size = new System.Drawing.Size(139, 40);
            this.buttonExit.TabIndex = 12;
            this.buttonExit.Text = "退出";
            this.buttonExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonExit.UseVisualStyleBackColor = true;
            this.buttonExit.Click += new System.EventHandler(this.buttonExit_Click);
            this.buttonExit.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonSaveAs
            // 
            this.buttonSaveAs.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonSaveAs.Image = global::DrawBoard.Properties.Resources.SaveAs_16;
            this.buttonSaveAs.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSaveAs.Location = new System.Drawing.Point(21, 426);
            this.buttonSaveAs.Name = "buttonSaveAs";
            this.buttonSaveAs.Size = new System.Drawing.Size(139, 40);
            this.buttonSaveAs.TabIndex = 13;
            this.buttonSaveAs.Text = "另存为";
            this.buttonSaveAs.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSaveAs.UseVisualStyleBackColor = true;
            this.buttonSaveAs.Click += new System.EventHandler(this.buttonSaveAs_Click);
            this.buttonSaveAs.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonSave
            // 
            this.buttonSave.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonSave.Image = global::DrawBoard.Properties.Resources.Save_16;
            this.buttonSave.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonSave.Location = new System.Drawing.Point(21, 380);
            this.buttonSave.Name = "buttonSave";
            this.buttonSave.Size = new System.Drawing.Size(139, 40);
            this.buttonSave.TabIndex = 13;
            this.buttonSave.Text = "保存";
            this.buttonSave.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonSave.UseVisualStyleBackColor = true;
            this.buttonSave.Click += new System.EventHandler(this.buttonSave_Click);
            this.buttonSave.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonStop
            // 
            this.buttonStop.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonStop.Image = global::DrawBoard.Properties.Resources.Stop_16;
            this.buttonStop.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonStop.Location = new System.Drawing.Point(21, 196);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(139, 40);
            this.buttonStop.TabIndex = 13;
            this.buttonStop.Text = "停止";
            this.buttonStop.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            this.buttonStop.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // buttonUndo
            // 
            this.buttonUndo.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttonUndo.Image = global::DrawBoard.Properties.Resources.Undo_16;
            this.buttonUndo.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonUndo.Location = new System.Drawing.Point(21, 242);
            this.buttonUndo.Name = "buttonUndo";
            this.buttonUndo.Size = new System.Drawing.Size(139, 40);
            this.buttonUndo.TabIndex = 13;
            this.buttonUndo.Text = "撤销";
            this.buttonUndo.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonUndo.UseVisualStyleBackColor = true;
            this.buttonUndo.Click += new System.EventHandler(this.buttonUndo_Click);
            this.buttonUndo.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 1000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // buttonfocus
            // 
            this.buttonfocus.Image = global::DrawBoard.Properties.Resources.Line_16;
            this.buttonfocus.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttonfocus.Location = new System.Drawing.Point(3, 489);
            this.buttonfocus.Name = "buttonfocus";
            this.buttonfocus.Size = new System.Drawing.Size(0, 0);
            this.buttonfocus.TabIndex = 14;
            this.buttonfocus.Text = "停止是的焦点";
            this.buttonfocus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttonfocus.UseVisualStyleBackColor = true;
            // 
            // buttoncheck
            // 
            this.buttoncheck.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.buttoncheck.Image = global::DrawBoard.Properties.Resources.Close_16;
            this.buttoncheck.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.buttoncheck.Location = new System.Drawing.Point(3, 288);
            this.buttoncheck.Name = "buttoncheck";
            this.buttoncheck.Size = new System.Drawing.Size(0, 0);
            this.buttoncheck.TabIndex = 15;
            this.buttoncheck.Text = "保证checkstatus的正确性";
            this.buttoncheck.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.buttoncheck.UseVisualStyleBackColor = true;
            this.buttoncheck.Click += new System.EventHandler(this.buttoncheck_Click);
            // 
            // DlgDrawTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(180, 526);
            this.Controls.Add(this.buttoncheck);
            this.Controls.Add(this.buttonfocus);
            this.Controls.Add(this.buttonUndo);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonSave);
            this.Controls.Add(this.buttonSaveAs);
            this.Controls.Add(this.buttonLine);
            this.Controls.Add(this.buttonRegtangle);
            this.Controls.Add(this.buttonCircle);
            this.Controls.Add(this.buttonSketch);
            this.Controls.Add(this.buttonWidth);
            this.Controls.Add(this.buttonExit);
            this.Controls.Add(this.buttonColor);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(202, 582);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(202, 582);
            this.Name = "DlgDrawTool";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            this.Text = "绘图工具箱";
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DlgDrawTool_FormClosed);
            this.Load += new System.EventHandler(this.DlgDrawTool_Load);
            this.MouseEnter += new System.EventHandler(this.DlgDrawTool_MouseEnter);
            this.MouseLeave += new System.EventHandler(this.DlgDrawTool_MouseLeave);
            this.Move += new System.EventHandler(this.DlgDrawTool_Move);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.Button buttonLine;
        public System.Windows.Forms.Button buttonRegtangle;
        public System.Windows.Forms.Button buttonCircle;
        public System.Windows.Forms.Button buttonSketch;
        public System.Windows.Forms.Button buttonWidth;
        public System.Windows.Forms.Button buttonColor;
        public System.Windows.Forms.Button buttonExit;
        public System.Windows.Forms.Button buttonSaveAs;
        public System.Windows.Forms.Button buttonSave;
        public System.Windows.Forms.Button buttonStop;
        public System.Windows.Forms.Button buttonUndo;
        public System.Windows.Forms.Timer timer1;
        public System.Windows.Forms.Button buttonfocus;
        public System.Windows.Forms.Button buttoncheck;
    }
}