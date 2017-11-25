using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Windows.Forms;
using ICSharpCode.SharpZipLib.Zip;
//Moudle 2
namespace Rhythm_Ocrosoft
{
    enum GameState
    {
        Stop, Play, Pause, Ready
    }
    class Game
    {
        #region 音乐文件相关
        private string _openSong;//保存歌曲的名称
        private string _maker;//制作者
        private int _difficulty = 1;//难度
        private int _cursor = 0;//_listKey的index
        private double _currentTime;//当前的时间，从musicPlayer获取
        private double _percent;//百分比，进度条使用
        private double _keyTime;//下一个按键对应的时间
        private bool[] temp = new bool[9];//保存每个时间有哪些按键
        private int rescueFlag = 0;//0表示不是继续事件，1表示是按下继续到收回菜单的状态，2表示显示ready的状态
        private int _speedCustom = 3;//速度自定义
        private int _keySpeed = 10;//下落速度
        //按键的时候与name属性校对，等于-1说明不是这个键
        private char[] keyChar = { 'A', 'S', 'D', 'F', Convert.ToChar(Keys.Space), 'J', 'K', 'L', Convert.ToChar(Keys.Oem1) };
        List<MyKey> _listKey = new List<MyKey>();
        List<DrKey> _listDrKey = new List<DrKey>();
        MyKey myKey = new MyKey();
        DrKey drKey = new DrKey();
        GameState _gameState;//游戏状态
        #endregion

        #region 公有字段

        public string OpenSong
        {
            get
            {
                return _openSong;
            }

            set
            {
                _openSong = value;
            }
        }

        public string Maker
        {
            get
            {
                return _maker;
            }

            set
            {
                _maker = value;
            }
        }

        internal GameState GameState
        {
            get
            {
                return _gameState;
            }

            set
            {
                _gameState = value;
            }
        }

        public int Difficulty
        {
            get
            {
                return _difficulty;
            }

            set
            {
                _difficulty = value;
            }
        }

        public double ZoomRate
        {
            get
            {
                return _zoomRate;
            }

            set
            {
                _zoomRate = value;
            }
        }

        public int KeyCount
        {
            get
            {
                return keyCount;
            }

            set
            {
                keyCount = value;
            }
        }

        public Point RescueB
        {
            get
            {
                return rescueB;
            }

            set
            {
                rescueB = value;
            }
        }

        public Point RestartB
        {
            get
            {
                return restartB;
            }

            set
            {
                restartB = value;
            }
        }

        public Point HomeB
        {
            get
            {
                return homeB;
            }

            set
            {
                homeB = value;
            }
        }

        public bool Setting
        {
            get
            {
                return setting;
            }

            set
            {
                setting = value;
            }
        }

        public int Settingi
        {
            get
            {
                return settingi;
            }

            set
            {
                settingi = value;
            }
        }

        public bool Moving
        {
            get
            {
                return moving;
            }

            set
            {
                moving = value;
            }
        }

        public Point MusicIconPoint
        {
            get
            {
                return _musicIconPoint;
            }

            set
            {
                _musicIconPoint = value;
            }
        }

        public Point PauseSetting
        {
            get
            {
                return _pauseSetting;
            }

            set
            {
                _pauseSetting = value;
            }
        }

        public Size MusicIconSize
        {
            get
            {
                return _musicIconSize;
            }

            set
            {
                _musicIconSize = value;
            }
        }

        public Size PauseSettingSize
        {
            get
            {
                return _pauseSettingSize;
            }

            set
            {
                _pauseSettingSize = value;
            }
        }

        public Point ReadyPoint
        {
            get
            {
                return readyPoint;
            }

            set
            {
                readyPoint = value;
            }
        }

        public int Readi
        {
            get
            {
                return readi;
            }

            set
            {
                readi = value;
            }
        }

        public double Percent
        {
            get
            {
                return _percent;
            }

            set
            {
                _percent = value;
            }
        }

        public double CurrentTime
        {
            get
            {
                return _currentTime;
            }

            set
            {
                _currentTime = value;
            }
        }

        public double KeyTime
        {
            get
            {
                return _keyTime;
            }

            set
            {
                _keyTime = value;
            }
        }

        internal List<MyKey> ListKey
        {
            get
            {
                return _listKey;
            }

            set
            {
                _listKey = value;
            }
        }

        public int RescurFlag
        {
            get
            {
                return rescueFlag;
            }

            set
            {
                rescueFlag = value;
            }
        }

        public int SpeedCustom
        {
            get
            {
                return _speedCustom;
            }

            set
            {
                _speedCustom = value;
            }
        }

        public int KeySpeed
        {
            get
            {
                return _keySpeed;
            }

            set
            {
                _keySpeed = value;
            }
        }
        #endregion

        #region 游戏计分相关
        private int keyCount;//计数
        private int _accept;//正确
        private int _perfect;//完美
        private int _combo;//连击
        private int _maxCombo;//最大连击
        #endregion

        #region 游戏显示相关
        Font myFont;//ready的字体
        Font fontSong;
        SolidBrush fontBrush = new SolidBrush(Color.White);//ready的字体刷
        private Point readyPoint;//ready的位置
        private int readi;//ready的计数器
        private Point _pointPosition;//进度条的位置
        private double _zoomRate;//缩放比例
        private Point rescueB;//继续按钮的位置
        private Point restartB;//重新开始按钮的位置
        private Point homeB;//结束按钮的位置
        private bool setting = false;//设置是否打开
        private int settingi = 0;//控制设置按钮的展开次数
        private bool moving = false;//使得收回设置后能够隐藏
        Bitmap backgroundImg = new Bitmap("Resources/BackGround-Blue.png");//背景
        Bitmap EndLine = new Bitmap("Resources/EndLine.png");//结束线
        Bitmap PointEasy = new Bitmap("Resources/PointEasy.png");//简单进度条
        Bitmap PointNormal = new Bitmap("Resources/PointNormal.png");//普通进度条
        Bitmap PointHard = new Bitmap("Resources/PointHard.png");//困难进度条
        Bitmap MusicIcon_Open = new Bitmap("Resources/MusicIcon_Open.png");//打开按钮
        Bitmap Pause_Setting = new Bitmap("Resources/Pause_Setting.png");//设置按钮
        Bitmap TimeLine = new Bitmap("Resources/TimeLine.png");//进度条
        Bitmap nameLine = new Bitmap("Resources/NameLine.png");//五线谱
        Bitmap Rescue = new Bitmap("Resources/Rescue.png");//继续
        Bitmap Restart = new Bitmap("Resources/Restart.png");//重新开始
        Bitmap CloseForm = new Bitmap("Resources/Close.png");//停止及关闭
        Bitmap Key2 = new Bitmap("Resources/Key2.png");//按键有效
        #endregion

        #region 鼠标移动
        private Point _musicIconPoint;//打开图标的位置
        private Size _musicIconSize;//大小
        private Point _pauseSetting;//暂停按钮的位置
        private Size _pauseSettingSize;//暂停按钮的大小
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
        //初始化0X2001
        public void Initialize()
        {
            try
            {
                _pointPosition = new Point((int)(-15 * _zoomRate), (int)(43 * _zoomRate));
                _percent = 0;
                CurrentTime = 0;
                _cursor = 0; ;
                _accept = 0;
                _perfect = 0;
                _combo = 0;
                _maxCombo = 0;
                _gameState = GameState.Stop;
                _listDrKey.Clear();
                myFont = new Font("Comic Sans MS", (float)(30 * _zoomRate), FontStyle.Bold);
                fontSong = new Font("Comic Sans MS", (float)(15 * _zoomRate), FontStyle.Bold);
                readyPoint = new Point((int)(410 * _zoomRate), (int)(280 * _zoomRate));
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2001，ErrorInfo：" + exc);
            }
        }
        //解压文件0X2002
        public void UnZipFile(string zipFilePath)
        {
            try
            {
                string openPath = Path.GetTempPath() + "ROTMP\\";
                if (Directory.Exists(Path.GetTempPath() + "ROTMP"))
                    Directory.Delete(Path.GetTempPath() + "ROTMP", true);
                Directory.CreateDirectory(openPath);
                if (!File.Exists(zipFilePath))
                {
                    Console.WriteLine("Cannot find file '{0}'", zipFilePath);
                    return;
                }
                using (ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath)))
                {
                    s.Password = "Rhythm!";
                    ZipEntry theEntry;
                    while ((theEntry = s.GetNextEntry()) != null)
                    {
                        string directoryName = openPath;
                        string fileName = Path.GetFileName(theEntry.Name);
                        if (directoryName.Length > 0)
                        {
                            Directory.CreateDirectory(directoryName);
                        }
                        if (fileName != String.Empty)
                        {
                            using (FileStream streamWriter = File.Create(directoryName + theEntry.Name))
                            {

                                int size = 2048;
                                byte[] data = new byte[2048];
                                while (true)
                                {
                                    size = s.Read(data, 0, data.Length);
                                    if (size > 0)
                                    {
                                        streamWriter.Write(data, 0, size);
                                    }
                                    else
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2002，ErrorInfo：" + exc);
            }
        }
        //开始游戏0X2003
        public void StartGame(AxWMPLib.AxWindowsMediaPlayer musicPlayerTrue)
        {
            try
            {
                musicPlayerTrue.URL = "Resources/Ready.mp3";
                Initialize();
                _gameState = GameState.Ready;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2003，ErrorInfo：" + exc);
            }
        }
        //比较函数0X2004
        private static int SortCompare(MyKey p1, MyKey p2)
        {
            try
            {
                if (p1.Min == p2.Min)
                {
                    if (p1.Sec == p2.Sec)
                    {
                        if (p1.Ms < p2.Ms)
                            return -1;
                        else
                            return 1;
                    }
                    else
                    {
                        if (p1.Sec < p2.Sec)
                            return -1;
                        else
                            return 1;
                    }
                }
                else
                {
                    if (p1.Min < p2.Min)
                        return -1;
                    else
                        return 1;
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("出现错误，ErrorCode：0X2004，ErrorInfo：" + exc);
                return 0;
            }
        }
        //绘图0X2005
        public void Draw(Graphics g)
        {
            try
            {
                //背景，标准线，进度条，进度光标，打开按钮
                g.DrawImage(backgroundImg, 0, 0, (int)(1024 * _zoomRate), (int)(768 * _zoomRate));
                g.DrawImage(EndLine, 0, (int)(690 * _zoomRate), (int)(1024 * _zoomRate), (int)(20 * _zoomRate));
                g.DrawImage(TimeLine, 0, (int)(50 * _zoomRate), (int)(1024 * _zoomRate), (int)(2 * _zoomRate));
                if (_percent >= 0 && _percent <= 1)//防错
                    _pointPosition = new Point((int)((_percent * 1024 - 15) * _zoomRate), _pointPosition.Y);
                g.DrawImage(PointDiff(_difficulty), _pointPosition.X, _pointPosition.Y, (int)(20 * _zoomRate), (int)(16 * _zoomRate));
                g.DrawImage(MusicIcon_Open, (int)(18 * _zoomRate), (int)(25 * _zoomRate), (int)(50 * _zoomRate), (int)(50 * _zoomRate));
                //三个设置
                if (moving)
                {
                    g.DrawImage(Rescue, rescueB.X, rescueB.Y, (int)(40 * _zoomRate), (int)(40 * _zoomRate));
                    g.DrawImage(Restart, RestartB.X, RestartB.Y, (int)(40 * _zoomRate), (int)(40 * _zoomRate));
                    g.DrawImage(CloseForm, HomeB.X, HomeB.Y, (int)(40 * _zoomRate), (int)(40 * _zoomRate));
                }
                //设置按钮，五线谱
                g.DrawImage(Pause_Setting, (int)(970 * _zoomRate), (int)(75 * _zoomRate), (int)(45 * _zoomRate), (int)(45 * _zoomRate));
                g.DrawImage(nameLine, 0, (int)(290 * _zoomRate), (int)(1024 * _zoomRate), (int)(75 * _zoomRate));
                //曲名，制作者
                g.DrawString(_openSong, fontSong, fontBrush, new Point((int)(200 * _zoomRate), (int)(290 * _zoomRate)));
                g.DrawString(_maker, fontSong, fontBrush, new Point((int)(200 * _zoomRate), (int)(335 * _zoomRate)));
                //绘制按键
                foreach (DrKey myDrKey in _listDrKey)
                {
                    Size size = new Size((int)(70 * _zoomRate), (int)(15 * _zoomRate));
                    myDrKey.Draw(g, size);
                }
                //ready的绘制,放在最后，继续游戏的时候ready可以挡住音符
                if (_gameState == GameState.Ready || RescurFlag == 2)
                {
                    if (readi >= 0)
                        g.DrawString("R", myFont, fontBrush, readyPoint);
                    if (readi >= 2)
                        g.DrawString("e", myFont, fontBrush, new Point(readyPoint.X + 55, readyPoint.Y));
                    if (readi >= 4)
                        g.DrawString("a", myFont, fontBrush, new Point(readyPoint.X + 105, readyPoint.Y));
                    if (readi >= 6)
                        g.DrawString("d", myFont, fontBrush, new Point(readyPoint.X + 150, readyPoint.Y));
                    if (readi >= 8)
                        g.DrawString("y", myFont, fontBrush, new Point(readyPoint.X + 200, readyPoint.Y));
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2005，ErrorInfo：" + exc);
            }
        }
        //转换0X2006
        Bitmap PointDiff(int diff)
        {
            try
            {
                if (diff == 0)
                    return PointEasy;
                else if (diff == 1)
                    return PointNormal;
                else if (diff == 2)
                    return PointHard;
                else
                    return PointEasy;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2006，ErrorInfo：" + exc);
                return PointEasy;
            }
        }
        //打开0X2007
        public void Open(string fileName)
        {
            try
            {
                _listKey.Clear();
                UnZipFile(fileName);
                FileStream fileStream = new FileStream(Path.GetTempPath() + "ROTMP\\" + "song.map", FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(fileStream);

                _openSong = binaryReader.ReadString();
                _maker = binaryReader.ReadString();
                int pattern = binaryReader.ReadInt32();//已删除
                _difficulty = binaryReader.ReadInt32();
                keyCount = binaryReader.ReadInt32();
                for (int i = 0; i < keyCount; i++)
                {
                    MyKey keys = new MyKey();
                    string show = binaryReader.ReadString();
                    keys.Min = binaryReader.ReadInt32();
                    keys.Sec = binaryReader.ReadInt32();
                    keys.Ms = binaryReader.ReadInt32();
                    keys.Count = binaryReader.ReadInt32();
                    for (int j = 0; j < 9; j++)
                    {
                        keys.Key[j] = binaryReader.ReadBoolean();
                    }
                    _listKey.Add(keys);
                }
                _listKey.Sort(SortCompare);
                _cursor = 0;
                myKey = _listKey[0];
                _keyTime = myKey.Min * 60 + myKey.Sec + myKey.Ms / 1000.0;
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2007，ErrorInfo：" + exc);
            }
        }
        //移动按键，并且创建按键0X2008
        public void keyMove()
        {
            try
            {
                //创建key
                while (Math.Abs(_currentTime + _speedCustom - _keyTime) <= 0.05)
                {
                    for (int i = 0; i < 9; i++)
                    {
                        if (myKey.Key[i] == true)
                        {
                            DrKey keys = new DrKey();
                            keys.Position = new Point((int)(155 * _zoomRate) + (int)(i * 80 * _zoomRate), (int)(120 * _zoomRate));
                            keys.KeyName = keyChar[i].ToString();
                            _listDrKey.Add(keys);
                        }
                    }
                    _cursor++;
                    if (_cursor >= keyCount)
                    {
                        _cursor = -1;
                        _keyTime = -1;
                    }
                    if (_cursor != -1)
                    {
                        myKey = _listKey[_cursor];
                        _keyTime = myKey.Min * 60 + myKey.Sec + myKey.Ms / 1000.0;
                    }
                }
                //移动key
                for (int i = 0; i < _listDrKey.Count; i++)
                {
                    drKey = _listDrKey[i];
                    drKey.MoveSpeed = (int)(_keySpeed * _zoomRate);
                    drKey.Move();
                    if (drKey.Position.Y >= 740 * _zoomRate)
                    {
                        _listDrKey.RemoveAt(i);
                        drKey.Dispose();
                        i--;
                    }
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2008，ErrorInfo：" + exc);
            }
        }
        //销毁0X2999
        public void Dispose()
        {
            try
            {
                drKey.Dispose();
                backgroundImg.Dispose();
                EndLine.Dispose();
                PointEasy.Dispose();
                PointNormal.Dispose();
                PointHard.Dispose();
                MusicIcon_Open.Dispose();
                Pause_Setting.Dispose();
                TimeLine.Dispose();
                nameLine.Dispose();
                Rescue.Dispose();
                Restart.Dispose();
                CloseForm.Dispose();
                Key2.Dispose();
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2999，ErrorInfo：" + exc);
            }
        }
        //判断按键是否有效,0X2009
        public void JudgeKeyPress(char keyChar, Graphics g)
        {
            try
            { for (int i = 0; i < _listDrKey.Count; i++)
                {
                    if (i > 27) break;
                    DrKey keys = new DrKey();
                    keys = _listDrKey[i];
                    if (keys.KeyName.IndexOf(keyChar) != -1)
                    {
                        if (keys.Position.Y >= 550 * _zoomRate)//有效
                        {
                            if (Math.Abs(keys.Position.Y - 700 * _zoomRate) <= 60 * _zoomRate)//正确
                            {
                                if (Math.Abs(keys.Position.Y - 700 * _zoomRate) <= 20)//完美
                                {
                                    _perfect++;
                                }
                                _listDrKey.RemoveAt(i);//移除
                                _accept++;//正确++;
                                _combo++;
                                if (_combo > _maxCombo)
                                    _maxCombo = _combo;
                                //显示连击，还没做完
                                // MessageBox.Show(keys.Position.ToString());
                            }
                            else
                            {
                                _listDrKey.RemoveAt(i);
                                _combo = 0;
                            }
                            //418, 193
                            //422, 228
                            g.DrawImage(Key2, keys.Position.X - (int)(2 * _zoomRate), keys.Position.Y - (int)(25 * _zoomRate), (int)(95 * 0.75 * _zoomRate), (int)(90 * 0.75 * _zoomRate));
                            break;
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                showError("出现错误，ErrorCode：0X2009，ErrorInfo：" + exc);
            }
        }
    }
}
