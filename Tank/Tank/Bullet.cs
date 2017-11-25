using System.Drawing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tank
{
    class Bullet
    {
        private Point _position = new Point(200, 200);

        public Point _Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Direction _direction = Direction.Up;

        public Direction _Direction
        {
            get { return _direction; }
            set { _direction = value; }
        }
        private int _step = 5;

        public int _Step
        {
            get { return _step; }
            set { _step = value; }
        }
        Side _side;

        public Side _Side
        {
            get { return _side; }
            set { _side = value; }
        }
        private Bitmap _bulletBmp=new Bitmap(16,16);

        public Bullet(Side side, Direction direction)
        {
            _side = side;
            _direction = direction;
            if (side == Side.Me)
            {
                if (direction == Direction.Up)
                    _bulletBmp = new Bitmap("Images\\MyBulletUp.gif");
                else if (direction == Direction.Down)
                    _bulletBmp = new Bitmap("Images\\MyBulletDown.gif");
                else if (direction == Direction.Left)
                    _bulletBmp = new Bitmap("Images\\MyBulletLeft.gif");
                else if (direction == Direction.Right)
                    _bulletBmp = new Bitmap("Images\\MyBulletRight.gif");
            }
            else
            {
                if (direction == Direction.Up)
                    _bulletBmp = new Bitmap("Images\\EnemyBulletUp.gif");
                else if (direction == Direction.Down)
                    _bulletBmp = new Bitmap("Images\\EnemyBulletDown.gif");
                else if (direction == Direction.Left)
                    _bulletBmp = new Bitmap("Images\\EnemyBulletLeft.gif");
                else if (direction == Direction.Right)
                    _bulletBmp = new Bitmap("Images\\EnemyBulletRight.gif");
            }
            _bulletBmp.MakeTransparent(Color.Black);
        }
        public void Move()
        {
            if (_direction == Direction.Up)
                _position.Y = _position.Y - _step;
            else if (_direction == Direction.Down)
                _position.Y = _position.Y + _step;
            else if (_direction == Direction.Left)
                _position.X = _position.X - _step;
            else if (_direction == Direction.Right)
                _position.X = _position.X + _step;
        }
        public void DrawMe(Graphics g)
        {
            g.DrawImage(_bulletBmp, _position);
        }
    }
}
