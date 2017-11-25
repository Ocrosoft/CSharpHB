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
        Stop, Line, Rectangle, Circle, Sketch
    }
    public abstract class Shape
    {
        public abstract void Write(BinaryWriter binaryWriter);
        public abstract void Read(BinaryReader binaryReader);
        private int _penWidth = 10;
        private Color _penColor = Color.Red;
        private DashStyle _dashStyle;

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
    }
}
