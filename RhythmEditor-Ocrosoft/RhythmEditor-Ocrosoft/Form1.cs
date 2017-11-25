using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
/// means it is [Code].
namespace RhythmEditor_Ocrosoft
{
    public partial class Form1 : Form
    {
        #region Enum
        enum playState { Stopped, Playing, Pausing, RewindingAndPlaying, ForwardingAndPlaying, ForwardingAndPausing, RewindingAndPausing }
        enum operation { Default, Select, Move }//this select is select note.select(line 33) is select an area.
        #endregion

        #region Editor
        private MyKey _myKey;
        private List<MyKey> _listKey = new List<MyKey>();
        private string _openMp3;//opened mp3,not rtm file.
        private string _openRTM;
        private int _page;//each page  shown 30s/page.
        private int _pageView;//the page view now.
        private bool _en1, _en2, _en3;//if Title、Author、Maker is english.
        private string _title;//Title
        private string _author;//Author
        private string _maker;//Maker
        private string _description;//Description
        private double _currentTime;
        private string _tempTime;
        private int _keyCount;//Total keys.
        private double _duration = 10000;//the time of opened song.
        private Point _timeCursorPosition;//TimeCursor's position.
        playState _playState;
        #endregion

        #region WorkSpace
        private operation _operator;
        private bool _select;
        private bool _selecting;
        private Point _selectStart;
        private Point _selectEnd;
        private Point _mousePositionWork;
        struct keyInfo //save note's info before move and delete it.
        {
            public int i;//the index of note in _listKey.
            public int j;//the index of note in Key[].(Check it in MyKey.cs)
        }
        keyInfo _keyInfo = new keyInfo();
        keyInfo _keyInfoTmp = new keyInfo();
        private bool _keyInfoLock;
        #endregion

        #region TimeLineSpace
        private Point _timeLineMousePosition;
        #endregion

        #region TimeSpace
        private Point _timeMousePosition;//save the mouse position if mouse is in time space.
        private bool _isCHangeCursorPosition;//if change time by drag time.
        private Point _mousePositionFormStart;//the start position of mouse.
        private Point _mousePositionForm;//mouse position for form.
        //flags for mouseMove.
        private bool _mouseIsOnPlay;
        private bool _mouseIsOnPause;
        private bool _mouseIsOnStop;
        private bool _mouseIsOnRewind;
        private bool _mouseIsOnForward;
        private bool _mouseIsOnPrePage;
        private bool _mouseIsOnNextPage;
        #endregion

        #region Bitmaps and Cursors.
        private Bitmap _timeCursor = new Bitmap("Resources\\TimeCursor.png");
        private Bitmap _buttonStart = new Bitmap("Resources\\Play.png");
        private Bitmap _buttonPlaying = new Bitmap("Resources\\Playing.png");
        private Bitmap _buttonStop = new Bitmap("Resources\\Stop.png");
        private Bitmap _buttonStopUnenabled = new Bitmap("Resources\\StopUnenabled.png");
        private Bitmap _buttonPause = new Bitmap("Resources\\Pause.png");
        private Bitmap _buttonPausing = new Bitmap("Resources\\Pausing.png");
        private Bitmap _buttonForward = new Bitmap("Resources\\Forward.png");
        private Bitmap _buttonForwarding = new Bitmap("Resources\\Forwarding.png");
        private Bitmap _buttonRewind = new Bitmap("Resources\\Rewind.png");
        private Bitmap _buttonRewinding = new Bitmap("Resources\\Rewinding.png");
        private Bitmap _buttonInfo = new Bitmap("Resources\\InfoBlue.png");
        private Bitmap _buttonInfoPress = new Bitmap("Resources\\InfoGreen.png");
        private Bitmap _buttonPrePage = new Bitmap("Resources\\PrePage.png");
        private Bitmap _buttonNextPage = new Bitmap("Resources\\NextPage.png");
        private Bitmap _leftCursor = new Bitmap("Resources\\LeftCursor.png");
        private Bitmap _rightCursor = new Bitmap("Resources\\RightCursor.png");
        private Bitmap _note = new Bitmap("Resources\\Note.png");
        private Bitmap _noteSelect = new Bitmap("Resources\\NoteSelected.png");
        #endregion

        public Form1()//Initialize
        {
            InitializeComponent();
            _playState = playState.Stopped;
            _listKey.Clear();
            _openMp3 = "";
            _openRTM = "";
            _en1 = false; _en2 = false; _en3 = false;
            _page = 1;
            _pageView = 1;
            _title = "";
            _author = "";
            _maker = "";
            _description = "";
            _currentTime = 0;
            _tempTime = "";
            _keyCount = 0;
            _duration = 0;
            _keyInfoLock = false;
            _operator = operation.Default;
            _timeCursorPosition = new Point(0, 0);
            _select = false;
            _selecting = false;
            _selectStart = new Point(0, 0);
            _selectEnd = new Point(0, 0);
            _timeMousePosition = new Point(0, 0);
            _timeLineMousePosition = new Point(0, 0);
            _isCHangeCursorPosition = false;
        }
        private void Form1_KeyDown(object sender, KeyEventArgs e)//left and right key.
        {
            if (e.KeyCode == Keys.Left)//Press the direction-left key,rewind.
                _timeCursorPosition = new Point(_timeCursorPosition.X - 3, _timeCursorPosition.Y);
            else if (e.KeyCode == Keys.Right)//Press the direction-right key,forward.
                _timeCursorPosition = new Point(_timeCursorPosition.X + 3, _timeCursorPosition.Y);
            if (_timeCursorPosition.X < 0)
                _timeCursorPosition = new Point(0, _timeCursorPosition.Y);
            else if (_timeCursorPosition.X > 989)
                _timeCursorPosition = new Point(989, _timeCursorPosition.Y);
            _currentTime = cursorPositionToTime(_timeCursorPosition.X);
            ///autoNestle(1);
            RefreshAll(1, 0, 0, 0);
        }
        private void Form1_Load(object sender, EventArgs e)//for test.
        {
            pictureBoxWork.Cursor = Cursors.Default;
            //test
            _myKey = new MyKey();
            _myKey.Min = 0; _myKey.Sec = 2; _myKey.Ms = 0; _myKey.Key[2] = true; _myKey.Key[3] = true; _myKey.Count = 2;
            _listKey.Add(_myKey);
            _myKey = new MyKey();
            _myKey.Min = 0; _myKey.Sec = 3; _myKey.Ms = 0; _myKey.Key[2] = true; _myKey.Key[6] = true; _myKey.Count = 2;
            _listKey.Add(_myKey);
            //test
        }
        private void RefreshAll(int a = 0, int b = 0, int c = 0, int d = 0)//refresh all picturebox(default).
        {
            if (a == 0) pictureBoxLabel.Invalidate();
            if (b == 0) pictureBoxTimeLine.Invalidate();
            if (c == 0) pictureBoxWork.Invalidate();
            if (d == 0) pictureBoxTime.Invalidate();
        }
        //================================================================BEGIN==LabelSpace==BEGIN===================================================================
        #region LabelSpace
        private void pictureBoxLabel_Paint(object sender, PaintEventArgs e)//Draw info button.
        {
            Graphics g = e.Graphics;
            //song's info.
            g.DrawImage(_buttonInfo, 900, 0, 80, 80);
        }
        private bool judgeMouseInRectangle(int x, int y, Point rectangle, int width, int height)//Judge if mouse is in rectangle.
        {
            if (x > rectangle.X && y > rectangle.Y && x < rectangle.X + width && y < rectangle.Y + height)
                return true;
            return false;
        }
        private void pictureBoxLabel_MouseMove(object sender, MouseEventArgs e)//change mouse style if mouse is on info button.
        {
            //change cursor.
            if (judgeMouseInRectangle(e.X, e.Y, new Point(900, 15), 80, 50))
            {
                pictureBoxLabel.Cursor = Cursors.Hand;
            }
            else
            {
                pictureBoxLabel.Cursor = Cursors.Default;
            }
        }
        private void pictureBoxLabel_MouseDown(object sender, MouseEventArgs e)//change info button's color 
        {
            //change button info's color.
            if (judgeMouseInRectangle(e.X, e.Y, new Point(900, 15), 80, 50))
            {
                pictureBoxLabel.CreateGraphics().DrawImage(_buttonInfoPress, 900, 0, 80, 80);
            }
        }
        private void pictureBoxLabel_MouseUp(object sender, MouseEventArgs e)//show RTM info.
        {
            RefreshAll(0, 1, 1, 1);
            if (judgeMouseInRectangle(e.X, e.Y, new Point(900, 15), 80, 50))
            {
                //creat new info form.
                FormInfo NewInfo = new FormInfo();
                //set default.
                NewInfo.checkBoxEn1.Checked = _en1;
                NewInfo.checkBoxEn2.Checked = _en2;
                NewInfo.checkBoxEn3.Checked = _en3;
                NewInfo.textBoxAuthor.Text = _author;
                NewInfo.textBoxDescription.Text = _description;
                NewInfo.textBoxMaker.Text = _maker;
                NewInfo.textBoxTitle.Text = _title;
                if (NewInfo.ShowDialog() == DialogResult.OK)
                {
                    //save new info.
                    _title = NewInfo.textBoxTitle.Text;
                    _en1 = NewInfo.checkBoxEn1.Checked;
                    _en2 = NewInfo.checkBoxEn2.Checked;
                    _en3 = NewInfo.checkBoxEn3.Checked;
                    _author = NewInfo.textBoxAuthor.Text;
                    _maker = NewInfo.textBoxMaker.Text;
                    _description = NewInfo.textBoxDescription.Text;
                }
            }
        }
        #endregion
        //================================================================END==LabelSpace==END=======================================================================
        //================================================================BEGIN==TimeLineSpace==BEGIN================================================================
        #region TimeLineSpace
        private void autoNestle(int type)//Nestle to standard line.
        {
            if (type == -1)//NestleDown
            {
                for (int i = 0; i <= 30; i++)
                {
                    if (_timeCursorPosition.X - (1024.0 / 31 * i) < 0)
                    {
                        _timeCursorPosition.X = (int)(1024.0 / 31 * i);
                        break;
                    }
                    if (_timeCursorPosition.X - (1024.0 / 31 / 2 + 1024.0 / 31 * i) < 0)
                    {
                        _timeCursorPosition.X = (int)(1024.0 / 31 / 2 + 1024.0 / 31 * i);
                        break;
                    }
                }
                _currentTime = cursorPositionToTime(_timeCursorPosition.X);
            }
            else if (type == 0)//NestleUp
            {
                for (int i = 0; i <= 30; i++)
                {
                    if (_timeCursorPosition.X - (1024.0 / 31 * i) < 0)
                    {
                        _timeCursorPosition.X = (int)(1024.0 / 31 / 2 + 1024.0 / 31 * --i);
                        break;
                    }
                    if (_timeCursorPosition.X - (1024 / 31 / 2 + 1024.0 / 31 * i) < 0)
                    {
                        _timeCursorPosition.X = (int)(1024.0 / 31 * i);
                        break;
                    }
                }
                _currentTime = cursorPositionToTime(_timeCursorPosition.X);
            }
            else if (type == 1)
            {
                for (int i = 0; i <= 30; i++)
                {
                    if (Math.Abs(_timeCursorPosition.X - (1024 / 31 / 2 + 1024.0 / 31 * i)) < 2)
                    {
                        _timeCursorPosition.X = (int)(1024 / 31 / 2 + 1024.0 / 31 * i);
                    }
                    if (Math.Abs(_timeCursorPosition.X - (1024.0 / 31 * i)) < 2)
                    {
                        _timeCursorPosition.X = (int)(1024.0 / 31 * i);
                    }
                }
                _currentTime = cursorPositionToTime(_timeCursorPosition.X);
            }
            else if (type == 2)
            {
                for (int i = 0; i <= 30; i++)
                {
                    if (Math.Abs(_selectStart.X - (1024 / 31 / 2 + 1024.0 / 31 * i)) < 2)
                    {
                        _selectStart.X = (int)(1024 / 31 / 2 + 1024.0 / 31 * i) + 5;
                    }
                    if (Math.Abs(_selectStart.X - (1024.0 / 31 * i)) < 2)
                    {
                        _selectStart.X = (int)(1024.0 / 31 * i) + 5;
                    }
                }
            }
            else if (type == 3)
            {
                for (int i = 0; i <= 30; i++)
                {
                    if (Math.Abs(_selectEnd.X - (1024 / 31 / 2 + 1024.0 / 31 * i)) <= 3)
                    {
                        _selectEnd.X = (int)(1024 / 31 / 2 + 1024.0 / 31 * i) + 4;
                    }
                    if (Math.Abs(_selectEnd.X - (1024.0 / 31 * i)) < 3)
                    {
                        _selectEnd.X = (int)(1024.0 / 31 * i) + 4;
                    }
                }
            }
            RefreshAll(1, 0, 0, 0);
        }
        private double cursorPositionToTime(int posx)//return time by input position.
        {
            double time = (_pageView - 1) * 30;
            //4 + 1024.0 / 31 * i
            double i = (posx) / (1024.0 / 31);
            time += i;
            ///time += (i - (int)i);
            ///time += (posx - 0)* (30 * 1000 / (1024.0 / 31 * 30 - 12));
            ///time /= 1000;//formate to second.
            return time;
        }
        private int cursorTimeToPosition(double time)//return position.X by input time.
        {
            int x;
            time *= 1000;//formate to ms.
            time -= (_page - 1) * 30 - (_page - 1) * 2;
            x = (int)(time * (4 + 1024.0 / 31 * 30 - 12) / 1000 / 30);
            return x;
        }
        private void pictureBoxTimeLine_Paint(object sender, PaintEventArgs e)//Draw select cursor and cursor.
        {
            Graphics g = e.Graphics;
            for (int i = 0; i <= 30; i++)
            {
                if (_pageView > 3)
                {
                    if (i % 2 == 0)
                    {
                        if(i==0)
                            g.DrawString(((_pageView - 1) * 30 + i).ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.White), (int)(4 + 1024.0 / 31 * i - 4), 0);
                        else g.DrawString(((_pageView - 1) * 30 + i).ToString() + ".0", new Font("微软雅黑", 10), new SolidBrush(Color.White), (int)(4 + 1024.0 / 31 * i - 12), 0);
                    }
                }
                else
                {
                    if (i == 0)
                        g.DrawString(((_pageView - 1) * 30 + i).ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.White), (int)(4 + 1024.0 / 31 * i - 6), 0);
                    else g.DrawString(((_pageView - 1) * 30 + i).ToString() + ".0", new Font("微软雅黑", 10), new SolidBrush(Color.White), (int)(4 + 1024.0 / 31 * i - 12), 0);
                }

                g.DrawLine(new Pen(Color.White, 1), new Point((int)(4 + 1024 / 31 / 2 + 1024.0 / 31 * i), 40), new Point((int)(4 + 1024.0 / 31 / 2 + 1024 / 31 * i), 45));
                g.DrawLine(new Pen(Color.White, 1), new Point((int)(4 + 1024.0 / 31 * i), 30), new Point((int)(4 + 1024.0 / 31 * i), 45));
            }
            ///g.DrawString("1.0", new Font("微软雅黑", 10), new SolidBrush(Color.White), 12, 0);
            //Draw left and right cursors.
            if (_selectStart != _selectEnd)
            {
                g.DrawImage(_selectStart.X < _selectEnd.X ? _leftCursor : _rightCursor,
                    _selectStart.X < _selectEnd.X ? _selectStart.X - 20 : _selectStart.X, 30, 20, 15);
                g.DrawImage(_selectStart.X < _selectEnd.X ? _rightCursor : _leftCursor,
                    _selectStart.X < _selectEnd.X ? _selectEnd.X : _selectEnd.X - 20, 30, 20, 15);
            }
            ///g.DrawLine(new Pen(Color.White, 1), new Point(_timeLineMousePosition.X+1, 40), new Point(_timeLineMousePosition.X+1, 50));
            //draw cursor.
            g.DrawImage(_timeCursor, _timeCursorPosition.X - 8, 24, 25, 25);
        }
        private void timerChangeSelectCursor_Tick(object sender, EventArgs e)//Change Start Cursor.
        {
            _selectStart = new Point(_timeLineMousePosition.X + 5, _selectStart.Y);
            autoNestle(2);
            //Refresh
            RefreshAll(1, 0, 0, 0);
        }
        private void timerChangeEndCursor_Tick(object sender, EventArgs e)//Change End Cursor.
        {
            _selectEnd = new Point(_timeLineMousePosition.X - 5, _selectEnd.Y);
            autoNestle(3);
            //Refresh.
            RefreshAll(1, 0, 0, 1);
        }
        private void pictureBoxTimeLine_MouseMove(object sender, MouseEventArgs e)//change cursor style if mouse is on time cursors.
        {
            _timeLineMousePosition = e.Location;
            //change cursor to hand.
            if (judgeMouseInRectangle(e.X, e.Y, new Point(_timeCursorPosition.X - 1, 24), 25, 25))
            {
                pictureBoxTimeLine.Cursor = Cursors.Hand;
            }
            else if (judgeMouseInRectangle(e.X, e.Y, new Point(_selectStart.X < _selectEnd.X ? _selectStart.X - 20 : _selectStart.X, 30), 20, 15))
            {
                pictureBoxTimeLine.Cursor = Cursors.Hand;
            }
            else if (judgeMouseInRectangle(e.X, e.Y, new Point(_selectStart.X < _selectEnd.X ? _selectEnd.X : _selectEnd.X - 10, 30), 20, 15))
            {
                pictureBoxTimeLine.Cursor = Cursors.Hand;
            }
            else//change cursor to default.
            {
                pictureBoxTimeLine.Cursor = Cursors.Default;
            }
            _mousePositionWork = _timeLineMousePosition;
            ///RefreshAll(1,1,1,1);
        }
        private void pictureBoxTimeLine_MouseDown(object sender, MouseEventArgs e)//drag cursor.
        {
            //Swap StartPoint with EndPoint.
            if (_selectStart.X > _selectEnd.X)
            {
                Point tPoint = _selectEnd;
                _selectEnd = _selectStart;
                _selectStart = tPoint;
            }
            if (_selectStart != _selectEnd)
            {
                if (judgeMouseInRectangle(e.X, e.Y, new Point(_selectStart.X - 20, 30), 20, 15))
                {
                    pictureBoxTimeLine.Cursor = Cursors.Hand;
                    timerChangeStartCursor.Enabled = true;
                    autoNestle(1);
                }
                else if (judgeMouseInRectangle(e.X, e.Y, new Point(_selectEnd.X, 30), 20, 15))
                {
                    pictureBoxTimeLine.Cursor = Cursors.Hand;
                    timerChangeEndCursor.Enabled = true;
                }
            }
            //Drag select cursor at first.
            else
            ///if (judgeMouseInRectangle(e.X, e.Y, new Point(_timeCursorPosition.X - 1, 24), 25, 25))
            {
                if (_playState == playState.Playing)
                {
                    musicPlayer.Ctlcontrols.pause();
                    _currentTime = cursorPositionToTime(e.X - 6);
                    musicPlayer.Ctlcontrols.currentPosition = _currentTime;
                    musicPlayer.Ctlcontrols.play();
                }
                else
                    timerChangeCursorPositionByDragCursor.Enabled = true;
            }
        }
        private void timerChangeCursorPositionByDragCursor_Tick(object sender, EventArgs e)//Drag Cursor.
        {
            ///pictureBoxWork.CreateGraphics().DrawString(_timeLineMousePosition.X.ToString(), new Font("微软雅黑", 10), new SolidBrush(Color.White), 120, 120);
            if (_timeLineMousePosition.X - 6 < 0)
                _timeCursorPosition = new Point(0, _timeCursorPosition.Y);
            else if (_timeLineMousePosition.X - 6 > 989)
                _timeCursorPosition = new Point(989, _timeCursorPosition.Y);
            else
                _timeCursorPosition = new Point(_timeLineMousePosition.X - 6, _timeCursorPosition.Y);
            autoNestle(1);
            _currentTime = cursorPositionToTime(_timeCursorPosition.X);
            if (_playState == playState.Pausing)
            {
                musicPlayer.Ctlcontrols.currentPosition = _currentTime;
            }
            //Refresh
            RefreshAll(1, 0, 0, 0);
        }
        private void pictureBoxTimeLine_MouseUp(object sender, MouseEventArgs e)//end of drag.
        {
            timerChangeCursorPositionByDragCursor.Enabled = false;
            timerChangeStartCursor.Enabled = false;
            timerChangeEndCursor.Enabled = false;
        }
        #endregion
        //================================================================END==TimeLineSpace==END================================================================
        //================================================================BEGIN==WorkSpace==BEGIN================================================================
        #region WorkSpace
        private void pictureBox1_Paint(object sender, PaintEventArgs e)//draw background,line,notes and so on.
        {
            Graphics g = e.Graphics;
            //background.
            g.FillRectangle(new SolidBrush(Color.Black), 5, 5, 1014, 537);
            if (_selectStart != _selectEnd)
                g.FillRectangle(new SolidBrush(Color.White), Math.Min(_selectStart.X, _selectEnd.X), 5, Math.Abs(_selectStart.X - _selectEnd.X), 537);
            //line for A\S\D\F.
            for (int i = 1; i <= 4; i++)
            {
                g.DrawLine(new Pen(Color.DarkGreen, 1), new Point(5, (int)(537.0 / 2 / 5 * i)), new Point(1024 - 5, (int)(537.0 / 2 / 5 * i)));
            }
            //line for J\K\L\SEM.
            for (int i = 1; i <= 4; i++)
            {
                g.DrawLine(new Pen(Color.DarkGreen, 1), new Point(5, (int)(547.0 / 2 / 5 * i + 537.0 / 2)), new Point(1024 - 5, (int)(547.0 / 2 / 5 * i + 537.0 / 2)));
            }
            //line for time.
            for (int i = 0; i <= 30; i++)
            {
                g.DrawLine(new Pen(Color.DarkGreen, 1), new Point((int)(4 + 1024.0 / 31 * i), 5), new Point((int)(4 + 1024.0 / 31 * i), 540));
            }
            //line divide two side.also for G\H\SPACE.
            g.DrawLine(new Pen(SystemColors.ActiveBorder, 2), new Point(5, (int)(537.0 / 2)), new Point(1024 - 5, (int)(537.0 / 2)));
            //draw time line(red).
            g.DrawLine(new Pen(Color.Red, 1), new Point(_timeCursorPosition.X + 5, 5), new Point(_timeCursorPosition.X + 5, 540));
            //draw all notes.
            for (int i = 0; i < _listKey.Count; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (_listKey[i].Key[j] == true)
                    {
                        if (_keyInfo.i == i && _keyInfo.j == j && _operator == operation.Move) { }
                        else if (_keyInfo.i == i && _keyInfo.j == j && _operator == operation.Select)
                        {
                            g.DrawImage(_noteSelect, cursorTimeToPosition(keyTimeCalcu(_listKey[i])), (int)(537.0 / 2 / 5 * j - 45 / 2), 10, 45);
                        }
                        else g.DrawImage(_note, cursorTimeToPosition(keyTimeCalcu(_listKey[i])), (int)(537.0 / 2 / 5 * j - 45 / 2), 10, 45);
                    }
                }
            }
            //draw note if move note.
            if (_operator == operation.Move)
            {
                #region adjust Y.
                int NestleY = 0;
                if (_mousePositionWork.Y < 547.0 / 2 / 5 + 547.0 / 2 / 5 / 2)
                {
                    NestleY = (int)(547.0 / 5 / 2);
                }
                else if (_mousePositionWork.Y > 547 / 2 + 547.0 / 2 / 5 * 3 + 547.0 / 5 / 2 / 2)
                {
                    NestleY = (int)(547.0 / 5 / 2 * 4 + 547.0 / 2);
                }
                else if (Math.Abs(_mousePositionWork.Y - 547.0 / 2) <= 547.0 / 5 / 2 / 2)
                { NestleY = (int)(547.0 / 2); }
                else if (_mousePositionWork.Y > 547.0 / 2 + 547.0 / 2 / 2 / 5)
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        double line = 537.0 / 2 / 5 * i + 547.0 / 2;
                        if (Math.Abs(_mousePositionWork.Y - line) <= 547.0 / 5 / 2 / 2)
                        {
                            NestleY = (int)line;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 1; i <= 4; i++)
                    {
                        double line = 547.0 / 2 / 5 * i;
                        if (Math.Abs(_mousePositionWork.Y - line) <= 547.0 / 5 / 2 / 2)
                        {
                            NestleY = (int)line;
                            break;
                        }
                    }
                }
                #endregion
                #region adjust X.
                int NestleX;
                if (_mousePositionWork.X < 6)
                    NestleX = 6;
                else if (_mousePositionWork.X > 994)
                    NestleX = 994;
                else NestleX = _mousePositionWork.X;
                #endregion
                g.DrawImage(_note, NestleX - 5, NestleY - 25, 10, 45);
            }
        }
        private void timerMoveNote_Tick(object sender, EventArgs e)//now is useless.
        {
            //Update notes' position.
        }
        private void pictureBoxWork_MouseDown(object sender, MouseEventArgs e)//show right button menu;select note(s).
        {
            if (e.Button == MouseButtons.Right && pictureBoxWork.Cursor == Cursors.Hand)
            {
                //show right button menu.
                rightButtonMenu.Show(Location.X + panelWork.Location.X + e.X, Location.Y + panelWork.Location.Y + e.Y);
                return;
            }
            if (e.Button == MouseButtons.Right && e.X > _selectStart.X && e.X < _selectEnd.X)
            {
                rightButtonMenu.Show(Location.X + panelWork.Location.X + e.X, Location.Y + panelWork.Location.Y + e.Y);
                return;
            }
            if (_operator == operation.Select)//without this,select one note,then click other note will cancle selection.
            {
                if (pictureBoxWork.Cursor == Cursors.Hand)
                {
                    _operator = operation.Move;
                    pictureBoxWork.Cursor = Cursors.SizeAll;
                    _keyInfo = _keyInfoTmp;
                    _keyInfoLock = true;
                }
            }
            if (_operator == operation.Default)
            {
                if (pictureBoxWork.Cursor == Cursors.Hand)
                {
                    ///_operator = operation.Select;
                    _operator = operation.Move;
                    pictureBoxWork.Cursor = Cursors.SizeAll;
                    _keyInfoLock = true;
                }
                else
                {
                    pictureBoxWork.Cursor = Cursors.IBeam;
                    _selectStart = new Point(e.X, e.Y);
                    _selectEnd = _selectStart;
                    _selecting = true;
                }
            }
        }
        private double keyTimeCalcu(MyKey key)//calculate time formate as second by MyKey.
        {
            double res = key.Min * 60 + key.Sec + key.Ms / 1000.0;
            return res;
        }
        private void pictureBoxWork_MouseMove(object sender, MouseEventArgs e)//Move notes;change cursor style if mouse is on note.
        {
            _mousePositionWork = e.Location;//Update mousePosition.
            if (_operator == operation.Move)//move note.
            {
                //draw note at position where mouse is.=>pictureBox1_Paint()
                RefreshAll(1, 1, 0, 1);
                return;
            }
            if (_selecting == true)
            {
                _selectEnd = new Point(e.X, e.Y);//set end point
                if (_selectEnd.X < 6)
                    _selectEnd = new Point(6, 0);
                else if (_selectEnd.X > 994)
                    _selectEnd = new Point(994, 0);
                if (_selectStart.X < 6)
                    _selectStart = new Point(6, 0);
                else if (_selectStart.X >= 994)
                    _selectStart = new Point(994, 0);
                RefreshAll(1, 0, 0, 0);
            }
            if (_playState == playState.Pausing || _playState == playState.Stopped)
            {
                #region change cursor style if mouse is on keys.
                int pos = _keyCount / 2;
                bool notOnAnyKey = true;
                while (keyTimeCalcu(_listKey[pos]) > (_page - 1) * 30)
                {
                    if (pos == 0) break;
                    pos /= 2;
                }
                for (int i = pos; i < _listKey.Count; i++)
                {
                    if (keyTimeCalcu(_listKey[pos]) < (_page - 1) * 30) continue;
                    else if (keyTimeCalcu(_listKey[pos]) >= (_page) * 30) break;
                    else
                    {
                        //g.DrawImage(_note, cursorTimeToPosition(keyTimeCalcu(_listKey[i])), (int)(537.0 / 2 / 5 * j - 45 / 2), 10, 45);
                        for (int j = 0; j < 9; j++)
                        {
                            if (_listKey[i].Key[j] == true)
                            {
                                if (judgeMouseInRectangle(e.X, e.Y, new Point(cursorTimeToPosition(keyTimeCalcu(_listKey[i])), (int)(537.0 / 2 / 5 * j - 45 / 2)), 10, 45))
                                {
                                    pictureBoxWork.Cursor = Cursors.Hand; notOnAnyKey = false;
                                    if (_keyInfoLock == false)
                                    { _keyInfo.i = i; _keyInfo.j = j; }
                                    else
                                    { _keyInfoTmp.i = i; _keyInfoTmp.j = j; }
                                }
                            }
                        }
                    }
                }
                if (notOnAnyKey == true)
                {
                    pictureBoxWork.Cursor = Cursors.Default;
                }
                #endregion
                _timeLineMousePosition = _mousePositionWork;
            }
        }
        private void pictureBoxWork_MouseUp(object sender, MouseEventArgs e)//The end of all operation in WorkSpace.
        {
            if (_selectStart == _selectEnd)
            {
                _select = false;
                ///_selecting = false;
            }
            if (_selecting == true)
            {
                _selecting = false;
            }
            if (_operator == operation.Select)
            {
                _operator = operation.Default;
                _keyInfoLock = false;
            }
            if (_operator == operation.Move)//select the note after move it.
            {
                for (int i = 1; i <= 4; i++)
                {
                    if (e.Y - (537.0 / 2 / 5 * i) <= 537.0 / 2 / 5)
                    {
                        //这里和下面写移动的代码，这之前要完成切换页面，不然改起来很麻烦。
                    }
                }
                for (int i = 1; i <= 4; i++)
                {
                    if (e.Y - (547.0 / 2 / 5 * i + 537.0 / 2) <= 537.0 / 2 / 5)
                    {
                        //
                    }
                }
                _operator = operation.Select;
            }
            pictureBoxWork.Cursor = Cursors.Default;
            RefreshAll(1, 0, 0, 0);
        }
        #endregion
        //================================================================END==WorkSpace==END====================================================================
        //================================================================BEGIN==TimeSpace==BEGIN================================================================
        #region TimeSpace
        private void pictureBox2_Paint(object sender, PaintEventArgs e)//Draw the time and the buttons.
        {
            //creat graphics.Onliy for convinience.
            Graphics g = e.Graphics;
            //if drag the time,set time's backcolor to black.
            if (_isCHangeCursorPosition)
                g.FillRectangle(new SolidBrush(Color.Black), 10, 10, 167, 51);
            //draw time.
            g.DrawString(timeDoubleToString(), new Font("微软雅黑", 25), new SolidBrush(SystemColors.HotTrack), 12, 12);
            //draw buttons' background.
            if (_mouseIsOnPlay)
                g.FillRectangle(new SolidBrush(Color.Black), 400, 20, 35, 35);
            else if (_mouseIsOnPause)
                g.FillRectangle(new SolidBrush(Color.Black), 450, 20, 35, 35);
            else if (_mouseIsOnStop)
                g.FillRectangle(new SolidBrush(Color.Black), 500, 20, 35, 35);
            else if (_mouseIsOnRewind)
                g.FillRectangle(new SolidBrush(Color.Black), 550, 20, 35, 35);
            else if (_mouseIsOnForward)
                g.FillRectangle(new SolidBrush(Color.Black), 600, 20, 35, 35);
            else if (_mouseIsOnNextPage)
                g.FillRectangle(new SolidBrush(Color.Black), 650, 20, 35, 35);
            else if (_mouseIsOnPrePage)
                g.FillRectangle(new SolidBrush(Color.Black), 350, 20, 35, 35);
            //draw buttons
            if (_playState == playState.Playing || _playState == playState.ForwardingAndPlaying || _playState == playState.RewindingAndPlaying)
                g.DrawImage(_buttonPlaying, 405, 25, 25, 25);
            else
                g.DrawImage(_buttonStart, 405, 25, 25, 25);
            if (_playState == playState.Pausing || _playState == playState.ForwardingAndPausing || _playState == playState.RewindingAndPausing)
                g.DrawImage(_buttonPausing, 455, 25, 25, 25);
            else
                g.DrawImage(_buttonPause, 455, 25, 25, 25);
            if (_playState == playState.Stopped)
                g.DrawImage(_buttonStopUnenabled, 505, 25, 25, 25);
            else
                g.DrawImage(_buttonStop, 505, 25, 25, 25);
            if (_playState == playState.RewindingAndPausing || _playState == playState.RewindingAndPlaying)
                g.DrawImage(_buttonRewinding, 555, 25, 25, 25);
            else
                g.DrawImage(_buttonRewind, 555, 25, 25, 25);
            if (_playState == playState.ForwardingAndPausing || _playState == playState.ForwardingAndPlaying)
                g.DrawImage(_buttonForwarding, 605, 25, 25, 25);
            else
                g.DrawImage(_buttonForward, 605, 25, 25, 25);
            g.DrawImage(_buttonPrePage, 355, 25, 25, 25);//page up
            g.DrawImage(_buttonNextPage, 655, 25, 25, 25);//page down
        }
        private void textBox1_TextChanged(object sender, EventArgs e)//Contol the format of input text.
        {
            if (textBoxTimeInput.Text.Length > 9)
            {
                //reload _tempTime to textbox.Means input is invalid.
                textBoxTimeInput.Text = _tempTime;
                //put cursor to last.
                textBoxTimeInput.SelectionStart = 9;
            }
            else
                //if length <= 9,save text to _tempTime.
                _tempTime = textBoxTimeInput.Text;

        }
        private void pictureBoxTime_MouseDown(object sender, MouseEventArgs e)//TimeSpace.Click and move mouse,change the cursor position.
        {
            if (_openMp3 == "")
                return;
            if (_timeMousePosition.X >= 10 && _timeMousePosition.Y >= 10 && _timeMousePosition.X <= 177 && _timeMousePosition.Y <= 61)
            {
                //set flag true.
                _isCHangeCursorPosition = true;
                //save mouse position.
                _mousePositionFormStart = MousePosition;
                //start scan.
                timerChangeCursorPosition.Enabled = true;
            }
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(550, 20), 15))
            {
                if (_playState != playState.Stopped && _mouseIsOnRewind)
                {
                    if (_playState == playState.Pausing)
                        _playState = playState.RewindingAndPausing;
                    else if (_playState == playState.Playing)
                        _playState = playState.RewindingAndPlaying;
                }
            }
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(600, 20), 15))
            {
                if (_playState != playState.Stopped && _mouseIsOnForward)
                {
                    if (_playState == playState.Pausing)
                        _playState = playState.ForwardingAndPausing;
                    else if (_playState == playState.Playing)
                        _playState = playState.ForwardingAndPlaying;
                }
            }
            //refresh
            RefreshAll(1, 0, 0, 0);
        }
        private void pictureBoxTime_MouseUp(object sender, MouseEventArgs e)//TimeSpace.Stop update the timecursor.
        {
            if (_timeMousePosition.X >= 10 && _timeMousePosition.Y >= 10 && _timeMousePosition.X <= 177 && _timeMousePosition.Y <= 61)
            {
                //if mouse in time rectangle,show textbox for input.
                textBoxTimeInput.Text = timeDoubleToString();
                textBoxTimeInput.Visible = true;
                return;
            }
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(550, 20), 15))
            {
                if (_playState == playState.RewindingAndPausing)
                    _playState = playState.Pausing;
                else if (_playState == playState.RewindingAndPlaying)
                    _playState = playState.Playing;
            }
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(600, 20), 15))
            {
                if (_playState == playState.ForwardingAndPausing)
                    _playState = playState.Pausing;
                else if (_playState == playState.ForwardingAndPlaying)
                    _playState = playState.Playing;
            }
            //set flag false.
            _isCHangeCursorPosition = false;
            //refresh
            RefreshAll(1, 1, 1, 0);
            //stop change timecursorposition.
            timerChangeCursorPosition.Enabled = false;
        }
        private void pictureBoxTime_MouseMove(object sender, MouseEventArgs e)//TimeSpace.If move mouse on the time,change cursor to VSplit.
        {
            if (_openMp3 == "") return;
            //Update mouse position.
            _timeMousePosition = e.Location;
            //if is change time cursor position,keep the cursor style.
            if (_isCHangeCursorPosition)
                return;
            //Change cursor by mouse position.
            if (e.X >= 10 && e.Y >= 10 && e.X <= 177 && e.Y <= 61)
            {
                //Change Mouse Cursor.
                pictureBoxTime.Cursor = Cursors.VSplit;
            }
            //Play.
            else if (judgeMouseInCircle(e.X, e.Y, new Point(400, 20), 15))
            {
                pictureBoxTime.Cursor = Cursors.Hand;
                if (!_mouseIsOnPlay)//Prevent from drawing everytimes mouse move.
                {
                    _mouseIsOnPlay = true;
                }
            }
            //Pause.
            else if (judgeMouseInCircle(e.X, e.Y, new Point(450, 20), 15))
            {
                pictureBoxTime.Cursor = Cursors.Hand;
                if (_playState == playState.Playing || _playState == playState.Pausing && !_mouseIsOnPause)
                {
                    _mouseIsOnPause = true;
                }
            }
            //Stop.
            else if (judgeMouseInCircle(e.X, e.Y, new Point(500, 20), 15))
            {
                pictureBoxTime.Cursor = Cursors.Hand;
                if ((_playState == playState.Pausing || _playState == playState.Playing) && !_mouseIsOnStop)
                {
                    _mouseIsOnStop = true;
                }
            }
            //Rewind.
            else if (judgeMouseInCircle(e.X, e.Y, new Point(550, 20), 15))
            {
                pictureBoxTime.Cursor = Cursors.Hand;
                if (_playState != playState.Stopped && !_mouseIsOnRewind)
                {
                    _mouseIsOnRewind = true;
                }
            }
            //Forward.
            else if (judgeMouseInCircle(e.X, e.Y, new Point(600, 20), 15))
            {
                pictureBoxTime.Cursor = Cursors.Hand;
                if (_playState != playState.Stopped && !_mouseIsOnForward)
                {
                    _mouseIsOnForward = true;
                }
            }
            //PrePage
            else if (judgeMouseInCircle(e.X, e.Y, new Point(350, 20), 15))
            {
                if (_pageView == 1) { }
                else
                {
                    pictureBoxTime.Cursor = Cursors.Hand;
                    _mouseIsOnPrePage = true;
                }
            }
            //NextPage
            else if (judgeMouseInCircle(e.X, e.Y, new Point(650, 20), 15))
            {
                if (_pageView == _page) { }
                else
                {
                    pictureBoxTime.Cursor = Cursors.Hand;
                    _mouseIsOnNextPage = true;
                }
            }
            else
            {
                //If mouse in other place ,change to default.
                pictureBoxTime.Cursor = Cursors.Default;
                //clear all flags.
                _mouseIsOnPause = false;
                _mouseIsOnPlay = false;
                _mouseIsOnStop = false;
                _mouseIsOnForward = false;
                _mouseIsOnRewind = false;
                _mouseIsOnPrePage = false;
                _mouseIsOnNextPage = false;
                //fowarding and rewinding,mouse must on the buttons.
                if (_playState == playState.RewindingAndPausing)
                    _playState = playState.Pausing;
                else if (_playState == playState.RewindingAndPlaying)
                    _playState = playState.Playing;
                if (_playState == playState.ForwardingAndPausing)
                    _playState = playState.Pausing;
                else if (_playState == playState.ForwardingAndPlaying)
                    _playState = playState.Playing;
            }
            //refresh.
            RefreshAll(1, 1, 1, 0);
        }
        private void timerChangeCursorPosition_Tick(object sender, EventArgs e) //TimerSpace.Change time cursor position by timer.
        {
            _mousePositionForm = MousePosition;
            int moveStep = _mousePositionForm.X - _mousePositionForm.Y;
            _timeCursorPosition.X += moveStep;
            RefreshAll(1, 0, 0, 0);
        }
        private bool checkInvalid(string time)//check if the input time is valid.
        {
            if (textBoxTimeInput.Visible == false)
                return true;
            //3rd and 5th must be ':' and '.'
            if (time.Length != 9 || time[2] != ':' || time[5] != '.')
                return false;
            //get time by string.
            ///int ttime = ((time[0] - '0') * 10 + (time[1] - '0')) * 60000 + ((time[3] - '0') * 10 + (time[4] - '0') * 10) * 1000 + ((time[6] - '0') * 100 + (time[7] - '0') * 10 + (time[8] - '0'));
            //if time more than duration,return false.
            if (timeStringToDouble(time) > _duration)
                return false;
            //return true.
            return true;
        }
        private void textBoxTimeInput_KeyDown_clickOtherPlace()//click other area will accept the input.
        {
            if (textBoxTimeInput.Visible == false)
                return;
            if (checkInvalid(textBoxTimeInput.Text) == true)
            {
                //Update current time.
                _currentTime = timeStringToDouble(textBoxTimeInput.Text);
            }
            else
            {
                //show error message.
                ///MessageBox.Show(this, "The time you input is invalid!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                //restore info.
                textBoxTimeInput.Text = timeDoubleToString();
            }
            //Refresh
            RefreshAll(1, 0, 0, 0);
            //set unvisible
            textBoxTimeInput.Visible = false;
            //set flag false.
            _isCHangeCursorPosition = false;
        }
        private void textBoxTimeInput_KeyDown(object sender, KeyEventArgs e) //press enter to accpet the input.
        {
            //Enter Key.
            if (e.KeyCode == Keys.Enter)
            {
                textBoxTimeInput_KeyDown_clickOtherPlace();
            }
        }
        private void pictureBoxTime_Click(object sender, EventArgs e)//Click and change cursor position.
        {
            if (_openMp3 == "") return;
            if (_timeMousePosition.X >= 10 && _timeMousePosition.Y >= 10 && _timeMousePosition.X <= 177 && _timeMousePosition.Y <= 61)
            { }
            //Play.
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(400, 20), 15))
            {
                if (_openMp3 == "")
                    return;
                if (musicPlayer.URL == "")
                    musicPlayer.URL = _openMp3;
                _playState = playState.Playing;
                timerTime.Enabled = true;
                musicPlayer.Ctlcontrols.currentPosition = _currentTime;
                musicPlayer.Ctlcontrols.play();
            }
            //Pause.
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(450, 20), 15))
            {
                if (_playState == playState.Playing)
                {
                    _playState = playState.Pausing;
                    musicPlayer.Ctlcontrols.pause();
                }
                else if (_playState == playState.Pausing)
                {
                    timerTime.Enabled = true;
                    _playState = playState.Playing;
                    musicPlayer.Ctlcontrols.play();
                }
            }
            //Stop.
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(500, 20), 15))
            {
                _playState = playState.Stopped;
                musicPlayer.Ctlcontrols.stop();
                _currentTime = 0;
                _timeCursorPosition = new Point(0, 0);
            }
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(650, 20), 15))//NextPage.
            {
                if (_pageView == _page) { }
                else {
                    _pageView++;
                    RefreshAll(1, 0, 0, 0);
                }
            }
            else if (judgeMouseInCircle(_timeMousePosition.X, _timeMousePosition.Y, new Point(350, 20), 15))//PrePage
            {
                if (_pageView == 1) { }
                else {
                    _pageView--;
                    RefreshAll(1, 0, 0, 0);
                }
            }
            else //if click in other zone,submit the input.
                textBoxTimeInput_KeyDown_clickOtherPlace();
            RefreshAll(1, 0, 0, 0);
        }
        private bool judgeMouseInCircle(int mouseX, int MouseY, Point circle, double r)//judge if the mouse is in area of buttons.
        {
            circle.X += (int)r;
            circle.Y += (int)r;
            if ((MouseY - circle.Y) * (MouseY - circle.Y) + (mouseX - circle.X) * (mouseX - circle.X) <= r * r)
            {
                return true;
            }
            return false;
        }
        private double timeStringToDouble(string newTime)//change time from string to double.
        {
            double time = 0;
            time = ((newTime[0] - '0') * 10 + (newTime[1] - '0')) * 60 + ((newTime[3] - '0') * 10 + (newTime[4] - '0')) + ((newTime[6] - '0') * 100 + (newTime[7] - '0') * 10 + (newTime[8] - '0')) * 0.001;
            return time;
        }
        private string timeDoubleToString()//change time from double to string.
        {
            int min = (int)(_currentTime / 60);
            int sec = (int)(_currentTime % 60);
            int msec = (int)((_currentTime - (int)_currentTime) * 1000);
            string currentTime = "0" + min.ToString() + ":";
            if (sec < 10)
                currentTime += "0";
            currentTime += sec.ToString() + ".";
            if (msec < 10)
                currentTime += "00";
            else if (msec < 100)
                currentTime += "0";
            currentTime += msec.ToString();
            return currentTime;
        }
        private void timerTime_Tick(object sender, EventArgs e)//Control the musicPlayer and times.
        {
            if (_playState == playState.Playing)
            {
                if (musicPlayer.playState == WMPLib.WMPPlayState.wmppsStopped)
                {
                    _playState = playState.Stopped;
                }
                //Update shown time.
                ///double tposition = musicPlayer.Ctlcontrols.currentPosition;
                _currentTime = musicPlayer.Ctlcontrols.currentPosition;
                //Update cursor's position.
                _timeCursorPosition = new Point((int)(((0 + 4 + 1024.0 / 31 * 30 - 12) / 30000.0) * _currentTime * 1000), _timeCursorPosition.Y);
                //Refresh.
                RefreshAll(1, 0, 0, 0);
            }
            else
            {
                //Stop automaticly.
                timerTime.Enabled = false;
                RefreshAll(1, 0, 0, 0);
            }
        }
        #endregion
        //================================================================END==TimeSpace==END====================================================================
        //================================================================BEGIN==MenuItems==BEGIN================================================================
        #region MenuItems
        private void MenuItemExit_Click(object sender, EventArgs e)//Exit Program.
        {
            Close();
        }
        private void MenuItemOpne_Click(object sender, EventArgs e) //Open File.
        {
            if (openFileDlg.ShowDialog() == DialogResult.OK)
            {
                string extension = System.IO.Path.GetExtension(openFileDlg.FileName);
                if (extension.ToLower() == ".mp3")//mp3 file
                {
                    //musicPlayer.settings.volume = 0;
                    _currentTime = 0;
                    _openMp3 = openFileDlg.FileName;
                    musicPlayer.URL = _openMp3;
                    timerGetDuration.Enabled = true;
                    return;
                }
                else if (extension.ToLower() == ".rtm")//rtm music file.
                {
                    _openRTM = openFileDlg.FileName;
                    return;
                }
            }
            else return;
        }
        private void MenuItemNestle_Click(object sender, EventArgs e)//nestle to left.
        {
            if (_currentTime > 0)
                autoNestle(0);
        }
        private void timerGetDuration_Tick(object sender, EventArgs e)
        {
            if (musicPlayer.playState == WMPLib.WMPPlayState.wmppsPlaying)
            {
                _duration = musicPlayer.currentMedia.duration;
                musicPlayer.URL = "";
                musicPlayer.settings.volume = 100;
                _page = (int)(Math.Ceiling(_duration / 30));//Calculate how many pages.
                _pageView = 1;
                timerGetDuration.Enabled = false;
            }
        }
        private void MenuItemNestleDown_Click(object sender, EventArgs e)//nestle to right.
        {
            if (_currentTime <= 30)
                autoNestle(-1);
        }
        private void MenuItemClearSelect_Click(object sender, EventArgs e) //Clear select area.
        {
            _selectStart = new Point(0, 0);
            _selectEnd = new Point(0, 0);
        }
        #endregion
        //================================================================END==MenuItems==END====================================================================
    }
}