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

namespace Chess
{
    public enum ClientState
    {
        离线 = 1, 空闲 = 2, 下挑战书 = 3, 接挑战书 = 4, 对弈 = 5
    }
    public enum Piece
    {
        无子 = 0,
        黑车 = 1, 黑马 = 2, 黑相 = 3, 黑仕 = 4, 黑帅 = 5, 黑炮 = 6, 黑兵 = 7,
        红车 = 8, 红马 = 9, 红相 = 10, 红仕 = 11, 红帅 = 12, 红炮 = 13, 红兵 = 14
    }
    public enum Player
    {
        无, 红, 黑
    }
    public enum Operation
    {
        拾子, 落子
    }
    public class Step
    {
        public Player _player;
        public Piece _pickPiece;
        public int _pickRow;
        public int _pickCol;
        public Piece _dropPiece;
        public int _dropRow;
        public int _dropCol;
    }
    class Chess
    {
        //当前玩家
        private Player _curPlayer = Player.无;
        public Player _CurPlayer
        {
            get { return _curPlayer; }
            set { _curPlayer = value; }
        }
        //当前操作
        private Operation _curOperation = Operation.拾子;
        public Operation _CurOperation
        {
            get { return _curOperation; }
            set { _curOperation = value; }
        }
        //保存棋子的数组
        private Piece[,] _piece = new Piece[11, 10];
        public Piece[,] _Piece
        {
            get { return _piece; }
            set { _piece = value; }
        }
        //所拾子
        private Piece _pickPiece = Piece.无子;
        public Piece _PickPiece
        {
            get { return _pickPiece; }
            set { _pickPiece = value; }
        }
        //拾子行
        private int _pickRow = -1;
        public int _PickRow
        {
            get { return _pickRow; }
            set { _pickRow = value; }
        }
        //拾子列
        private int _pickCol = -1;
        public int _PickCol
        {
            get { return _pickCol; }
            set { _pickCol = value; }
        }
        //所落子
        private Piece _dropPiece = Piece.无子;
        public Piece _DropPiece
        {
            get { return _dropPiece; }
            set { _dropPiece = value; }
        }
        private int _dropRow = -1;
        public int _DropRow
        {
            get { return _dropRow; }
            set { _dropRow = value; }
        }
        private int _dropCol = -1;
        public int _DropCol
        {
            get { return _dropCol; }
            set { _dropCol = value; }
        }
        private Step _lastStep=new Step();
        public Step _LastStep
        {
            get { return _lastStep; }
            set { _lastStep = value; }
        }
        //音乐播放器
        SoundPlayer sp;
        //拾子
        public bool PickPiece(int pickRow, int pickCol)
        {
            //走棋方为“红”，且操作为“拾子”，且是己方棋子
            if (_curPlayer == Player.红 && _curOperation == Operation.拾子
                && _piece[pickRow, pickCol].ToString().Substring(0, 1) == _curPlayer.ToString())
            {
                //保存棋子值、行号、列号
                _pickPiece = _piece[pickRow, pickCol];
                _pickRow = pickRow;
                _pickCol = pickCol;
                //保存上一步
                _lastStep._pickPiece = _pickPiece;
                _lastStep._pickRow = _pickRow;
                _lastStep._pickCol = _pickCol;
                //切换操作
                _curOperation = Operation.落子;
                return true;
            }
            else
                return false;
        }
        //落子
        public bool DropPiece(int dropRow, int dropCol)
        {
            if (_curPlayer == Player.红 && _curOperation == Operation.落子
                && MatchRules(_curPlayer, _pickRow, _pickCol, dropRow, dropCol) == true)
            {
                
                _dropPiece = _piece[dropRow, dropCol];
                _dropRow = dropRow;
                _dropCol = dropCol;
                _piece[_pickRow, _pickCol] = Piece.无子;
                _piece[_dropRow, _dropCol] = _pickPiece;

                _lastStep._dropPiece = _dropPiece;
                _lastStep._dropRow = _dropRow;
                _lastStep._dropCol = _dropCol;

                _pickPiece = Piece.无子;
                _pickRow = -1;
                _pickCol = -1;
                _curOperation = Operation.拾子;
                //播放音效
                //MessageBox.Show(dropRow.ToString() + " " + dropCol.ToString());
                if (_piece[dropRow, dropCol] == Piece.无子)
                    spPlay(Properties.Resources.go);
                else
                    spPlay(Properties.Resources.eat);
                //交换玩家
                if (_CurPlayer == Player.红) 
                    _CurPlayer = Player.黑;
                else 
                    _CurPlayer = Player.红;
                return true;
            }
            else
                return false;
        }
        //走棋规则
        private Boolean Xiang(int row, int col)//象走田，不越界
        {
            if (_pickPiece == Piece.黑相)
            {
                if (Math.Abs(_pickRow - row) == 2 && Math.Abs(_pickCol - col) == 2 && _piece[(_pickRow + row) / 2, (_pickCol + col) / 2] == Piece.无子 && row <= 5)
                    return true;
            }
            if (_pickPiece == Piece.红相)
            {
                if (Math.Abs(_pickRow - row) == 2 && Math.Abs(_pickCol - col) == 2 && _piece[(_pickRow + row) / 2, (_pickCol + col) / 2] == Piece.无子 && row >= 6)
                    return true;
            }
            return false;
        }
        private Boolean Bing(int row, int col)//兵不后退，越界后可以前左右走一步
        {
            if (_pickPiece == Piece.黑兵)
            {
                if (_pickRow <= 5)//未过界
                {
                    if (row == _pickRow + 1 && col == _pickCol)
                        return true;
                }
                if (_pickRow >= 6)
                {
                    if (row == _pickRow && Math.Abs(col - _pickCol) == 1)
                        return true;
                    if (col == _pickCol && (row - _pickRow) == 1)
                        return true;
                }
            }
            if (_pickPiece == Piece.红兵)
            {
                if (_pickRow >= 6)//未过界
                {
                    if (row == _pickRow - 1 && col == _pickCol)
                        return true;
                }
                if (_pickRow <= 5)
                {
                    if (row == _pickRow && Math.Abs(col - _pickCol) == 1)
                        return true;
                    if (col == _pickCol && (row - _pickRow) == -1)
                        return true;
                }
            }
            return false;
        }
        private Boolean ju(int row, int col)//车横竖走，不跨子
        {
            if (_pickPiece == Piece.黑车 || _pickPiece == Piece.红车)
            {
                int max, min;
                bool blankFlag = false;
                if (_pickRow == row)//横着走
                {
                    max = col > _pickCol ? col : _pickCol;
                    min = col > _pickCol ? _pickCol : col;
                    blankFlag = true;
                    for (int i = min + 1; i <= max - 1; i++)
                        if (_piece[row, i] != Piece.无子)
                            blankFlag = false;
                    if (blankFlag) return true;
                }
                blankFlag = false;
                if (_pickCol == col)//竖着走
                {
                    max = row > _pickRow ? row : _pickRow;
                    min = row > _pickRow ? _pickRow : row;
                    blankFlag = true;
                    for (int i = min + 1; i <= max - 1; i++)
                        if (_piece[i, col] != Piece.无子)
                            blankFlag = false;
                    if (blankFlag) return true;
                }
            }
            return false;
        }
        private Boolean ma(int row, int col)//马走日
        {
            if (_pickPiece == Piece.红马 || _pickPiece == Piece.黑马)
            {
                if (_pickCol != col && _pickRow != row && Math.Abs(_pickCol - col) + Math.Abs(_pickRow - row) == 3)//位移和为3
                {
                    if (_piece[_pickRow - (_pickRow - row) / 2, _pickCol - (_pickCol - col) / 2] == Piece.无子)
                        return true;
                }
            }
            return false;
        }
        private Boolean pao(int row, int col)//炮隔山
        {
            if (_pickPiece.ToString().IndexOf("炮") != -1)
            {
                if (_pickRow == row)//这两个是炮吃子
                {
                    int cnt = 0;
                    for (int i = _pickCol < col ? _pickCol + 1 : col + 1; i <= (_pickCol < col ? col - 1 : _pickCol - 1); i++)
                    {
                        if (_piece[row, i] != Piece.无子) cnt++;
                    }
                    if (cnt == 1 && _piece[row, col] != Piece.无子 && _piece[row, col].ToString().IndexOf(_curPlayer.ToString()) == -1) return true;
                }
                if (_pickCol == col)
                {
                    int cnt = 0;
                    for (int i = _pickRow < row ? _pickRow + 1 : row + 1; i <= (_pickRow < row ? row - 1 : _pickRow - 1); i++)
                    {
                        if (_piece[i, col] != Piece.无子) cnt++;
                    }
                    if (cnt == 1 && _piece[row, col] != Piece.无子 && _piece[row, col].ToString().IndexOf(_curPlayer.ToString()) == -1) return true;
                }
                //这两个是炮移动,同车
                int max, min;
                bool blankFlag = false;
                if (_pickRow == row)//横着走
                {
                    max = col > _pickCol ? col : _pickCol;
                    min = col > _pickCol ? _pickCol : col;
                    blankFlag = true;
                    for (int i = min + 1; i <= max - 1; i++)
                        if (_piece[row, i] != Piece.无子)
                            blankFlag = false;
                    if (blankFlag && _piece[row, col] == Piece.无子) return true;
                }
                blankFlag = false;
                if (_pickCol == col)//竖着走
                {
                    max = row > _pickRow ? row : _pickRow;
                    min = row > _pickRow ? _pickRow : row;
                    blankFlag = true;
                    for (int i = min + 1; i <= max - 1; i++)
                        if (_piece[i, col] != Piece.无子)
                            blankFlag = false;
                    if (blankFlag && _piece[row, col] == Piece.无子) return true;
                }
            }
            return false;
        }
        private Boolean shi(int row, int col)//士走斜，不出九宫
        {
            if (_pickPiece == Piece.黑仕)
            {
                if (Math.Abs(col - _pickCol) == Math.Abs(row - _pickRow) && Math.Abs(col - _pickCol) == 1)
                    if (col >= 4 && col <= 6 && row >= 1 && row <= 3) return true;
            }
            if (_pickPiece == Piece.红仕)
            {
                if (Math.Abs(col - _pickCol) == Math.Abs(row - _pickRow) && Math.Abs(col - _pickCol) == 1)
                    if (col >= 4 && col <= 6 && row >= 8 && row <= 10) return true;
            }
            return false;
        }
        private Boolean jiang(int row, int col)//将,不出九宫,无线不可走//(5,1)(5,3)等不能斜着走
        {
            if (_pickPiece == Piece.黑帅)
            {
                int cnt = 0;
                for (int i = _pickRow + 1; i <= 10; i++)
                {
                    if (_piece[i, _pickCol] != Piece.无子) cnt++;
                    if (_piece[i, _pickCol] == Piece.红帅 && cnt == 1)
                        if (col == _pickCol && i == row) return true;
                }
                if (Math.Abs(col - _pickCol) == Math.Abs(row - _pickRow) && Math.Abs(col - _pickCol) == 1)//斜着走
                    if (!((_pickRow == 1 || _pickRow == 3) && _pickCol == 5))
                        if (!((_pickCol == 4 || _pickCol == 6) && _pickRow == 2))
                            if (col >= 4 && col <= 6 && row >= 1 && row <= 3) return true;
                if (Math.Abs(col - _pickCol) + Math.Abs(row - _pickRow) == 1)//横竖走
                    if (col >= 4 && col <= 6 && row >= 1 && row <= 3) return true;
            }
            if (_pickPiece == Piece.红帅)
            {
                int cnt = 0;
                for (int i = _pickRow - 1; i >= 1; i--)
                {
                    if (_piece[i, _pickCol] != Piece.无子) cnt++;
                    if (_piece[i, _pickCol] == Piece.黑帅 && cnt == 1)
                        if (col == _pickCol && i == row) return true;
                }
                if (Math.Abs(col - _pickCol) == Math.Abs(row - _pickRow) && Math.Abs(col - _pickCol) == 1)
                    if (!((_pickRow == 8 || _pickRow == 10) && _pickCol == 5))
                        if (!((_pickCol == 4 || _pickCol == 6) && _pickRow == 9))
                            if (col >= 4 && col <= 6 && row >= 8 && row <= 10) return true;
                if (Math.Abs(col - _pickCol) + Math.Abs(row - _pickRow) == 1)
                    if (col >= 4 && col <= 6 && row >= 8 && row <= 10) return true;
            }
            return false;
        }
        public bool MatchRules(Player curPlayer, int pickRow, int pickCol, int dropRow, int dropCol)
        {
            //bool validFlag = false;
            //MessageBox.Show(_piece[dropRow, dropCol].ToString());
            //自己的棋子
            if (_piece[dropRow, dropCol].ToString().Substring(0, 1) == "红")
                return false;
            if (Xiang(dropRow, dropCol))
                return true;
            if (Bing(dropRow, dropCol))
                return true;
            if (ju(dropRow, dropCol))
                return true;
            if (ma(dropRow, dropCol))
                return true;
            if (pao(dropRow, dropCol))
                return true;
            if (shi(dropRow, dropCol))
                return true;
            if (jiang(dropRow, dropCol))
                return true;
            return false;
        }
        //播放MP3
        public void spPlay(Stream stream)
        {
            sp = new SoundPlayer(stream);
            sp.Play();
        }
        public void Initialize()
        {
            _CurPlayer = Player.无;
            _PickPiece = Piece.无子;
            _PickRow = -1;
            _PickCol = -1;
            //全部设置为无子
            for (int row = 1; row <= 10; row++)
                for (int col = 1; col <= 9; col++)
                    _Piece[row, col] = Piece.无子;
        }
        public Player IsOver()
        {
            if (_lastStep._dropPiece == Piece.黑帅)
                return Player.红;
            else return Player.无;
        }
    }
}
