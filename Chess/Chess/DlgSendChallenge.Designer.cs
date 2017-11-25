namespace Chess
{
    partial class DlgSendChallenge
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
            this.labelInfo = new System.Windows.Forms.Label();
            this.buttonCancle = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // labelInfo
            // 
            this.labelInfo.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.labelInfo.Location = new System.Drawing.Point(19, 9);
            this.labelInfo.Name = "labelInfo";
            this.labelInfo.Size = new System.Drawing.Size(381, 65);
            this.labelInfo.TabIndex = 0;
            this.labelInfo.Text = "你已经发出了挑战书，\r\n请耐心等待对方回应。";
            this.labelInfo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // buttonCancle
            // 
            this.buttonCancle.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.buttonCancle.Location = new System.Drawing.Point(132, 78);
            this.buttonCancle.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.buttonCancle.Name = "buttonCancle";
            this.buttonCancle.Size = new System.Drawing.Size(138, 45);
            this.buttonCancle.TabIndex = 1;
            this.buttonCancle.Text = "撤回挑战书";
            this.buttonCancle.UseVisualStyleBackColor = true;
            // 
            // DlgSendChallenge
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.buttonCancle;
            this.ClientSize = new System.Drawing.Size(405, 136);
            this.Controls.Add(this.buttonCancle);
            this.Controls.Add(this.labelInfo);
            this.Font = new System.Drawing.Font("微软雅黑", 9F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "DlgSendChallenge";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "下挑战书";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label labelInfo;
        private System.Windows.Forms.Button buttonCancle;
    }
}