using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Tank
{
    public enum GameState
    {
        Close, Open, Pause
    }
    public enum Direction
    {
        Up, Down, Left, Right
    }
    public enum Side
    {
        Me, Enemy
    }
    class ttank
    {
        private Point _positon = new Point(200, 200);
        private Direction _direction = Direction.Up;
        private int _step = 5;
        private int _size = 30;
        Side _side;

        private Bitmap[] _tankBmp = new Bitmap[8];
        private Bitmap _nowTankBmp = new Bitmap(30, 30);
        private bool _tankBmpChange = true;

        public Point _Positon
        {
            get { return _positon; }
            set { _positon = value; }
        }
        public Direction _Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        public int _Step
        {
            get { return _step; }
            set { _step = value; }
        }
        public int _Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public Side _Side
        {
            get { return _side; }
            set { _side = value; }
        }

        public ttank(Side side)
        {
            _side = side;
            if (side == Side.Me)
            {
                _tankBmp[0] = new Bitmap("Images\\MyTankUp1.gif");
                _tankBmp[1] = new Bitmap("Images\\MyTankUp2.gif");
                _tankBmp[2] = new Bitmap("Images\\MyTankDown1.gif");
                _tankBmp[3] = new Bitmap("Images\\MyTankDown2.gif");
                _tankBmp[4] = new Bitmap("Images\\MyTankLeft1.gif");
                _tankBmp[5] = new Bitmap("Images\\MyTankLeft2.gif");
                _tankBmp[6] = new Bitmap("Images\\MyTankRight1.gif");
                _tankBmp[7] = new Bitmap("Images\\MyTankRight2.gif");

                _positon.X = Screen.PrimaryScreen.Bounds.Width / 2 - _size / 2;
                _positon.Y = Screen.PrimaryScreen.Bounds.Height - 150;
                _direction = Direction.Up;
            }
            else
            {
                _tankBmp[0] = new Bitmap("Images\\EnemyTankUp1.gif");
                _tankBmp[1] = new Bitmap("Images\\EnemyTankUp2.gif");
                _tankBmp[2] = new Bitmap("Images\\EnemyTankDown1.gif");
                _tankBmp[3] = new Bitmap("Images\\EnemyTankDown2.gif");
                _tankBmp[4] = new Bitmap("Images\\EnemyTankLeft1.gif");
                _tankBmp[5] = new Bitmap("Images\\EnemyTankLeft2.gif");
                _tankBmp[6] = new Bitmap("Images\\EnemyTankRight1.gif");
                _tankBmp[7] = new Bitmap("Images\\EnemyTankRight2.gif");

                _positon.X = Screen.PrimaryScreen.Bounds.Width / 2 - _size / 2;
                _positon.Y = _size;
                _direction = Direction.Down;
            }
            for (int i = 0; i <= 7; i++)
                _tankBmp[i].MakeTransparent(Color.Black);
            _nowTankBmp = _tankBmp[0];
        }

        public void Move(Direction direction)
        {
            _direction = direction;
            if (_direction == Direction.Up)
            {
                _positon.Y = _positon.Y - _step;
                if (_tankBmpChange == true)
                    _nowTankBmp = _tankBmp[0];
                else
                    _nowTankBmp = _tankBmp[1];
            }
            else if (_direction == Direction.Down)
            {
                _positon.Y = _positon.Y + _step;
                if (_tankBmpChange == true)
                    _nowTankBmp = _tankBmp[2];
                else
                    _nowTankBmp = _tankBmp[3];
            }
            else if (_direction == Direction.Left)
            {
                _positon.X = _positon.X - _step;
                if (_tankBmpChange == true)
                    _nowTankBmp = _tankBmp[4];
                else
                    _nowTankBmp = _tankBmp[5];
            }
            else if (_direction == Direction.Right)
            {
                _positon.X = _positon.X + _step;
                if (_tankBmpChange == true)
                    _nowTankBmp = _tankBmp[6];
                else
                    _nowTankBmp = _tankBmp[7];
            }
            _tankBmpChange = !_tankBmpChange;
        }

        public void DrawMe(Graphics g)
        {
            g.DrawImage(_nowTankBmp, _positon);
        }

        public Bullet Fire()
        {
            Bullet myBullet = new Bullet(_side, _direction);
            if(_direction==Direction.Up)
                myBullet._Position=new Point(_positon.X+8,_positon.Y-15);
            else if(_direction==Direction.Down)
                myBullet._Position=new Point(_positon.X+8,_positon.Y+30);
            else if(_direction==Direction.Left)
                myBullet._Position=new Point(_positon.X-15,_positon.Y+8);
            else if(_direction==Direction.Right)
                myBullet._Position=new Point(_positon.X+30,_positon.Y+8);
            return myBullet;
        }
    }
}
