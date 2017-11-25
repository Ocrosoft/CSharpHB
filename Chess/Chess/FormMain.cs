using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Media;
using System.Reflection;
using System.Resources;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Text.RegularExpressions;
using Microsoft.Win32;

namespace Chess
{
    public partial class FormMain : Form
    {
        //Chess类对象
        private Chess _chess = new Chess();
        private List<Step> _listStep = new List<Step>();
        //客户端Socket对象,用于与服务端进行通讯
        private Socket _socketClient = null;
        //线程对象,用于客户端接收服务端数据包
        private Thread threadReceivePacket = null;
        //本地客户端IP、端口、昵称、状态
        private IPAddress _localIP = IPAddress.Parse("0.0.0.0");
        private int _localPort = 0;
        private string _localNickName = "";
        private ClientState _localState = ClientState.离线;
        //服务端IP、端口
        private int _serverPort = 0;
        private IPAddress _serverIP = IPAddress.Parse("0.0.0.0");
        //下棋对手IP、端口、昵称
        private IPAddress _opponentIP = IPAddress.Parse("0.0.0.0");
        private int _opponentPort = 0;
        private string _oppoentNickName = "";
        //下挑战书的对话框
        DlgSendChallenge dlgSendChallenge = null;
        //接挑战书的对话框
        DlgAcceptChallenge dlgAcceptChallenge = null;
        private int _rowHeight = 60;
        private int _columWidth = 60;
        //棋盘左上角坐标
        private Point _leftTop = new Point(80, 100);
        //棋子半径
        private int _pieceRadius = 29;
        private Point _mouseMovePoint;
        //AI
        private bool _ai = false;
        //背景图片
        Bitmap bmoBG = Properties.Resources.棋盘桌面;
        public FormMain()
        {
            InitializeComponent();
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            _rowHeight = Screen.PrimaryScreen.Bounds.Size.Height / 14;
            _columWidth = _rowHeight;
            for (int row = 1; row <= 10; row++)
                for (int col = 1; col <= 9; col++)
                    _chess._Piece[row, col] = Piece.无子;
        }
        public void DrawChessBoard(Graphics g)
        {
            g.DrawImage(bmoBG, new Point(0, 0));
            Pen thisPen = new Pen(Color.Black, 2);
            Pen thickPen = new Pen(Color.Black, 6);
            g.DrawRectangle(thickPen, new Rectangle(_leftTop.X - 10, _leftTop.Y - 10, _columWidth * 8 + 20, _rowHeight * 9 + 20));
            for (int row = 1; row <= 10; row++)
            {
                g.DrawLine(thisPen, new Point(_leftTop.X, _leftTop.Y + _rowHeight * (row - 1)),
                                    new Point(_leftTop.X + 8 * _columWidth, _leftTop.Y + _rowHeight * (row - 1)));
            }
            for (int col = 1; col <= 9; col++)
            {
                g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * (col - 1), _leftTop.Y),
                                    new Point(_leftTop.X + _columWidth * (col - 1), _leftTop.Y + 4 * _columWidth));
                g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * (col - 1), _leftTop.Y + 5 * _columWidth),
                                    new Point(_leftTop.X + _columWidth * (col - 1), _leftTop.Y + 9 * _columWidth));
            }
            g.DrawLine(thisPen, new Point(_leftTop.X, _leftTop.Y + 4 * _columWidth),
                                    new Point(_leftTop.X, _leftTop.Y + 5 * _columWidth));
            g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * 8, _leftTop.Y + 4 * _columWidth),
                                    new Point(_leftTop.X + _columWidth * 8, _leftTop.Y + 5 * _columWidth));

            g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * 3, _leftTop.Y),
                                    new Point(_leftTop.X + _columWidth * 5, _leftTop.Y + 2 * _columWidth));
            g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * 3, _leftTop.Y + 2 * _columWidth),
                                    new Point(_leftTop.X + _columWidth * 5, _leftTop.Y));
            g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * 3, _leftTop.Y + 7 * _columWidth),
                                    new Point(_leftTop.X + _columWidth * 5, _leftTop.Y + 9 * _columWidth));
            g.DrawLine(thisPen, new Point(_leftTop.X + _columWidth * 3, _leftTop.Y + 9 * _columWidth),
                                    new Point(_leftTop.X + _columWidth * 5, _leftTop.Y + 7 * _columWidth));
            Font font1 = new Font("隶书", (float)(_rowHeight * 0.8), FontStyle.Regular, GraphicsUnit.Pixel);
            SolidBrush fontBrush = new SolidBrush(Color.Black);
            g.DrawString("楚河", font1, fontBrush, new Point(_leftTop.X + _columWidth, (int)(_leftTop.Y + _rowHeight * 4.1)));
            g.DrawString("汉界", font1, fontBrush, new Point((int)(_leftTop.X + _columWidth * 5), (int)(_leftTop.Y + _rowHeight * 4.1)));
            Font font2 = new Font("黑体", (float)(_rowHeight * 0.6), FontStyle.Regular, GraphicsUnit.Pixel);
            for (int row = 1; row <= 10; row++)
            {
                g.DrawString(row.ToString(), font2, fontBrush, new Point((int)(_leftTop.X + _columWidth * 8.4),
                    (int)(_leftTop.Y - _rowHeight * 0.3 + _rowHeight * (row - 1))));
            }
            string[] colNumber = new string[9] { "一", "二", "三", "四", "五", "六", "七", "八", "九" };
            Font font3 = new Font("黑体", (float)(_rowHeight * 0.5), FontStyle.Regular, GraphicsUnit.Pixel);
            for (int col = 1; col <= 9; col++)
            {
                g.DrawString(colNumber[col - 1], font3, fontBrush, new Point((int)(_leftTop.X - _rowHeight * 0.3 + _columWidth * (col - 1)),
                    (int)(_leftTop.Y + _columWidth * 9.4)));
            }
            g.DrawString("黑方", font3, fontBrush, new Point((int)(_leftTop.X + _columWidth * 8.4 + 45),
                    (int)(_leftTop.Y - _rowHeight * 0.3 + _rowHeight * 2)));
            g.DrawString("红方", font3, fontBrush, new Point((int)(_leftTop.X + _columWidth * 8.4 + 45),
                    (int)(_leftTop.Y - _rowHeight * 0.3 + _rowHeight * 7)));
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 1, _leftTop.Y + _rowHeight * 2), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 7, _leftTop.Y + _rowHeight * 2), true, true, true, true);
            //
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 0, _leftTop.Y + _rowHeight * 3), false, true, false, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 2, _leftTop.Y + _rowHeight * 3), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 4, _leftTop.Y + _rowHeight * 3), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 6, _leftTop.Y + _rowHeight * 3), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 8, _leftTop.Y + _rowHeight * 3), true, false, true, false);
            //
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 0, _leftTop.Y + _rowHeight * 6), false, true, false, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 2, _leftTop.Y + _rowHeight * 6), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 4, _leftTop.Y + _rowHeight * 6), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 6, _leftTop.Y + _rowHeight * 6), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 8, _leftTop.Y + _rowHeight * 6), true, false, true, false);
            //
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 1, _leftTop.Y + _rowHeight * 7), true, true, true, true);
            DrawCrossLine(g, new Point(_leftTop.X + _columWidth * 7, _leftTop.Y + _rowHeight * 7), true, true, true, true);
            if (_chess._CurPlayer == Player.红)
                g.DrawImage(Properties.Resources.红方头像, new Point(_leftTop.X + 8 * _columWidth + 100, _leftTop.Y + 8 * _rowHeight));
            else if (_chess._CurPlayer == Player.黑)
                g.DrawImage(Properties.Resources.蓝方头像, new Point(_leftTop.X + 8 * _columWidth + 100, _leftTop.Y + 0 * _rowHeight - 15));
            if (_oppoentNickName == "")
                this.Text = "中国象棋";
            else
                this.Text = "中国象棋 - [我方-" + _localNickName + "] vs [敌方-" + _oppoentNickName + "]";
        }
        public void DrawCrossLine(Graphics g, Point center, Boolean leftTop, Boolean rightTop, Boolean leftBottom, Boolean rightBottom)
        {
            int offset = 4, length = 8;
            Point corner = new Point();
            Pen thinPen = new Pen(Color.Black, 2);
            corner.X = center.X - offset;
            corner.Y = center.Y - offset;
            if (leftTop)
            {
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X - length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y - length));
            }
            corner.X = center.X + offset;
            corner.Y = center.Y - offset;
            if (rightTop)
            {
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X + length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y - length), new Point(corner.X, corner.Y));
            }
            corner.X = center.X - offset;
            corner.Y = center.Y + offset;
            if (leftBottom)
            {
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X - length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y + length));
            }
            corner.X = center.X + offset;
            corner.Y = center.Y + offset;
            if (rightBottom)
            {
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X + length, corner.Y));
                g.DrawLine(thinPen, new Point(corner.X, corner.Y), new Point(corner.X, corner.Y + length));
            }
        }
        public void DrawChessPiece(Graphics g)
        {
            for (int row = 1; row <= 10; row++)
            {
                for (int col = 1; col <= 9; col++)
                {
                    if (_chess._Piece[row, col] != Piece.无子)
                    {
                        Bitmap bmpPiece = (Bitmap)Properties.Resources.ResourceManager.GetObject(_chess._Piece[row, col].ToString());
                        g.DrawImage(bmpPiece,
                            new Point(_leftTop.X + (col - 1) * _columWidth - _pieceRadius + 2,
                                _leftTop.Y + (row - 1) * _rowHeight - _pieceRadius + 2));
                    }
                }
            }
        }
        //无效
        private void MenuItemStart_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("你是否需要开局？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            //{
            //    _listStep.Clear();
            //    _chess.spPlay(Properties.Resources.begin);
            //    _chess._CurPlayer = Player.红;
            //    _chess._PickPiece = Piece.无子;
            //    _chess._PickRow = -1;
            //    _chess._PickCol = -1;
            //    for (int row = 1; row <= 10; row++)
            //        for (int col = 1; col <= 9; col++)
            //            _chess._Piece[row, col] = Piece.无子;
            //    _chess._Piece[1, 1] = Piece.黑车;
            //    _chess._Piece[1, 2] = Piece.黑马;
            //    _chess._Piece[1, 3] = Piece.黑相;
            //    _chess._Piece[1, 4] = Piece.黑仕;
            //    _chess._Piece[1, 5] = Piece.黑帅;
            //    _chess._Piece[1, 6] = Piece.黑仕;
            //    _chess._Piece[1, 7] = Piece.黑相;
            //    _chess._Piece[1, 8] = Piece.黑马;
            //    _chess._Piece[1, 9] = Piece.黑车;
            //    _chess._Piece[3, 2] = Piece.黑炮;
            //    _chess._Piece[3, 8] = Piece.黑炮;
            //    _chess._Piece[4, 1] = Piece.黑兵;
            //    _chess._Piece[4, 3] = Piece.黑兵;
            //    _chess._Piece[4, 5] = Piece.黑兵;
            //    _chess._Piece[4, 7] = Piece.黑兵;
            //    _chess._Piece[4, 9] = Piece.黑兵;
            //    _chess._Piece[10, 1] = Piece.红车;
            //    _chess._Piece[10, 2] = Piece.红马;
            //    _chess._Piece[10, 3] = Piece.红相;
            //    _chess._Piece[10, 4] = Piece.红仕;
            //    _chess._Piece[10, 5] = Piece.红帅;
            //    _chess._Piece[10, 6] = Piece.红仕;
            //    _chess._Piece[10, 7] = Piece.红相;
            //    _chess._Piece[10, 8] = Piece.红马;
            //    _chess._Piece[10, 9] = Piece.红车;
            //    _chess._Piece[8, 2] = Piece.红炮;
            //    _chess._Piece[8, 8] = Piece.红炮;
            //    _chess._Piece[7, 1] = Piece.红兵;
            //    _chess._Piece[7, 3] = Piece.红兵;
            //    _chess._Piece[7, 5] = Piece.红兵;
            //    _chess._Piece[7, 7] = Piece.红兵;
            //    _chess._Piece[7, 9] = Piece.红兵;
            //    pictureBox1.Invalidate();
            //}
        }
        private void chessStart(Player first)
        {
            //对战不允许悔棋
            _chess.spPlay(Properties.Resources.begin);
            _chess._CurPlayer = first;
            _chess._PickPiece = Piece.无子;
            _chess._PickRow = -1;
            _chess._PickCol = -1;
            for (int row = 1; row <= 10; row++)
                for (int col = 1; col <= 9; col++)
                    _chess._Piece[row, col] = Piece.无子;
            _chess._Piece[1, 1] = Piece.黑车;
            _chess._Piece[1, 2] = Piece.黑马;
            _chess._Piece[1, 3] = Piece.黑相;
            _chess._Piece[1, 4] = Piece.黑仕;
            _chess._Piece[1, 5] = Piece.黑帅;
            _chess._Piece[1, 6] = Piece.黑仕;
            _chess._Piece[1, 7] = Piece.黑相;
            _chess._Piece[1, 8] = Piece.黑马;
            _chess._Piece[1, 9] = Piece.黑车;
            _chess._Piece[3, 2] = Piece.黑炮;
            _chess._Piece[3, 8] = Piece.黑炮;
            _chess._Piece[4, 1] = Piece.黑兵;
            _chess._Piece[4, 3] = Piece.黑兵;
            _chess._Piece[4, 5] = Piece.黑兵;
            _chess._Piece[4, 7] = Piece.黑兵;
            _chess._Piece[4, 9] = Piece.黑兵;
            _chess._Piece[10, 1] = Piece.红车;
            _chess._Piece[10, 2] = Piece.红马;
            _chess._Piece[10, 3] = Piece.红相;
            _chess._Piece[10, 4] = Piece.红仕;
            _chess._Piece[10, 5] = Piece.红帅;
            _chess._Piece[10, 6] = Piece.红仕;
            _chess._Piece[10, 7] = Piece.红相;
            _chess._Piece[10, 8] = Piece.红马;
            _chess._Piece[10, 9] = Piece.红车;
            _chess._Piece[8, 2] = Piece.红炮;
            _chess._Piece[8, 8] = Piece.红炮;
            _chess._Piece[7, 1] = Piece.红兵;
            _chess._Piece[7, 3] = Piece.红兵;
            _chess._Piece[7, 5] = Piece.红兵;
            _chess._Piece[7, 7] = Piece.红兵;
            _chess._Piece[7, 9] = Piece.红兵;
            pictureBox1.Invalidate();
        }
        public bool ConvertPointToRowCol(Point point, out int row, out int col)
        {
            int tempRow;
            tempRow = (point.Y - _leftTop.Y) / _rowHeight + 1;
            if (((point.Y - _leftTop.Y) % _rowHeight) >= _rowHeight / 2)
                tempRow++;
            int tempCol;
            tempCol = (point.X - _leftTop.X) / _columWidth + 1;
            if (((point.X - _leftTop.X) % _columWidth) >= _columWidth / 2)
                tempCol++;

            Point crossPoint = new Point();
            crossPoint.X = _leftTop.X + _columWidth * (tempCol - 1);
            crossPoint.Y = _leftTop.Y + _rowHeight * (tempRow - 1);
            double Radious = Math.Sqrt(Math.Pow(point.X - crossPoint.X, 2) + Math.Pow(point.Y - crossPoint.Y, 2));
            if ((Radious <= _pieceRadius) && (tempRow <= 10) && (tempRow >= 1) && (tempCol <= 9) && (tempCol >= 0))
            {
                row = tempRow; col = tempCol;
                return true;
            }
            else
            {
                row = -1; col = -1;
                return false;
            }
        }
        public void DrawFourCorner(Graphics g, Point center)
        {
            Pen pen = new Pen(Color.Yellow, 4);
            //lefttop
            g.DrawLine(pen, center.X - _pieceRadius, center.Y - _pieceRadius, center.X - _pieceRadius / 2, center.Y - _pieceRadius);
            g.DrawLine(pen, center.X - _pieceRadius, center.Y - _pieceRadius, center.X - _pieceRadius, center.Y - _pieceRadius / 2);
            //leftbottom
            g.DrawLine(pen, center.X - _pieceRadius, center.Y + _pieceRadius, center.X - _pieceRadius, center.Y + _pieceRadius / 2);
            g.DrawLine(pen, center.X - _pieceRadius, center.Y + _pieceRadius, center.X - _pieceRadius / 2, center.Y + _pieceRadius);
            //righttop
            g.DrawLine(pen, center.X + _pieceRadius, center.Y - _pieceRadius, center.X + _pieceRadius / 2, center.Y - _pieceRadius);
            g.DrawLine(pen, center.X + _pieceRadius, center.Y - _pieceRadius, center.X + _pieceRadius, center.Y - _pieceRadius / 2);
            //rightbottom
            g.DrawLine(pen, center.X + _pieceRadius, center.Y + _pieceRadius, center.X + _pieceRadius, center.Y + _pieceRadius / 2);
            g.DrawLine(pen, center.X + _pieceRadius, center.Y + _pieceRadius, center.X + _pieceRadius / 2, center.Y + _pieceRadius);
        }
        //防止IP地址的错误(这样会阻止粘贴)，相比之下还是写在change或者login的click里比较好
        private void textBoxServerIP_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == '.' || e.KeyChar >= '0' && e.KeyChar <= '9' || e.KeyChar == (char)Keys.Back)
            //    e.Handled = false;
            //else e.Handled = true;
        }
        //鼠标移动，棋子随着鼠标移动
        private void pictureBox1_MouseMove(object sender, MouseEventArgs e)
        {
            if (_chess._PickPiece != Piece.无子)
            {
                _mouseMovePoint.X = e.X;
                _mouseMovePoint.Y = e.Y;
                pictureBox1.Invalidate();
            }
        }
        //鼠标下压
        private void pictureBox1_MouseDown(object sender, MouseEventArgs e)
        {
            int row, col;
            bool value = ConvertPointToRowCol(new Point(e.X, e.Y), out row, out col);
            if (e.Button == MouseButtons.Right)
            {
                _chess._PickPiece = Piece.无子;
                _chess._PickRow = -1;
                _chess._PickCol = -1;
                _chess._CurOperation = Operation.拾子;
                pictureBox1.Invalidate();
                return;
            }
            if (value)
            {
                if (_chess._PickPiece == Piece.无子)
                {
                    if (_chess.PickPiece(row, col) == true)
                    {
                        Graphics g = this.CreateGraphics();
                        DrawFourCorner(g, new Point(_leftTop.X + _columWidth * (_chess._PickCol - 1), _leftTop.Y + _rowHeight * (_chess._PickRow - 1)));
                    }
                }
                else//落子
                {
                    //保存历史记录信息
                    Step chessStep = new Step();
                    chessStep._player = _chess._CurPlayer;
                    chessStep._pickPiece = _chess._PickPiece;
                    chessStep._pickRow = _chess._PickRow;
                    chessStep._pickCol = _chess._PickCol;
                    chessStep._dropPiece = _chess._Piece[row, col];
                    chessStep._dropRow = row;
                    chessStep._dropCol = col;
                    _listStep.Add(chessStep);
                    if (_chess.DropPiece(row, col) == true)//落子成功
                    {
                        //发送数据包
                        if (_socketClient != null)
                        {
                            string content;//发送的数据
                            content = "移动棋子-" + _chess._LastStep._pickPiece + _chess._LastStep._pickRow.ToString("D2")
                               + _chess._LastStep._pickCol.ToString("D2") + _chess._LastStep._dropPiece + _chess._LastStep._dropRow.ToString("D2")
                               + _chess._LastStep._dropCol.ToString("D2");
                            ClientSendPtpPacket(content, _opponentIP, _opponentPort);
                        }
                        pictureBox1.Invalidate();
                        Player winner = _chess.IsOver();
                        if (winner != Player.无)
                        {
                            _chess._CurPlayer = Player.无;
                            _opponentIP = IPAddress.Parse("0.0.0.0");
                            _opponentPort = 0;
                            _oppoentNickName = "";
                            _localState = ClientState.空闲;
                            ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                            _chess.spPlay(Properties.Resources.over);
                            //Sp.Play();
                            MessageBox.Show(this, "恭喜，你赢了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            _chess.Initialize();
                            pictureBox1.Invalidate();
                        }
                    }
                }
            }
        }
        //绘制
        private void pictureBox1_Paint(object sender, PaintEventArgs e)
        {
            DrawChessBoard(e.Graphics);
            DrawChessPiece(e.Graphics);
            if (_chess._PickPiece != Piece.无子)
            {
                DrawFourCorner(e.Graphics, new Point(_leftTop.X + _columWidth * (_chess._PickCol - 1),
                    _leftTop.Y + _rowHeight * (_chess._PickRow - 1)));
                Bitmap bmpPiece = (Bitmap)Properties.Resources.ResourceManager.GetObject(_chess._PickPiece.ToString());
                e.Graphics.DrawImage(bmpPiece, new Point(_mouseMovePoint.X - _pieceRadius, _mouseMovePoint.Y - _pieceRadius));
            }
        }
        //接包
        private void ClientReceivePacket()
        {
            while (true)
            {
                try
                {
                    if (_socketClient.Connected == true)
                    {
                        Byte[] bytePacket = new Byte[4000];
                        int length = _socketClient.Receive(bytePacket);
                        String receivePacket = System.Text.Encoding.UTF8.GetString(bytePacket, 0, length);
                        //拆包
                        IPAddress fromIp, toIp;
                        int fromPort, toPort;
                        string content = "";
                        bool result = DecodePacket(receivePacket, out fromIp, out fromPort, out toIp, out toPort, out content);
                        if (result == true)
                        {
                            if (content.IndexOf("新人报到-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                //ListViewItem item = new ListViewItem(nickName);
                                //item.SubItems.Add(fromIp.ToString());
                                //item.SubItems.Add(fromPort.ToString());
                                //listViewClient.Items.Add(item);
                                AddItemToListViewClient(nickName, fromIp, fromPort, ClientState.空闲);
                                ClientSendPtpPacket("回复新人-" + _localNickName, fromIp, fromPort);
                            }
                            else if (content.IndexOf("回复新人-") == 0)
                            {
                                //MessageBox.Show("huifu");
                                string nickName = content.Remove(0, 5);
                                ClientState clientState = ClientState.空闲;
                                int pos1 = content.IndexOf("-");
                                int pos2 = content.IndexOf("-", pos1 + 1);
                                if (pos2 != -1)
                                {
                                    nickName = content.Substring(pos1 + 1, pos2 - pos1 - 1);
                                    clientState = (ClientState)Enum.Parse(typeof(ClientState), content.Substring(pos2 + 1));
                                }
                                //ListViewItem item = new ListViewItem(nickName);
                                //item.SubItems.Add(fromIp.ToString());
                                //item.SubItems.Add(fromPort.ToString());
                                //listViewClient.Items.Add(item);
                                AddItemToListViewClient(nickName, fromIp, fromPort, clientState);
                            }
                            else if (content.IndexOf("我开溜了-") == 0)
                            {
                                RemoveItemFromListViewClient(fromIp, fromPort);
                            }
                            else if (content.IndexOf("下挑战书-") == 0)
                            {
                                //MessageBox.Show(_localState.ToString());
                                if (_localState == ClientState.空闲)
                                {

                                    string nickName = content.Remove(0, 5);
                                    _localState = ClientState.接挑战书;
                                    ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                    Thread threadAcceptChallenge = new Thread(AcceptChallenge);
                                    threadAcceptChallenge.IsBackground = true;
                                    ClientParam clientParam = new ClientParam();
                                    clientParam._ip = fromIp;
                                    clientParam._port = fromPort;
                                    clientParam._nickName = nickName;
                                    threadAcceptChallenge.Start(clientParam);
                                }
                                else
                                {
                                    ClientSendPtpPacket("我正在忙-" + _localNickName, fromIp, fromPort);
                                }
                            }
                            else if (content.IndexOf("接挑战书-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                _opponentIP = fromIp;
                                _opponentPort = fromPort;
                                _oppoentNickName = nickName;
                                _localState = ClientState.对弈;
                                ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                if (dlgSendChallenge != null)
                                {
                                    dlgSendChallenge.DialogResult = DialogResult.Ignore;
                                    dlgSendChallenge.Close();
                                }
                                ClientSendPtpPacket("你先走棋-" + _localNickName, fromIp, fromPort);
                                chessStart(Player.黑);
                            }
                            else if (content.IndexOf("你先走棋-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                _opponentIP = fromIp;
                                _opponentPort = fromPort;
                                _oppoentNickName = nickName;
                                _localState = ClientState.对弈;
                                ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                chessStart(Player.红);
                            }
                            else if (content.IndexOf("放弃挑战-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                if (dlgAcceptChallenge != null)
                                {
                                    dlgAcceptChallenge.DialogResult = DialogResult.Ignore;
                                    dlgAcceptChallenge.Close();
                                }
                                _opponentIP = IPAddress.Parse("0.0.0.0");
                                _opponentPort = 0;
                                _oppoentNickName = "";
                                _localState = ClientState.空闲;
                                ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                MessageBox.Show(this, "[" + nickName + "] 放弃挑战了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                _chess.Initialize();
                                pictureBox1.Invalidate();
                            }
                            else if (content.IndexOf("挂免战牌-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                _localState = ClientState.空闲;
                                ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                if (dlgSendChallenge != null)
                                {
                                    dlgSendChallenge.DialogResult = DialogResult.Ignore;
                                    dlgSendChallenge.Close();
                                    MessageBox.Show(this, "[" + nickName + "] 高挂免战牌，不敢迎战。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }
                            }
                            else if (content.IndexOf("我正在忙-") == 0)
                            {
                                string nickName = content.Remove(0, 5);
                                _localState = ClientState.空闲;
                                ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                if (dlgSendChallenge != null)
                                {
                                    dlgSendChallenge.DialogResult = DialogResult.Ignore;
                                    dlgSendChallenge.Close();
                                }
                                MessageBox.Show(this, "[" + nickName + "] 正在忙，没空迎战。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            }
                            else if (content.IndexOf("移动棋子-") == 0)
                            {
                                string pickPiece = content.Substring(5, 2);
                                if (pickPiece.Substring(0, 1) == "红")
                                    pickPiece = "黑" + pickPiece.Substring(1, 1);
                                else if (pickPiece.Substring(0, 1) == "黑")
                                    pickPiece = "红" + pickPiece.Substring(1, 1);
                                int pickRow = 11 - Convert.ToInt32(content.Substring(7, 2));
                                int pickCol = 10 - Convert.ToInt32(content.Substring(15, 2));

                                string dropPiece = content.Substring(11, 2);
                                if (dropPiece.Substring(0, 1) == "黑")
                                    dropPiece = "红" + dropPiece.Substring(1, 1);
                                else if (dropPiece.Substring(0, 1) == "红")
                                    dropPiece = "黑" + dropPiece.Substring(1, 1);
                                int dropRow = 11 - Convert.ToInt32(content.Substring(13, 2));
                                int dropCol = 10 - Convert.ToInt32(content.Substring(15, 2));

                                _chess._Piece[pickRow, pickCol] = Piece.无子;
                                _chess._Piece[dropRow, dropCol] = (Piece)Enum.Parse(typeof(Piece), pickPiece);
                                if (dropPiece.IndexOf("帅") != -1)
                                {
                                    _chess._CurPlayer = Player.无;
                                    _opponentIP = IPAddress.Parse("0.0.0.0");
                                    _opponentPort = 0;
                                    _oppoentNickName = "";
                                    _localState = ClientState.空闲;
                                    ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                                    _chess.spPlay(Properties.Resources.over);
                                    //Sp.Play();
                                    MessageBox.Show(this, "不好意思，你输了！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    _chess.Initialize();
                                    pictureBox1.Invalidate();
                                }
                                else
                                {
                                    if (dropPiece == "无子")
                                    {
                                        _chess.spPlay(Properties.Resources.go);
                                        //Sp.Play();
                                    }
                                    else
                                    {
                                        _chess.spPlay(Properties.Resources.eat);
                                        //Sp.Play();
                                    }
                                    if (_chess._CurPlayer == Player.红)
                                        _chess._CurPlayer = Player.黑;
                                    else
                                        _chess._CurPlayer = Player.红;
                                }
                                pictureBox1.Invalidate();
                                //_chess._piece[]
                            }
                            else if (content.IndexOf("大厅关闭-") == 0)
                            {
                                listViewClient.Items.Clear();
                                _localIP = IPAddress.Parse("0.0.0.0");
                                _localPort = 0;
                                _localNickName = "";
                                _localState = ClientState.离线;
                                _serverIP = IPAddress.Parse("0.0.0.0");
                                _serverPort = 0;
                                textboxServerIp.Enabled = true;
                                numricServerPort.Enabled = true;
                                textBoxNickName.Enabled = true;
                                buttonLogin.Enabled = true;
                                buttonQuit.Enabled = false;
                                buttonchallenge.Enabled = false;
                                buttonsurrender.Enabled = false;
                                MessageBox.Show(this, "游戏大厅被管理员关闭了，下次再玩吧！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            }
                        }
                    }
                    else return;
                }
                catch (Exception excep)
                {
                    //MessageBox.Show(this, excep.Message, "异常-Receive", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //连接服务器
        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                _socketClient = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                IPAddress serverIp = IPAddress.Parse(textboxServerIp.Text);
                int serverPort = Convert.ToInt32(numricServerPort.Value);
                IPEndPoint ipEndPoint = new IPEndPoint(serverIp, serverPort);
                _socketClient.Connect(ipEndPoint);

                threadReceivePacket = new Thread(new ThreadStart(ClientReceivePacket));
                threadReceivePacket.IsBackground = true;
                threadReceivePacket.Start();
                _localIP = ((IPEndPoint)_socketClient.LocalEndPoint).Address;
                _localPort = ((IPEndPoint)_socketClient.LocalEndPoint).Port;
                _localNickName = textBoxNickName.Text;
                _serverIP = ((IPEndPoint)_socketClient.RemoteEndPoint).Address;
                _serverPort = ((IPEndPoint)_socketClient.RemoteEndPoint).Port;
                _localState = ClientState.空闲;
                textboxServerIp.Enabled = false;
                numricServerPort.Enabled = false;
                textBoxNickName.Enabled = false;
                buttonLogin.Enabled = false;
                buttonQuit.Enabled = true;
                buttonsurrender.Enabled = true;
                buttonchallenge.Enabled = true;
                //MessageBox.Show(this, "连接服务器成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                //发送广播
                ClientSendBroadcastPacket("新人报到-" + _localNickName);
            }
            catch (Exception excep)
            {
                MessageBox.Show(this, excep.Message, "异常-Login", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //发送点对点消息
        private void ClientSendPtpPacket(string content, IPAddress toIp, int toPort)
        {
            try
            {
                if (_socketClient != null && _socketClient.Connected == true)
                {
                    string sendPacket = _localIP.ToString() + "-" + _localPort.ToString() + "-" + toIp.ToString() + "-" + toPort.ToString() + "-" + content;
                    Byte[] bytePacket = System.Text.Encoding.UTF8.GetBytes(sendPacket);
                    _socketClient.Send(bytePacket);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(this, excep.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //发送广播
        public void ClientSendBroadcastPacket(string content)
        {
            try
            {
                if (_socketClient != null && _socketClient.Connected == true)
                {
                    string sendPacket = _localIP.ToString() + "-"
                        + _localPort.ToString() +
                        "-" + IPAddress.Parse("255.255.255.255").ToString() + "-" + "0" + "-" + content;
                    Byte[] bytePacket = System.Text.Encoding.UTF8.GetBytes(sendPacket);
                    _socketClient.Send(bytePacket);
                    //MessageBox.Show(sendPacket);
                }
            }
            catch (Exception excep)
            {
                MessageBox.Show(this, excep.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        //解包
        public bool DecodePacket(string packet, out IPAddress fromIp, out int fromPort, out IPAddress toIp, out int toPort, out string content)
        {
            int count = 0;
            int[] pos = new int[4];
            for (int i = 0; i <= packet.Length - 1; i++)
                if (packet[i] == '-')
                {
                    pos[count] = i;
                    count++;
                    if (count >= 4)
                        break;
                }
            if (count == 4)
            {
                try
                {
                    fromIp = IPAddress.Parse(packet.Substring(0, pos[0] - 0));
                    fromPort = Convert.ToInt32(packet.Substring(pos[0] + 1, pos[1] - pos[0] - 1));
                    toIp = IPAddress.Parse(packet.Substring(pos[1] + 1, pos[2] - pos[1] - 1));
                    toPort = Convert.ToInt32(packet.Substring(pos[2] + 1, pos[3] - pos[2] - 1));
                    content = packet.Remove(0, pos[3] + 1);
                    return true;
                }
                catch (Exception excep)
                {
                    fromIp = IPAddress.Parse("0.0.0.0");
                    fromPort = 0;
                    toIp = IPAddress.Parse("0.0.0.0");
                    toPort = 0;
                    content = "";
                    return false;
                }
            }
            else
            {
                fromIp = IPAddress.Parse("0.0.0.0");
                fromPort = 0;
                toIp = IPAddress.Parse("0.0.0.0");
                toPort = 0;
                content = "";
                return false;
            }
        }
        //退出大厅
        private void buttonQuit_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(this, "需要退出大厅吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    ClientSendBroadcastPacket("我开溜了-" + _localNickName);
                    _localIP = IPAddress.Parse("0.0.0.0");
                    _localPort = 0;
                    _localNickName = "";
                    _localState = ClientState.离线;
                    _serverIP = IPAddress.Parse("0.0.0.0");
                    _serverPort = 0;

                    textboxServerIp.Enabled = true;
                    numricServerPort.Enabled = true;
                    textBoxNickName.Enabled = true;
                    buttonLogin.Enabled = true;
                    buttonQuit.Enabled = false;
                    buttonsurrender.Enabled = false;
                    buttonchallenge.Enabled = false;
                    listViewClient.Items.Clear();
                    //象棋初始化
                    _chess.Initialize();
                    pictureBox1.Invalidate();
                    if (_socketClient != null)
                        _socketClient.Close();
                    if (threadReceivePacket != null)
                        threadReceivePacket.Abort();
                }
                catch (Exception excep)
                {
                    MessageBox.Show(this, excep.Message, "异常", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //移除元素
        public void RemoveItemFromListViewClient(IPAddress ip, int port)
        {
            for (int i = 0; i <= listViewClient.Items.Count - 1; i++)
            {
                if (listViewClient.Items[i].SubItems[1].Text == ip.ToString()
                    && listViewClient.Items[i].SubItems[2].Text == port.ToString())
                {
                    listViewClient.Items.RemoveAt(i);
                    return;
                }
            }
        }
        //添加元素
        public void AddItemToListViewClient(string nickName, IPAddress ip, int port, ClientState state)
        {
            for (int i = 0; i <= listViewClient.Items.Count - 1; i++)
            {
                if (listViewClient.Items[i].SubItems[1].Text == ip.ToString()
                    && listViewClient.Items[i].SubItems[2].Text == port.ToString())
                {
                    listViewClient.Items[i].Text = nickName;
                    listViewClient.Items[i].SubItems[3].Text = state.ToString();
                    return;
                }
            }
            ListViewItem item = new ListViewItem(nickName);
            item.SubItems.Add(ip.ToString());
            item.SubItems.Add(port.ToString());
            item.SubItems.Add(state.ToString());
            listViewClient.Items.Add(item);
        }
        //关闭提醒
        private void FormMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show(this, "需要关闭程序吗？", "请确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                e.Cancel = true;
            else
            {
                try
                {
                    ClientSendBroadcastPacket("我开溜了-" + _localNickName);
                    if (_socketClient != null && _socketClient.Connected == true)
                        _socketClient.Close();
                    if (threadReceivePacket != null)
                        threadReceivePacket.Abort();
                }
                catch (Exception excep)
                {
                    //MessageBox.Show(this, excep.Message, "异常-Close", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        //下挑战书
        private void buttonchallenge_Click(object sender, EventArgs e)
        {
            if (_localState == ClientState.空闲)
            {
                if (listViewClient.SelectedItems.Count == 0)
                {
                    MessageBox.Show(this, "请现在用户列表中选择你想挑战的对手！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                IPAddress oppoentIp = IPAddress.Parse(listViewClient.SelectedItems[0].SubItems[1].Text);
                int opponentPort = Convert.ToInt32(listViewClient.SelectedItems[0].SubItems[2].Text);
                string opponentNickName = listViewClient.SelectedItems[0].Text;
                try
                {
                    ClientSendPtpPacket("下挑战书-" + _localNickName, oppoentIp, opponentPort);
                    _localState = ClientState.下挑战书;
                    ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                    dlgSendChallenge = new DlgSendChallenge();
                    dlgSendChallenge._infor = "你已经向 [" + opponentNickName + "] 发出了挑战书，\n请耐心等待对方回应。";
                    if (dlgSendChallenge.ShowDialog(this) == DialogResult.Cancel)
                    {
                        dlgSendChallenge = null;
                        ClientSendPtpPacket("放弃挑战-" + _localNickName.ToString(), oppoentIp, opponentPort);
                        _localState = ClientState.空闲;
                        UpdateItemOfListViewClient(_localIP, _localPort, _localState);
                        ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
                    }
                }
                catch (Exception excp)
                {
                    //MessageBox.Show(this, excep.Message, "异常-Close", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
                MessageBox.Show(this, "你现在忙，不能下挑战书！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        //更新listViewClient的一行客户端的状态
        public void UpdateItemOfListViewClient(IPAddress ip, int port, ClientState newState)
        {
            //对listViewClient进行遍历
            for (int i = 0; i <= listViewClient.Items.Count - 1; i++)
            {
                //如果是待更新的客户端行
                if (listViewClient.Items[i].SubItems[1].Text == ip.ToString()
                    && listViewClient.Items[i].SubItems[2].Text == port.ToString())
                {
                    listViewClient.Items[i].SubItems[3].Text = newState.ToString();
                    return;
                }
            }
        }
        //接受挑战
        private void AcceptChallenge(object objClientParam)
        {
            ClientParam clientParam = (ClientParam)objClientParam;
            dlgAcceptChallenge = new DlgAcceptChallenge();
            dlgAcceptChallenge._infor = "[" + clientParam._nickName + "] 向你发出了挑战，你接受他的挑战吗？";
            DialogResult result = dlgAcceptChallenge.ShowDialog(this);
            if (result == DialogResult.Yes)
            {
                ClientSendPtpPacket("接挑战书-" + _localNickName, clientParam._ip, clientParam._port);
            }
            else if (result == DialogResult.No || result == DialogResult.Cancel)
            {
                ClientSendPtpPacket("挂免战牌-" + _localNickName, clientParam._ip, clientParam._port);
                _localState = ClientState.空闲;
                ClientSendBroadcastPacket("状态通报-" + _localState.ToString());
            }
        }
        private static string GenerateSurname()
        {
            string name = string.Empty;
            string[] currentConsonant;
            string[] vowels = "a,a,a,a,a,e,e,e,e,e,e,e,e,e,e,e,i,i,i,o,o,o,u,y,ee,ee,ea,ea,ey,eau,eigh,oa,oo,ou,ough,ay".Split(',');
            string[] commonConsonants = "s,s,s,s,t,t,t,t,t,n,n,r,l,d,sm,sl,sh,sh,th,th,th".Split(',');
            string[] averageConsonants = "sh,sh,st,st,b,c,f,g,h,k,l,m,p,p,ph,wh".Split(',');
            string[] middleConsonants = "x,ss,ss,ch,ch,ck,ck,dd,kn,rt,gh,mm,nd,nd,nn,pp,ps,tt,ff,rr,rk,mp,ll".Split(','); //Can't start
            string[] rareConsonants = "j,j,j,v,v,w,w,w,z,qu,qu".Split(',');
            Random rng = new Random(Guid.NewGuid().GetHashCode()); //http://codebetter.com/blogs/59496.aspx
            int[] lengthArray = new int[] { 2, 2, 2, 2, 2, 2, 3, 3, 3, 4 }; //Favor shorter names but allow longer ones
            int length = lengthArray[rng.Next(lengthArray.Length)];
            for (int i = 0; i < length; i++)
            {
                int letterType = rng.Next(1000);
                if (letterType < 775) currentConsonant = commonConsonants;
                else if (letterType < 875 && i > 0) currentConsonant = middleConsonants;
                else if (letterType < 985) currentConsonant = averageConsonants;
                else currentConsonant = rareConsonants;
                name += currentConsonant[rng.Next(currentConsonant.Length)];
                name += vowels[rng.Next(vowels.Length)];
                if (name.Length > 4 && rng.Next(1000) < 800) break; //Getting long, must roll to save
                if (name.Length > 6 && rng.Next(1000) < 950) break; //Really long, roll again to save
                if (name.Length > 7) break; //Probably ridiculous, stop building and add ending
            }
            int endingType = rng.Next(1000);
            if (name.Length > 6)
                endingType -= (name.Length * 25); //Don't add long endings if already long
            else
                endingType += (name.Length * 10); //Favor long endings if short
            if (endingType < 400) { } // Ends with vowel
            else if (endingType < 775) name += commonConsonants[rng.Next(commonConsonants.Length)];
            else if (endingType < 825) name += averageConsonants[rng.Next(averageConsonants.Length)];
            else if (endingType < 840) name += "ski";
            else if (endingType < 860) name += "son";
            else if (Regex.IsMatch(name, "(.+)(ay|e|ee|ea|oo)$") || name.Length < 5)
            {
                name = "Mc" + name.Substring(0, 1).ToUpper() + name.Substring(1);
                return name;
            }
            else name += "ez";
            name = name.Substring(0, 1).ToUpper() + name.Substring(1); //Capitalize first letter
            return name;
        }
        //启动
        private void FormMain_Load(object sender, EventArgs e)
        {
            textBoxNickName.Text = GenerateSurname();
            //MessageBox.Show(this.webBrowser1.Version.ToString());
            RegistryKey key = Registry.LocalMachine;
            RegistryKey software = key.OpenSubKey("SOFTWARE\\Microsoft\\Internet Explorer\\Main\\FeatureControl\\FEATURE_BROWSER_EMULATION", true);
            software.SetValue("Chess", "10000",RegistryValueKind.DWord);
            this.webBrowser1.Document.Body.Style = "zoom:3.0";
        }
        //悔棋
        private void MenuItemUndo_Click(object sender, EventArgs e)
        {
            //MessageBox.Show(_listStep.Count.ToString());
            if (_listStep.Count >= 1)
            {
                if (MessageBox.Show("你是否需要悔棋？", "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Step chessStep = _listStep[_listStep.Count - 1];
                    _chess._CurPlayer = chessStep._player;
                    _chess._Piece[chessStep._pickRow, chessStep._pickCol] = chessStep._pickPiece;
                    _chess._Piece[chessStep._dropRow, chessStep._dropCol] = chessStep._dropPiece;
                    _listStep.RemoveAt(_listStep.Count - 1);
                    _chess._PickPiece = Piece.无子;
                    _chess._PickRow = -1;
                    _chess._PickCol = -1;
                    pictureBox1.Invalidate();
                }
            }
        }
        //保存
        private void MenuItemSave_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream fileStream = new FileStream(saveFileDialog1.FileName, FileMode.Create);
                BinaryWriter binaryWriter = new BinaryWriter(fileStream);
                binaryWriter.Write(_chess._CurPlayer.ToString());
                for (int row = 1; row <= 10; row++)
                    for (int col = 1; col <= 9; col++)
                        binaryWriter.Write(_chess._Piece[row, col].ToString());
                binaryWriter.Write(_listStep.Count);
                for (int i = 0; i <= _listStep.Count - 1; i++)
                {
                    binaryWriter.Write(_listStep[i]._player.ToString());
                    binaryWriter.Write(_listStep[i]._pickPiece.ToString());
                    binaryWriter.Write(_listStep[i]._pickRow);
                    binaryWriter.Write(_listStep[i]._pickCol);
                    binaryWriter.Write(_listStep[i]._dropPiece.ToString());
                    binaryWriter.Write(_listStep[i]._dropRow);
                    binaryWriter.Write(_listStep[i]._dropCol);
                }
                binaryWriter.Close();
                fileStream.Close();
            }
        }
        //打开
        private void MenuItemOpen_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                FileStream filestream = new FileStream(openFileDialog1.FileName, FileMode.Open, FileAccess.Read);
                BinaryReader binaryReader = new BinaryReader(filestream);
                _chess._CurPlayer = (Player)Enum.Parse(typeof(Player), binaryReader.ReadString());
                for (int row = 1; row <= 10; row++)
                    for (int col = 1; col <= 9; col++)
                        _chess._Piece[row, col] = (Piece)Enum.Parse(typeof(Piece), binaryReader.ReadString());
                int count = binaryReader.ReadInt32();
                _listStep.Clear();
                for (int i = 0; i <= count - 1; i++)
                {
                    Step chessStep = new Step();
                    chessStep._player = (Player)Enum.Parse(typeof(Player), binaryReader.ReadString());
                    chessStep._pickPiece = (Piece)Enum.Parse(typeof(Piece), binaryReader.ReadString());
                    chessStep._pickRow = binaryReader.ReadInt32();
                    chessStep._pickCol = binaryReader.ReadInt32();
                    chessStep._dropPiece = (Piece)Enum.Parse(typeof(Piece), binaryReader.ReadString());
                    chessStep._dropRow = binaryReader.ReadInt32();
                    chessStep._dropCol = binaryReader.ReadInt32();
                    _listStep.Add(chessStep);
                }
                binaryReader.Close();
                filestream.Close();
                pictureBox1.Invalidate();
            }
        }

        private void buttonRandomName_Click(object sender, EventArgs e)
        {
            textBoxNickName.Text = GenerateSurname();
        }

        private void MenuItemvsc_Click(object sender, EventArgs e)
        {
            if (_ai == true)
            {
                //webBrowser1.Url = "";
                webBrowser1.Visible = false;
                buttonLogin.Enabled = true;
                MenuItemSave.Enabled = true;
                _ai = false;
            }
            else
            {
                if (buttonQuit.Enabled == false)
                {
                    //webBrowser1.Url = (Uri)
                    webBrowser1.Visible = true;
                    buttonLogin.Enabled = false;
                    MenuItemSave.Enabled = false;
                    _ai = true;
                }
                else
                    MessageBox.Show(this, "请先断开服务器连接！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}