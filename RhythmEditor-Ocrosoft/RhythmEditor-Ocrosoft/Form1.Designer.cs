namespace RhythmEditor_Ocrosoft
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
            this.pictureBoxWork = new System.Windows.Forms.PictureBox();
            this.panelMain = new System.Windows.Forms.Panel();
            this.panelTimeLineSpace = new System.Windows.Forms.Panel();
            this.pictureBoxTimeLine = new System.Windows.Forms.PictureBox();
            this.panelTime = new System.Windows.Forms.Panel();
            this.textBoxTimeInput = new System.Windows.Forms.TextBox();
            this.pictureBoxTime = new System.Windows.Forms.PictureBox();
            this.panelWork = new System.Windows.Forms.Panel();
            this.musicPlayer = new AxWMPLib.AxWindowsMediaPlayer();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.MenuItemFile = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNew = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemOpne = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemSave = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemClose = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.MenuItemExit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemEdit = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemView = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNestleUp = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemNestleDown = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBoxLabel = new System.Windows.Forms.PictureBox();
            this.openFileDlg = new System.Windows.Forms.OpenFileDialog();
            this.timerChangeCursorPosition = new System.Windows.Forms.Timer(this.components);
            this.timerTime = new System.Windows.Forms.Timer(this.components);
            this.timerChangeCursorPositionByDragCursor = new System.Windows.Forms.Timer(this.components);
            this.timerChangeStartCursor = new System.Windows.Forms.Timer(this.components);
            this.timerChangeEndCursor = new System.Windows.Forms.Timer(this.components);
            this.rightButtonMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuItemDeleteNote = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuItemClearSelect = new System.Windows.Forms.ToolStripMenuItem();
            this.timerMoveNote = new System.Windows.Forms.Timer(this.components);
            this.timerGetDuration = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWork)).BeginInit();
            this.panelMain.SuspendLayout();
            this.panelTimeLineSpace.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimeLine)).BeginInit();
            this.panelTime.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTime)).BeginInit();
            this.panelWork.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.musicPlayer)).BeginInit();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabel)).BeginInit();
            this.rightButtonMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBoxWork
            // 
            this.pictureBoxWork.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBoxWork.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxWork.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxWork.Name = "pictureBoxWork";
            this.pictureBoxWork.Size = new System.Drawing.Size(1024, 547);
            this.pictureBoxWork.TabIndex = 0;
            this.pictureBoxWork.TabStop = false;
            this.pictureBoxWork.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox1_Paint);
            this.pictureBoxWork.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWork_MouseDown);
            this.pictureBoxWork.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWork_MouseMove);
            this.pictureBoxWork.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxWork_MouseUp);
            // 
            // panelMain
            // 
            this.panelMain.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.panelMain.Controls.Add(this.panelTimeLineSpace);
            this.panelMain.Controls.Add(this.panelTime);
            this.panelMain.Controls.Add(this.panelWork);
            this.panelMain.Controls.Add(this.menuStrip1);
            this.panelMain.Controls.Add(this.panel1);
            this.panelMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelMain.Location = new System.Drawing.Point(0, 0);
            this.panelMain.Name = "panelMain";
            this.panelMain.Size = new System.Drawing.Size(1024, 768);
            this.panelMain.TabIndex = 1;
            // 
            // panelTimeLineSpace
            // 
            this.panelTimeLineSpace.Controls.Add(this.pictureBoxTimeLine);
            this.panelTimeLineSpace.Location = new System.Drawing.Point(0, 110);
            this.panelTimeLineSpace.Name = "panelTimeLineSpace";
            this.panelTimeLineSpace.Size = new System.Drawing.Size(1024, 47);
            this.panelTimeLineSpace.TabIndex = 3;
            // 
            // pictureBoxTimeLine
            // 
            this.pictureBoxTimeLine.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTimeLine.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTimeLine.Name = "pictureBoxTimeLine";
            this.pictureBoxTimeLine.Size = new System.Drawing.Size(1024, 47);
            this.pictureBoxTimeLine.TabIndex = 0;
            this.pictureBoxTimeLine.TabStop = false;
            this.pictureBoxTimeLine.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxTimeLine_Paint);
            this.pictureBoxTimeLine.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTimeLine_MouseDown);
            this.pictureBoxTimeLine.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTimeLine_MouseMove);
            this.pictureBoxTimeLine.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTimeLine_MouseUp);
            // 
            // panelTime
            // 
            this.panelTime.Controls.Add(this.textBoxTimeInput);
            this.panelTime.Controls.Add(this.pictureBoxTime);
            this.panelTime.Location = new System.Drawing.Point(0, 697);
            this.panelTime.Name = "panelTime";
            this.panelTime.Size = new System.Drawing.Size(1024, 71);
            this.panelTime.TabIndex = 2;
            // 
            // textBoxTimeInput
            // 
            this.textBoxTimeInput.Font = new System.Drawing.Font("微软雅黑", 25F);
            this.textBoxTimeInput.Location = new System.Drawing.Point(10, 10);
            this.textBoxTimeInput.Name = "textBoxTimeInput";
            this.textBoxTimeInput.Size = new System.Drawing.Size(167, 51);
            this.textBoxTimeInput.TabIndex = 1;
            this.textBoxTimeInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBoxTimeInput.Visible = false;
            this.textBoxTimeInput.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            this.textBoxTimeInput.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBoxTimeInput_KeyDown);
            // 
            // pictureBoxTime
            // 
            this.pictureBoxTime.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.pictureBoxTime.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxTime.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxTime.Name = "pictureBoxTime";
            this.pictureBoxTime.Size = new System.Drawing.Size(1024, 71);
            this.pictureBoxTime.TabIndex = 0;
            this.pictureBoxTime.TabStop = false;
            this.pictureBoxTime.Click += new System.EventHandler(this.pictureBoxTime_Click);
            this.pictureBoxTime.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBox2_Paint);
            this.pictureBoxTime.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTime_MouseDown);
            this.pictureBoxTime.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTime_MouseMove);
            this.pictureBoxTime.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxTime_MouseUp);
            // 
            // panelWork
            // 
            this.panelWork.Controls.Add(this.musicPlayer);
            this.panelWork.Controls.Add(this.pictureBoxWork);
            this.panelWork.Location = new System.Drawing.Point(0, 151);
            this.panelWork.Name = "panelWork";
            this.panelWork.Size = new System.Drawing.Size(1024, 547);
            this.panelWork.TabIndex = 1;
            // 
            // musicPlayer
            // 
            this.musicPlayer.Enabled = true;
            this.musicPlayer.Location = new System.Drawing.Point(298, 17);
            this.musicPlayer.Name = "musicPlayer";
            this.musicPlayer.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("musicPlayer.OcxState")));
            this.musicPlayer.Size = new System.Drawing.Size(398, 47);
            this.musicPlayer.TabIndex = 1;
            this.musicPlayer.Visible = false;
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemFile,
            this.MenuItemEdit,
            this.MenuItemView});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1024, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // MenuItemFile
            // 
            this.MenuItemFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNew,
            this.MenuItemOpne,
            this.toolStripSeparator2,
            this.MenuItemSave,
            this.MenuItemSaveAs,
            this.toolStripSeparator1,
            this.MenuItemClose,
            this.toolStripSeparator3,
            this.MenuItemExit});
            this.MenuItemFile.Name = "MenuItemFile";
            this.MenuItemFile.Size = new System.Drawing.Size(39, 21);
            this.MenuItemFile.Text = "File";
            // 
            // MenuItemNew
            // 
            this.MenuItemNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemNew.Name = "MenuItemNew";
            this.MenuItemNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.MenuItemNew.Size = new System.Drawing.Size(199, 22);
            this.MenuItemNew.Text = "New";
            // 
            // MenuItemOpne
            // 
            this.MenuItemOpne.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemOpne.Name = "MenuItemOpne";
            this.MenuItemOpne.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.MenuItemOpne.Size = new System.Drawing.Size(199, 22);
            this.MenuItemOpne.Text = "Open";
            this.MenuItemOpne.Click += new System.EventHandler(this.MenuItemOpne_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(196, 6);
            // 
            // MenuItemSave
            // 
            this.MenuItemSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemSave.Name = "MenuItemSave";
            this.MenuItemSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.MenuItemSave.Size = new System.Drawing.Size(199, 22);
            this.MenuItemSave.Text = "Save";
            // 
            // MenuItemSaveAs
            // 
            this.MenuItemSaveAs.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemSaveAs.Name = "MenuItemSaveAs";
            this.MenuItemSaveAs.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.MenuItemSaveAs.Size = new System.Drawing.Size(199, 22);
            this.MenuItemSaveAs.Text = "Save As";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(196, 6);
            // 
            // MenuItemClose
            // 
            this.MenuItemClose.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemClose.Name = "MenuItemClose";
            this.MenuItemClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.W)));
            this.MenuItemClose.Size = new System.Drawing.Size(199, 22);
            this.MenuItemClose.Text = "Close";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(196, 6);
            // 
            // MenuItemExit
            // 
            this.MenuItemExit.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.MenuItemExit.Name = "MenuItemExit";
            this.MenuItemExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.MenuItemExit.Size = new System.Drawing.Size(199, 22);
            this.MenuItemExit.Text = "Exit";
            this.MenuItemExit.Click += new System.EventHandler(this.MenuItemExit_Click);
            // 
            // MenuItemEdit
            // 
            this.MenuItemEdit.Name = "MenuItemEdit";
            this.MenuItemEdit.Size = new System.Drawing.Size(42, 21);
            this.MenuItemEdit.Text = "Edit";
            // 
            // MenuItemView
            // 
            this.MenuItemView.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemNestleUp,
            this.MenuItemNestleDown});
            this.MenuItemView.Name = "MenuItemView";
            this.MenuItemView.Size = new System.Drawing.Size(47, 21);
            this.MenuItemView.Text = "View";
            // 
            // MenuItemNestleUp
            // 
            this.MenuItemNestleUp.Name = "MenuItemNestleUp";
            this.MenuItemNestleUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.MenuItemNestleUp.Size = new System.Drawing.Size(220, 22);
            this.MenuItemNestleUp.Text = "Nestle Up";
            this.MenuItemNestleUp.Click += new System.EventHandler(this.MenuItemNestle_Click);
            // 
            // MenuItemNestleDown
            // 
            this.MenuItemNestleDown.Name = "MenuItemNestleDown";
            this.MenuItemNestleDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.MenuItemNestleDown.Size = new System.Drawing.Size(220, 22);
            this.MenuItemNestleDown.Text = "Nestle Down";
            this.MenuItemNestleDown.Click += new System.EventHandler(this.MenuItemNestleDown_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBoxLabel);
            this.panel1.Location = new System.Drawing.Point(3, 28);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1018, 86);
            this.panel1.TabIndex = 4;
            // 
            // pictureBoxLabel
            // 
            this.pictureBoxLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBoxLabel.Location = new System.Drawing.Point(0, 0);
            this.pictureBoxLabel.Name = "pictureBoxLabel";
            this.pictureBoxLabel.Size = new System.Drawing.Size(1018, 86);
            this.pictureBoxLabel.TabIndex = 0;
            this.pictureBoxLabel.TabStop = false;
            this.pictureBoxLabel.Paint += new System.Windows.Forms.PaintEventHandler(this.pictureBoxLabel_Paint);
            this.pictureBoxLabel.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLabel_MouseDown);
            this.pictureBoxLabel.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLabel_MouseMove);
            this.pictureBoxLabel.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pictureBoxLabel_MouseUp);
            // 
            // openFileDlg
            // 
            this.openFileDlg.FileName = "openFileDialog1";
            this.openFileDlg.Filter = "音乐文件|*.rtm;*.mp3";
            // 
            // timerChangeCursorPosition
            // 
            this.timerChangeCursorPosition.Interval = 1;
            this.timerChangeCursorPosition.Tick += new System.EventHandler(this.timerChangeCursorPosition_Tick);
            // 
            // timerTime
            // 
            this.timerTime.Interval = 1;
            this.timerTime.Tick += new System.EventHandler(this.timerTime_Tick);
            // 
            // timerChangeCursorPositionByDragCursor
            // 
            this.timerChangeCursorPositionByDragCursor.Interval = 1;
            this.timerChangeCursorPositionByDragCursor.Tick += new System.EventHandler(this.timerChangeCursorPositionByDragCursor_Tick);
            // 
            // timerChangeStartCursor
            // 
            this.timerChangeStartCursor.Interval = 1;
            this.timerChangeStartCursor.Tick += new System.EventHandler(this.timerChangeSelectCursor_Tick);
            // 
            // timerChangeEndCursor
            // 
            this.timerChangeEndCursor.Interval = 1;
            this.timerChangeEndCursor.Tick += new System.EventHandler(this.timerChangeEndCursor_Tick);
            // 
            // rightButtonMenu
            // 
            this.rightButtonMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuItemDeleteNote,
            this.MenuItemClearSelect});
            this.rightButtonMenu.Name = "rightButtonMenu";
            this.rightButtonMenu.Size = new System.Drawing.Size(125, 48);
            // 
            // MenuItemDeleteNote
            // 
            this.MenuItemDeleteNote.Name = "MenuItemDeleteNote";
            this.MenuItemDeleteNote.Size = new System.Drawing.Size(124, 22);
            this.MenuItemDeleteNote.Text = "删除";
            // 
            // MenuItemClearSelect
            // 
            this.MenuItemClearSelect.Name = "MenuItemClearSelect";
            this.MenuItemClearSelect.Size = new System.Drawing.Size(124, 22);
            this.MenuItemClearSelect.Text = "清除选区";
            this.MenuItemClearSelect.Click += new System.EventHandler(this.MenuItemClearSelect_Click);
            // 
            // timerMoveNote
            // 
            this.timerMoveNote.Interval = 1;
            this.timerMoveNote.Tick += new System.EventHandler(this.timerMoveNote_Tick);
            // 
            // timerGetDuration
            // 
            this.timerGetDuration.Interval = 1;
            this.timerGetDuration.Tick += new System.EventHandler(this.timerGetDuration_Tick);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1024, 768);
            this.Controls.Add(this.panelMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "RhythmEditor Ver2.0";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.Form1_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxWork)).EndInit();
            this.panelMain.ResumeLayout(false);
            this.panelMain.PerformLayout();
            this.panelTimeLineSpace.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTimeLine)).EndInit();
            this.panelTime.ResumeLayout(false);
            this.panelTime.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxTime)).EndInit();
            this.panelWork.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.musicPlayer)).EndInit();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBoxLabel)).EndInit();
            this.rightButtonMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBoxWork;
        private System.Windows.Forms.Panel panelTime;
        private System.Windows.Forms.PictureBox pictureBoxTime;
        private System.Windows.Forms.Panel panelWork;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemFile;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNew;
        private System.Windows.Forms.ToolStripMenuItem MenuItemOpne;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSave;
        private System.Windows.Forms.ToolStripMenuItem MenuItemSaveAs;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClose;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem MenuItemExit;
        private System.Windows.Forms.ToolStripMenuItem MenuItemEdit;
        private System.Windows.Forms.Panel panelMain;
        private AxWMPLib.AxWindowsMediaPlayer musicPlayer;
        private System.Windows.Forms.ToolStripMenuItem MenuItemView;
        private System.Windows.Forms.TextBox textBoxTimeInput;
        private System.Windows.Forms.OpenFileDialog openFileDlg;
        private System.Windows.Forms.Timer timerChangeCursorPosition;
        private System.Windows.Forms.Panel panelTimeLineSpace;
        private System.Windows.Forms.PictureBox pictureBoxTimeLine;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBoxLabel;
        private System.Windows.Forms.Timer timerTime;
        private System.Windows.Forms.Timer timerChangeCursorPositionByDragCursor;
        private System.Windows.Forms.Timer timerChangeStartCursor;
        private System.Windows.Forms.Timer timerChangeEndCursor;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNestleUp;
        private System.Windows.Forms.ToolStripMenuItem MenuItemNestleDown;
        private System.Windows.Forms.ContextMenuStrip rightButtonMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuItemDeleteNote;
        private System.Windows.Forms.Timer timerMoveNote;
        private System.Windows.Forms.ToolStripMenuItem MenuItemClearSelect;
        private System.Windows.Forms.Timer timerGetDuration;
    }
}

