using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
//Moudle 1
namespace Rhythm_Ocrosoft
{
    public partial class FormMain : Form
    {
        Game myGame = new Game();
        bool openEnabled = true;//游戏中不能进行打开操作
        bool pauseEnabled = true;//等待时间内不能进行暂停操作
        int _mouseX;//鼠标的位置
        int _mouseY;
        //int speedCustom = 3;//自定义速度,暂时不用
        int keySpeed = 10;//按键下落速度，针对不同配置的电脑
        public static double ZoomRate;

        #region Bitmap
        Bitmap MusicIcon_Open = new Bitmap("Resources/MusicIcon_Open.png");
        Bitmap Pause_Setting = new Bitmap("Resources/Pause_Setting.png");
        Bitmap Rescue = new Bitmap("Resources/Rescue.png");//继续
        Bitmap Restart = new Bitmap("Resources/Restart.png");//重新开始
        Bitmap CloseForm = new Bitmap("Resources/Close.png");//停止及关闭
        #endregion

        //显示错误
        private void showError(string error)
        {
            try
            {
                MessageBox.Show(null, error + ",请联系Ocrosoft@Ocrosoft.com", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch
            {
                MessageBox.Show(null, "错误显示失败,请联系Ocrosoft@Ocrosoft.com", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        //构造
        public FormMain()
        {
            InitializeComponent();
        }
        //pictureBoxGame绘图事件0X1002
        private void pictureBoxGame_Paint(object sender, PaintEventArgs e)
        {
            try
            {
                myGame.Draw(e.Graphics);
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1002，ErrorInfo：" + exc);
            }
        }
        //启动0X1003
        private void FormMain_Load(object sender, EventArgs e)
        {
            try
            {
                Opacity = 0;
                Enabled = false;
                //_timerOpacityUP= new System.Threading.Timer(new System.Threading.TimerCallback(timerOpacityTick), null, 0, 1);
                timerOpacityUP.Enabled = true;

                #region 窗口的大小设置
                int width = Screen.PrimaryScreen.Bounds.Width;
                int height = Screen.PrimaryScreen.Bounds.Height;

                Height = height;
                Width = width;
                Location = new Point(0, 0);

                if (width * 1.0 / 1024 > height * 1.0 / 768)
                {
                    pictureBoxGame.Height = height;
                    pictureBoxGame.Width = (int)(1024 * height * 1.0 / 768);
                    myGame.ZoomRate = height * 1.0 / 768;

                    pictureBoxGame.Location = new Point((int)((width - pictureBoxGame.Width) / 2.0), 0);
                }
                else
                {
                    pictureBoxGame.Width = width;
                    pictureBoxGame.Height = (int)(768 * width * 1.0 / 1024);
                    myGame.ZoomRate = width * 1.0 / 1024;

                    pictureBoxGame.Location = new Point(0, (int)((height - pictureBoxGame.Height) / 2.0));
                }
                ZoomRate = myGame.ZoomRate;
                #endregion
                //打开、暂停的位置和大小
                myGame.MusicIconPoint = new Point((int)(18 * myGame.ZoomRate), (int)(25 * myGame.ZoomRate));
                myGame.MusicIconSize = new Size((int)(50 * myGame.ZoomRate), (int)(50 * myGame.ZoomRate));
                myGame.PauseSetting = new Point((int)(970 * myGame.ZoomRate), (int)(75 * myGame.ZoomRate));
                myGame.PauseSettingSize = new Size((int)(45 * myGame.ZoomRate), (int)(45 * myGame.ZoomRate));
                //设置三个设置项的位置
                myGame.RescueB = new Point((int)(972 * myGame.ZoomRate), (int)(78 * myGame.ZoomRate));
                myGame.RestartB = new Point((int)(972 * myGame.ZoomRate), (int)(78 * myGame.ZoomRate));
                myGame.HomeB = new Point((int)(972 * myGame.ZoomRate), (int)(78 * myGame.ZoomRate));
                //初始化
                myGame.Initialize();
                musicPlayer.settings.volume = 0;
                //绘制
                pictureBoxGame.Invalidate();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1003，ErrorInfo：" + exc);
            }
        }
        //打开0X1004
        private void Open_Click()
        {
            try
            {
                myGame.Initialize();
                DialogResult dlgre = openFile.ShowDialog();
                //收回设置选项
                if (myGame.Setting)
                {
                    Pause_Click();
                }
                if (dlgre == DialogResult.OK) { openEnabled = false; }
                else return;
                myGame.Open(openFile.FileName);
                myGame.StartGame(musicPlayerTrue);
                myGame.Readi = 0;
                pauseEnabled = false;//开始。不能进行暂停操作
                timerReady.Enabled = true;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1004，ErrorInfo：" + exc);
            }
        }
        //显示Ready0X1005
        private void timerReady_Tick(object sender, EventArgs e)
        {
            if (myGame.Readi == 0)
            {
                myGame.ReadyPoint = new Point((int)(410 * myGame.ZoomRate), (int)(280 * myGame.ZoomRate));
            }
            try
            {
                if (myGame.Readi != 40)
                {
                    if (myGame.Readi <= 8)
                        myGame.ReadyPoint = new Point(myGame.ReadyPoint.X, myGame.ReadyPoint.Y + 3);
                    myGame.Readi++;
                }
                else
                {
                    myGame.Readi = 0;
                    if (myGame.RescurFlag == 2)
                    {
                        musicPlayer.Ctlcontrols.play();
                        //musicPlayerTrue.Ctlcontrols.play();
                        myGame.RescurFlag = 0;
                    }
                    else
                    {
                        //开始下落
                        musicPlayer.URL = Path.GetTempPath() + "ROTMP\\" + "song.mp3";
                        musicPlayer.settings.volume = 100;
                        pauseEnabled = true;
                        //timerWait.Enabled = true;
                    }
                    myGame.GameState = GameState.Play;
                    timerGameCtl.Enabled = true;
                    timerReady.Enabled = false;
                }
                pictureBoxGame.Invalidate();
            }
            catch (Exception exc)
            {
                timerReady.Enabled = false;
                showError("出现错误，ErrorCode：0X1005，ErrorInfo：" + exc);
            }
        }
        //暂停0X1006
        private void Pause_Click()
        {
            try
            {
                if (myGame.GameState != GameState.Stop)
                    myGame.GameState = GameState.Pause;//设置状态为暂停
                timerGameCtl.Enabled = false;//游戏控制timer
                musicPlayer.Ctlcontrols.pause();//两个播放器
                //musicPlayerTrue.Ctlcontrols.pause();
                myGame.Moving = true;
                timerSetting.Enabled = true;
                pictureBoxGame.Invalidate();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1006，ErrorInfo：" + exc);
            }
        }
        //展开、收回设置0X1007
        private void timerSetting_Tick(object sender, EventArgs e)
        {
            try
            {
                if (myGame.Settingi != 7)//标况下，移动7次10/7单位
                {
                    if (myGame.Setting == false)//展开
                    {
                        myGame.RescueB = new Point(myGame.RescueB.X - (int)(10 * myGame.ZoomRate), myGame.RescueB.Y);
                        myGame.RestartB = new Point(myGame.RestartB.X - (int)(7 * myGame.ZoomRate), myGame.RestartB.Y + (int)(7 * myGame.ZoomRate));
                        myGame.HomeB = new Point(myGame.HomeB.X, myGame.HomeB.Y + (int)(10 * myGame.ZoomRate));
                    }
                    else//收回
                    {
                        myGame.RescueB = new Point(myGame.RescueB.X + (int)(10 * myGame.ZoomRate), myGame.RescueB.Y);
                        myGame.RestartB = new Point(myGame.RestartB.X + (int)(7 * myGame.ZoomRate), myGame.RestartB.Y - (int)(7 * myGame.ZoomRate));
                        myGame.HomeB = new Point(myGame.HomeB.X, myGame.HomeB.Y - (int)(10 * myGame.ZoomRate));
                    }
                    myGame.Settingi++;
                }
                else
                {
                    if (myGame.RescurFlag == 1)//如果是Rescue
                    {
                        myGame.RescurFlag = 2;
                        myGame.Readi = 0;
                        timerReady.Enabled = true;
                        pauseEnabled = true;
                    }
                    myGame.Settingi = 0;
                    if (myGame.Setting)
                        myGame.Moving = false;
                    myGame.Setting = !myGame.Setting;
                    timerSetting.Enabled = false;
                }
                pictureBoxGame.Invalidate();
            }
            catch (Exception exc)
            {
                timerSetting.Enabled = false;
                showError("出现错误，ErrorCode：0X1007，ErrorInfo：" + exc);
            }

        }
        //继续按钮0X1008
        private void Rescue_Click()
        {
            if (myGame.GameState == GameState.Pause)
            {
                try
                {
                    musicPlayerTrue.URL = "Resources/Ready.mp3";
                    myGame.RescurFlag = 1;
                    pauseEnabled = false;
                    timerSetting.Enabled = true;
                }
                catch (Exception exc)
                {
                    showError("出现错误，ErrorCode：0X1008，ErrorInfo：" + exc);
                }
            }
        }
        //判断一个点是否在圆内0X1009
        private bool judgeMouseInCircle(int mouseX, int MouseY, Point circle, double r)
        {
            try
            {
                circle.X += (int)r;
                circle.Y += (int)r;
                if ((MouseY - circle.Y) * (MouseY - circle.Y) + (mouseX - circle.X) * (mouseX - circle.X) <= r * r)
                {
                    return true;
                }
                return false;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1009，ErrorInfo：" + exc);
                return false;
            }
        }
        //鼠标移动事件，修改鼠标样式0X1010
        private void pictureBoxGame_MouseMove(object sender, MouseEventArgs e)
        {
            try
            {
                _mouseX = e.X;
                _mouseY = e.Y;
                if (judgeMouseInCircle(e.X, e.Y, myGame.MusicIconPoint, myGame.MusicIconSize.Height / 2))
                {
                    if (openEnabled == true)
                        pictureBoxGame.Cursor = Cursors.Hand;
                }
                else if (judgeMouseInCircle(e.X, e.Y, myGame.PauseSetting, myGame.PauseSettingSize.Height / 2))
                {
                    if (pauseEnabled == true)
                        pictureBoxGame.Cursor = Cursors.Hand;
                }
                else if (judgeMouseInCircle(e.X, e.Y, myGame.RescueB, (int)(40 * myGame.ZoomRate / 2)))
                {
                    pictureBoxGame.Cursor = Cursors.Hand;
                }
                else if (judgeMouseInCircle(e.X, e.Y, myGame.RestartB, (int)(40 * myGame.ZoomRate / 2)))
                {
                    pictureBoxGame.Cursor = Cursors.Hand;
                }
                else if (judgeMouseInCircle(e.X, e.Y, myGame.HomeB, (int)(40 * myGame.ZoomRate / 2)))
                {
                    pictureBoxGame.Cursor = Cursors.Hand;
                }
                else
                {
                    pictureBoxGame.Cursor = Cursors.Default;
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1010，ErrorInfo：" + exc);
            }
        }
        //点击事件0X1011
        private void pictureBoxGame_Click(object sender, EventArgs e)
        {
            try
            {
                if (judgeMouseInCircle(_mouseX, _mouseY, myGame.MusicIconPoint, myGame.MusicIconSize.Height / 2))
                {
                    if (openEnabled == true)
                        Open_Click();
                }
                else if (judgeMouseInCircle(_mouseX, _mouseY, myGame.PauseSetting, myGame.PauseSettingSize.Height / 2))
                {
                    if (pauseEnabled == true)
                        Pause_Click();
                }
                else if (judgeMouseInCircle(_mouseX, _mouseY, myGame.RescueB, (int)(40 * myGame.ZoomRate / 2)))
                {
                    Rescue_Click();
                }
                else if (judgeMouseInCircle(_mouseX, _mouseY, myGame.RestartB, (int)(40 * myGame.ZoomRate / 2)))
                {
                    advancedSetting adv = new advancedSetting();
                    adv.Speed = keySpeed;
                    if (adv.ShowDialog() == DialogResult.OK)
                    {
                        keySpeed = adv.Speed;
                        //MessageBox.Show(keySpeed.ToString());
                        myGame.KeySpeed = keySpeed;
                        adv.Dispose();
                        timerSetting.Enabled = true;
                    }
                }
                else if (judgeMouseInCircle(_mouseX, _mouseY, myGame.HomeB, (int)(40 * myGame.ZoomRate / 2)))
                {
                    close_Click();
                }
                else
                {
                    Focus();
                    if (myGame.Setting)
                    {
                        Pause_Click();
                    }
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1011，ErrorInfo：" + exc);
            }
        }
        //等待，用来调整延迟0X1012
        private void timerWait_Tick(object sender, EventArgs e)
        {
            try
            {
                //真正开始播放
                musicPlayerTrue.URL = Path.GetTempPath() + "ROTMP\\" + "song.mp3";
                //等待结束，可以进行暂停操作
                pauseEnabled = true;
                timerWait.Enabled = false;
            }
            catch (Exception exc)
            {
                timerWait.Enabled = false;
                showError("出现错误，ErrorCode：0X1012，ErrorInfo：" + exc);
            }
        }
        //关闭代码0X1013
        private void close_Click()
        {
            if (myGame.GameState != GameState.Stop)
            {
                try
                {
                    myGame.Initialize();
                    openEnabled = true;
                    musicPlayer.URL = "";
                    musicPlayerTrue.URL = "";
                    pictureBoxGame.Invalidate();
                }
                catch (Exception exc)
                {
                    showError("出现错误，ErrorCode：0X1013，ErrorInfo：" + exc);
                }

            }
            else
            {
                try
                {
                    timerOpacitiDOWN.Enabled = true;
                }
                catch (Exception exc)
                {
                    showError("出现错误，ErrorCode：0X1013，ErrorInfo：" + exc);
                }
            }
            timerSetting.Enabled = true;
        }
        //淡入0X1014
        private void timerOpacityUP_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Opacity != 1)
                {
                    Opacity += 0.02;
                }
                else
                {
                    Enabled = true;
                    timerOpacityUP.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                timerOpacityUP.Enabled = false;
                showError("出现错误，ErrorCode：0X1014，ErrorInfo：" + exc);
            }
        }
        //淡出0X1015
        private void timerOpacitiDOWN_Tick(object sender, EventArgs e)
        {
            try
            {
                if (Opacity != 0)
                {
                    Opacity -= 0.02;
                }
                else
                {
                    MusicIcon_Open.Dispose();
                    Pause_Setting.Dispose();
                    Rescue.Dispose();
                    Restart.Dispose();
                    CloseForm.Dispose();
                    myGame.Dispose();
                    Close();
                    timerOpacitiDOWN.Enabled = false;
                }
            }
            catch (Exception exc)
            {
                timerOpacitiDOWN.Enabled = false;
                showError("出现错误，ErrorCode：0X1015，ErrorInfo：" + exc);
            }
        }
        //游戏控制0X1016
        private void timerGameCtl_Tick(object sender, EventArgs e)
        {
            try
            {
                if ((int)musicPlayerTrue.playState == 1 && (int)musicPlayer.playState == 1)
                {
                    //游戏结束
                    myGame.GameFinished = true;
                    timerGameCtl.Enabled = false;
                    ShowResult();
                    return;
                }
                myGame.Percent = musicPlayer.Ctlcontrols.currentPosition / musicPlayer.currentMedia.duration;
                myGame.CurrentTime = musicPlayer.Ctlcontrols.currentPosition;
                myGame.keyMove();
                pictureBoxGame.Invalidate();
            }
            catch (Exception exc)
            {
                timerGameCtl.Enabled = false;
                showError("出现错误，ErrorCode：0X1016，ErrorInfo：" + exc);
            }
        }
        //空白窗体点击事件,0X1017
        private void FormMain_MouseClick(object sender, MouseEventArgs e)
        {
            try
            {
                if (myGame.Setting)
                {
                    Pause_Click();
                }
                Focus();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1017，ErrorInfo：" + exc);
            }
        }
        //按键事件,0X1018
        private void FormMain_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                int keyCode = (int)e.KeyCode;
                if (keyCode == (int)Keys.G || keyCode == (int)Keys.H)
                    keyCode = (int)Keys.Space;
                myGame.JudgeKeyPress(Convert.ToChar(keyCode), pictureBoxGame.CreateGraphics());//判断是否正确并进行移除操作
            }
            catch (Exception exc)
            {
                timerGameCtl.Enabled = false;
                showError("出现错误，ErrorCode：0X1018，ErrorInfo：" + exc);
            }
        }
        //显示结果,0X1019
        private void ShowResult()
        {
            try
            {
                FormResult myresult = new FormResult();
                myresult.Rate = ((myGame.Accept - myGame.Perfect) * 0.8 + myGame.Perfect * 1.0) / myGame.KeyCount * 100;
                myresult.Accept = -myGame.Accept;
                myresult.Combo = -myGame.MaxCombo;
                myresult.KeyCount = myGame.KeyCount;
                myresult.SongName = myGame.OpenSong;
                myresult.ShowDialog();
                myGame.Initialize();
                musicPlayer.URL = "";
                musicPlayerTrue.URL = "";
                myGame.Percent = 0;
                openEnabled = true;
                pictureBoxGame.Invalidate();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X1019，ErrorInfo：" + exc);
            }
        }
    }
}
