using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.IO;
using System.Drawing.Drawing2D;

namespace DrawBoard
{
    public enum OperationType
    {
        Stop, Line, Rectangle, Circle, Sketch, Delete
    }
    public abstract class Shape
    {
        public abstract void Write(BinaryWriter binaryWriter);
        public abstract void Read(BinaryReader binaryReader);
        private int _penWidth = 10;
        private Color _penColor = Color.Red;
        private DashStyle _dashStyle;
        public abstract bool Select(Point point, double distance);

        public DashStyle _DashStyle
        {
            get { return _dashStyle; }
            set { _dashStyle = value; }
        }

        public int _PenWidth
        {
            get { return _penWidth; }
            set { _penWidth = value; }
        }
        public Color _PenColor
        {
            get { return _penColor; }
            set { _penColor = value; }
        }
        public abstract void Draw(Graphics g, double zoomRatio);
    }
    public class Line : Shape
    {
        private Point _p1;
        private Point _p2;

        public Point _P1
        {
            get { return _p1; }
            set { _p1 = value; }
        }
        public Point _P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }

        public Line() { }

        public override void Draw(Graphics g,double zoomRatio)
        {
            Pen pen = new Pen(base._PenColor, (float)((int)(base._PenWidth * zoomRatio)))
            {
                DashStyle = base._DashStyle
            };
            g.DrawLine(pen, (int)(this._p1.X * zoomRatio), (int)(this._p1.Y * zoomRatio), (int)(this._p2.X * zoomRatio), (int)(this._p2.Y * zoomRatio));
        }
        public override void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(_PenWidth);
            binaryWriter.Write(_PenColor.ToArgb());
            binaryWriter.Write(_p1.X); binaryWriter.Write(_p1.Y);
            binaryWriter.Write(_p2.X); binaryWriter.Write(_p2.Y);
        }
        public override void Read(BinaryReader binaryReader)
        {
            _PenWidth = binaryReader.ReadInt32();
            _PenColor = Color.FromArgb(binaryReader.ReadInt32());
            _p1.X = binaryReader.ReadInt32(); _p1.Y = binaryReader.ReadInt32();
            _p2.X = binaryReader.ReadInt32(); _p2.Y = binaryReader.ReadInt32();
        }
        public override bool Select(Point point, double distance)
        {
            double num = Math.Abs((int)(((this._p2.X - this._p1.X) * (point.Y - this._p1.Y)) - ((point.X - this._p1.X) * (this._p2.Y - this._p1.Y)))) / 2;
            double num2 = Math.Sqrt((double)(((this._p2.X - this._p1.X) * (this._p2.X - this._p1.X)) + ((this._p2.Y - this._p1.Y) * (this._p2.Y - this._p1.Y))));
            double num3 = (2.0 * num) / num2;
            double num4 = (this._p1.X <= this._p2.X) ? ((double)this._p1.X) : ((double)this._p2.X);
            double num5 = (this._p1.X <= this._p2.X) ? ((double)this._p2.X) : ((double)this._p1.X);
            double num6 = (this._p1.Y <= this._p2.Y) ? ((double)this._p1.Y) : ((double)this._p2.Y);
            double num7 = (this._p1.Y <= this._p2.Y) ? ((double)this._p2.Y) : ((double)this._p1.Y);
            double num8 = (num7 - num6) / num2;
            double num9 = (num5 - num4) / num2;
            return ((((num3 <= distance) && (point.X >= (num4 - (distance * num8)))) && ((point.X <= (num5 + (distance * num8))) && (point.Y >= (num6 - (distance * num9))))) && (point.Y <= (num7 + (distance * num9))));
        }
    }
    public class Rectangle : Shape
    {
        private Point _p1;
        private Point _p2;

        public Point _P1
        {
            get { return _p1; }
            set { _p1 = value; }
        }
        public Point _P2
        {
            get { return _p2; }
            set { _p2 = value; }
        }
        public Rectangle()
        { }
        public override void Draw(Graphics g, double zoomRatio)
        {
            Pen pen = new Pen(base._PenColor, (float)((int)(base._PenWidth * zoomRatio)))
            {
                DashStyle = base._DashStyle
            };
            Point point = new Point
            {
                X = (this._p1.X <= this._p2.X) ? this._p1.X : this._p2.X,
                Y = (this._p1.Y <= this._p2.Y) ? this._p1.Y : this._p2.Y
            };
            g.DrawRectangle(pen, (int)(point.X * zoomRatio), (int)(point.Y * zoomRatio), (int)(Math.Abs((int)(this._p2.X - this._p1.X)) * zoomRatio), (int)(Math.Abs((int)(this._p2.Y - this._p1.Y)) * zoomRatio));
        }
        public override void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(_PenWidth);
            binaryWriter.Write(_PenColor.ToArgb());
            binaryWriter.Write(_p1.X); binaryWriter.Write(_p1.Y);
            binaryWriter.Write(_p2.X); binaryWriter.Write(_p2.Y);
        }
        public override void Read(BinaryReader binaryReader)
        {
            _PenWidth = binaryReader.ReadInt32();
            _PenColor = Color.FromArgb(binaryReader.ReadInt32());
            _p1.X = binaryReader.ReadInt32(); _p1.Y = binaryReader.ReadInt32();
            _p2.X = binaryReader.ReadInt32(); _p2.Y = binaryReader.ReadInt32();
        }
        public override bool Select(Point point, double distance)
        {
            Point point2 = new Point
            {
                X = (this._p1.X <= this._p2.X) ? this._p1.X : this._p2.X,
                Y = (this._p1.Y <= this._p2.Y) ? this._p1.Y : this._p2.Y
            };
            Point point3 = new Point
            {
                X = (this._p1.X <= this._p2.X) ? this._p2.X : this._p1.X,
                Y = (this._p1.Y <= this._p2.Y) ? this._p2.Y : this._p1.Y
            };
            return (((((point.X >= (point2.X - distance)) && (point.X <= (point2.X + distance))) && (point.Y >= (point2.Y - distance))) && (point.Y <= (point3.Y + distance))) || (((((point.X >= (point3.X - distance)) && (point.X <= (point3.X + distance))) && (point.Y >= (point2.Y - distance))) && (point.Y <= (point3.Y + distance))) || (((((point.X >= (point2.X - distance)) && (point.X <= (point3.X + distance))) && (point.Y >= (point2.Y - distance))) && (point.Y <= (point2.Y + distance))) || ((((point.X >= (point2.X - distance)) && (point.X <= (point3.X + distance))) && (point.Y >= (point3.Y - distance))) && (point.Y <= (point3.Y + distance))))));
        }
    }
    public class Circle : Shape
    {
        public Point _pCenter;
        public double _r;
        public Point _PCenter
        {
            get { return _pCenter; }
            set { _pCenter = value; }
        }
        public double _R
        {
            get { return _r; }
            set { _r = value; }
        }
        public Circle()
        { }
        public override void Draw(Graphics g, double zoomRatio)
        {
            Pen pen = new Pen(base._PenColor, (float)((int)(base._PenWidth * zoomRatio)))
            {
                DashStyle = base._DashStyle
            };
            g.DrawLine(pen, (int)((this._pCenter.X - 3) * zoomRatio), (int)(this._pCenter.Y * zoomRatio), (int)((this._pCenter.X + 3) * zoomRatio), (int)(this._pCenter.Y * zoomRatio));
            g.DrawLine(pen, (int)(this._pCenter.X * zoomRatio), (int)((this._pCenter.Y - 3) * zoomRatio), (int)(this._pCenter.X * zoomRatio), (int)((this._pCenter.Y + 3) * zoomRatio));
            g.DrawEllipse(pen, (int)((this._pCenter.X - this._r) * zoomRatio), (int)((this._pCenter.Y - this._r) * zoomRatio), (int)((2f * this._r) * zoomRatio), (int)((2f * this._r) * zoomRatio));
        }
        public override void Write(BinaryWriter binaryWriter)
        {
            binaryWriter.Write(_PenWidth);
            binaryWriter.Write(_PenColor.ToArgb());
            binaryWriter.Write(_pCenter.X); binaryWriter.Write(_pCenter.Y);
            binaryWriter.Write(_r);
        }
        public override void Read(BinaryReader binaryReader)
        {
            _PenWidth = binaryReader.ReadInt32();
            _PenColor = Color.FromArgb(binaryReader.ReadInt32());
            _pCenter.X = binaryReader.ReadInt32(); _pCenter.Y = binaryReader.ReadInt32();
            _r = binaryReader.ReadDouble();
        }
        public override bool Select(Point point, double distance)
        {
            double num = Math.Sqrt((double)(((point.X - this._pCenter.X) * (point.X - this._pCenter.X)) + ((point.Y - this._pCenter.Y) * (point.Y - this._pCenter.Y))));
            return ((num >= (this._r - distance)) && (num <= (this._r + distance)));
        }
    }
    public class Sketch : Shape
    {
        public List<Point> _pointList = new List<Point>();
        public Sketch()
        { }
        public override void Draw(Graphics g, double zoomRatio)
        {
            Pen pen = new Pen(base._PenColor, (float)((int)(base._PenWidth * zoomRatio)))
            {
                DashStyle = base._DashStyle,
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };
            for (int i = 1; i <= (this._pointList.Count - 1); i++)
            {
                Point point1 = this._pointList[i - 1];
                Point point2 = this._pointList[i];
                g.DrawLine(pen, new Point((int)(point1.X * zoomRatio), (int)(point1.Y * zoomRatio)), new Point((int)(point2.X * zoomRatio), (int)(point2.Y * zoomRatio)));
            }
        }
        public override void Write(BinaryWriter binatyWriter)
        {
            binatyWriter.Write(_PenWidth);
            binatyWriter.Write(_PenColor.ToArgb());
            binatyWriter.Write(_pointList.Count());
            foreach (Point tempPoint in _pointList)
            {
                binatyWriter.Write(tempPoint.X);
                binatyWriter.Write(tempPoint.Y);
            }
        }
        public override void Read(BinaryReader binaryReader)
        {
            _pointList.Clear();
            _PenWidth = binaryReader.ReadInt32();
            _PenColor = Color.FromArgb(binaryReader.ReadInt32());
            int pointCount = binaryReader.ReadInt32();
            for (int i = 0; i <= pointCount - 1; i++)
            {
                int x = binaryReader.ReadInt32();
                int y = binaryReader.ReadInt32();
                Point point = new Point(x, y);
                _pointList.Add(point);
            }
        }
        public override bool Select(Point point, double distance)
        {
            for (int i = 1; i <= (this._pointList.Count - 1); i++)
            {
                if (this.SelectLine(point, this._pointList[i - 1], this._pointList[i], distance))
                {
                    return true;
                }
            }
            return false;
        }
        public bool SelectLine(Point point, Point p1, Point p2, double distance)
        {
            double num = Math.Abs((int)(((p2.X - p1.X) * (point.Y - p1.Y)) - ((point.X - p1.X) * (p2.Y - p1.Y)))) / 2;
            double num2 = Math.Sqrt((double)(((p2.X - p1.X) * (p2.X - p1.X)) + ((p2.Y - p1.Y) * (p2.Y - p1.Y))));
            double num3 = (2.0 * num) / num2;
            double num4 = (p1.X <= p2.X) ? ((double)p1.X) : ((double)p2.X);
            double num5 = (p1.X <= p2.X) ? ((double)p2.X) : ((double)p1.X);
            double num6 = (p1.Y <= p2.Y) ? ((double)p1.Y) : ((double)p2.Y);
            double num7 = (p1.Y <= p2.Y) ? ((double)p2.Y) : ((double)p1.Y);
            double num8 = (num7 - num6) / num2;
            double num9 = (num5 - num4) / num2;
            return ((((num3 <= distance) && (point.X >= (num4 - (distance * num8)))) && ((point.X <= (num5 + (distance * num8))) && (point.Y >= (num6 - (distance * num9))))) && (point.Y <= (num7 + (distance * num9))));
        }
    }
}
