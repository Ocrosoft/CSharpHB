namespace Rhythm_Ocrosoft
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormMain));
            this.pictureBoxGame = new System.Windows.Forms.PictureBox();
            this.openFile = new System.Windows.Forms.OpenFileDialog();
            this.timerReady = new System.Windows.Forms.Timer(this.components);
            this.timerSetting = new System.Windows.Forms.Timer(this.components);
            this.timerWait = new System.Windows.Forms.Timer(this.components);
            this.timerOpacityUP = new System.Windows.Forms.Timer(this.components);
            this.timerOpacitiDOWN = new System.Windows.Forms.Timer(this.components);
            this.musicPlayerTrue = new AxWMPLib.AxWindowsMediaPlayer();
            this.musicPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.timerGameCtl = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGame)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicPlayerTrue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicPlayer)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBoxGame
            // 
            this.pictureBoxGame.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxGame.Name = "pictureBoxGame";
            this.pictureBoxGame.Size = new System.Drawing.Size(784, 756);
            this.pictureBoxGame.TabIndex = 0;
            this.pictureBoxGame.TabStop = false;
            this.pictureBoxGame.Click += new System.EventHandler(this.pictureBoxGame_Click);
            this.pictureBoxGame.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxGame_Paint);
            this.pictureBoxGame.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxGame_MouseMove);
            // 
            // openFile
            // 
            this.openFile.FileName = "Sample";
            this.openFile.Filter = "音乐文件|*.rtm";
            // 
            // timerReady
            // 
            this.timerReady.Tick += new System.EventHandler(this.timerReady_Tick);
            // 
            // timerSetting
            // 
            this.timerSetting.Interval = 1;
            this.timerSetting.Tick += new System.EventHandler(this.timerSetting_Tick);
            // 
            // timerWait
            // 
            this.timerWait.Interval = 3500;
            this.timerWait.Tick += new System.EventHandler(this.timerWait_Tick);
            // 
            // timerOpacityUP
            // 
            this.timerOpacityUP.Interval = 1;
            this.timerOpacityUP.Tick += new System.EventHandler(this.timerOpacityUP_Tick);
            // 
            // timerOpacitiDOWN
            // 
            this.timerOpacitiDOWN.Interval = 1;
            this.timerOpacitiDOWN.Tick += new System.EventHandler(this.timerOpacitiDOWN_Tick);
            // 
            // musicPlayerTrue
            // 
            this.musicPlayerTrue.Enabled = true;
            this.musicPlayerTrue.Location = new System.Drawing.Point(790, 38);
            this.musicPlayerTrue.Name = "musicPlayerTrue";
            this.musicPlayerTrue.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("musicPlayerTrue.OcxState")));
            this.musicPlayerTrue.Size = new System.Drawing.Size(222, 32);
            this.musicPlayerTrue.TabIndex = 6;
            this.musicPlayerTrue.Visible = false;
            // 
            // musicPlayer
            // 
            this.musicPlayer.Enabled = true;
            this.musicPlayer.Location = new System.Drawing.Point(790, 0);
            this.musicPlayer.Name = "musicPlayer";
            this.musicPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("musicPlayer.OcxState")));
            this.musicPlayer.Size = new System.Drawing.Size(222, 32);
            this.musicPlayer.TabIndex = 5;
            this.musicPlayer.Visible = false;
            // 
            // timerGameCtl
            // 
            this.timerGameCtl.Interval = 1;
            this.timerGameCtl.Tick += new System.EventHandler(this.timerGameCtl_Tick);
            // 
            // FormMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.musicPlayerTrue);
            this.Controls.Add(this.musicPlayer);
            this.Controls.Add(this.pictureBoxGame);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormMain";
            this.Load += new System.EventHandler(this.FormMain_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormMain_KeyDown);
            this.MouseClick += new System.Windows.Forms.MouseEventHandler(this.FormMain_MouseClick);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxGame)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicPlayerTrue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.musicPlayer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxGame;
        private System.Windows.Forms.OpenFileDialog openFile;
        private AxWMPLib.AxWindowsMediaPlayer musicPlayer;
        private AxWMPLib.AxWindowsMediaPlayer musicPlayerTrue;
        private System.Windows.Forms.Timer timerReady;
        private System.Windows.Forms.Timer timerSetting;
        private System.Windows.Forms.Timer timerWait;
        private System.Windows.Forms.Timer timerOpacityUP;
        private System.Windows.Forms.Timer timerOpacitiDOWN;
        private System.Windows.Forms.Timer timerGameCtl;
    }
}

