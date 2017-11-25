namespace Timer
{
    partial class Form1
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.labelHour = new System.Windows.Forms.Label();
            this.labelCol1 = new System.Windows.Forms.Label();
            this.labelCol2 = new System.Windows.Forms.Label();
            this.labelMin = new System.Windows.Forms.Label();
            this.labelSec = new System.Windows.Forms.Label();
            this.buttonStart = new System.Windows.Forms.Button();
            this.buttonPause = new System.Windows.Forms.Button();
            this.buttonStop = new System.Windows.Forms.Button();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // labelHour
            // 
            this.labelHour.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.labelHour.Location = new System.Drawing.Point(33, 76);
            this.labelHour.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelHour.Name = "labelHour";
            this.labelHour.Size = new System.Drawing.Size(85, 67);
            this.labelHour.TabIndex = 0;
            this.labelHour.Text = "00";
            // 
            // labelCol1
            // 
            this.labelCol1.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.labelCol1.Location = new System.Drawing.Point(111, 71);
            this.labelCol1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCol1.Name = "labelCol1";
            this.labelCol1.Size = new System.Drawing.Size(33, 67);
            this.labelCol1.TabIndex = 1;
            this.labelCol1.Text = ":";
            // 
            // labelCol2
            // 
            this.labelCol2.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.labelCol2.Location = new System.Drawing.Point(224, 71);
            this.labelCol2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelCol2.Name = "labelCol2";
            this.labelCol2.Size = new System.Drawing.Size(33, 67);
            this.labelCol2.TabIndex = 3;
            this.labelCol2.Text = ":";
            // 
            // labelMin
            // 
            this.labelMin.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.labelMin.Location = new System.Drawing.Point(147, 76);
            this.labelMin.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelMin.Name = "labelMin";
            this.labelMin.Size = new System.Drawing.Size(87, 67);
            this.labelMin.TabIndex = 2;
            this.labelMin.Text = "00";
            // 
            // labelSec
            // 
            this.labelSec.Font = new System.Drawing.Font("微软雅黑", 35F);
            this.labelSec.Location = new System.Drawing.Point(256, 76);
            this.labelSec.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.labelSec.Name = "labelSec";
            this.labelSec.Size = new System.Drawing.Size(131, 67);
            this.labelSec.TabIndex = 4;
            this.labelSec.Text = "00.0";
            // 
            // buttonStart
            // 
            this.buttonStart.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonStart.Location = new System.Drawing.Point(32, 152);
            this.buttonStart.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(81, 48);
            this.buttonStart.TabIndex = 5;
            this.buttonStart.Text = "开始";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // buttonPause
            // 
            this.buttonPause.Enabled = false;
            this.buttonPause.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonPause.Location = new System.Drawing.Point(143, 152);
            this.buttonPause.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonPause.Name = "buttonPause";
            this.buttonPause.Size = new System.Drawing.Size(81, 48);
            this.buttonPause.TabIndex = 5;
            this.buttonPause.Text = "暂停";
            this.buttonPause.UseVisualStyleBackColor = true;
            this.buttonPause.Click += new System.EventHandler(this.buttonPause_Click);
            // 
            // buttonStop
            // 
            this.buttonStop.Enabled = false;
            this.buttonStop.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.buttonStop.Location = new System.Drawing.Point(255, 152);
            this.buttonStop.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.buttonStop.Name = "buttonStop";
            this.buttonStop.Size = new System.Drawing.Size(110, 48);
            this.buttonStop.TabIndex = 5;
            this.buttonStop.Text = "停止";
            this.buttonStop.UseVisualStyleBackColor = true;
            this.buttonStop.Click += new System.EventHandler(this.buttonStop_Click);
            // 
            // timer1
            // 
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("微软雅黑", 25F);
            this.textBox1.Location = new System.Drawing.Point(43, 21);
            this.textBox1.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(127, 51);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "0";
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("微软雅黑", 25F);
            this.label1.Location = new System.Drawing.Point(169, 21);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(217, 67);
            this.label1.TabIndex = 4;
            this.label1.Text = "          秒表";
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // Form1
            // 
            this.AcceptButton = this.buttonStart;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(398, 227);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.buttonStop);
            this.Controls.Add(this.buttonPause);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelSec);
            this.Controls.Add(this.labelMin);
            this.Controls.Add(this.labelHour);
            this.Controls.Add(this.labelCol1);
            this.Controls.Add(this.labelCol2);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(414, 266);
            this.MinimumSize = new System.Drawing.Size(414, 266);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "计时器";
            this.TopMost = true;
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label labelHour;
        private System.Windows.Forms.Label labelCol1;
        private System.Windows.Forms.Label labelCol2;
        private System.Windows.Forms.Label labelMin;
        private System.Windows.Forms.Label labelSec;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.Button buttonPause;
        private System.Windows.Forms.Button buttonStop;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}

