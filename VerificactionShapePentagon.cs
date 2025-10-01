using SplashKitSDK;
using System;
using System.IO;

namespace ShapeDrawer
{
    public class MyPentagon : Shape
    {
        private int _radius;
        private const int MIN_RADIUS = 10;

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public MyPentagon() : this(Color.Purple, 50)
        {
        }

        public MyPentagon(Color color, int radius) : base(color)
        {
            _radius = radius;
        }

        private Point2D[] Vertices()
        {
            Point2D[] pts = new Point2D[5];
            int i = 0;
            while (i < 5)
            {
                double angle = (2.0 * Math.PI * i / 5.0) - (Math.PI / 2.0);
                float vx = _x + (float)((double)_radius * Math.Cos(angle));
                float vy = _y + (float)((double)_radius * Math.Sin(angle));
                Point2D p = new Point2D();
                p.X = vx;
                p.Y = vy;
                pts[i] = p;
                i = i + 1;
            }
            return pts;
        }

        public override void Draw()
        {
            Point2D[] v = Vertices();
            int i = 1;
            while (i < v.Length - 1)
            {
                SplashKit.FillTriangle(_color,
                    v[0].X, v[0].Y,
                    v[i].X, v[i].Y,
                    v[i + 1].X, v[i + 1].Y);
                i = i + 1;
            }
            if (Selected)
            {
                DrawOutline();
            }
        }

        public override void DrawOutline()
        {
            Point2D[] v = Vertices();
            int i = 0;
            while (i < v.Length)
            {
                int j = i + 1;
                if (j == v.Length) j = 0;
                SplashKit.DrawLine(Color.Black, v[i].X, v[i].Y, v[j].X, v[j].Y);
                i = i + 1;
            }
        }

        
        public override bool IsAt(Point2D pt)
        {
            Point2D[] v = Vertices();
            bool inside = false;
            int n = v.Length;
            int i = 0;
            int j = n - 1;
            while (i < n)
            {
                bool crosses = ((v[i].Y > pt.Y) != (v[j].Y > pt.Y));
                if (crosses)
                {
                    double xi = (double)v[i].X;
                    double yi = (double)v[i].Y;
                    double xj = (double)v[j].X;
                    double yj = (double)v[j].Y;
                    double py = (double)pt.Y;
                    double xCrossD = xi + (py - yi) * (xj - xi) / (yj - yi);
                    float xCross = (float)xCrossD;
                    if (xCross >= pt.X)
                    {
                        inside = !inside;
                    }
                }
                j = i;
                i = i + 1;
            }
            return inside;
        }

        public override void ResizeBy(int delta)
        {
            int newRadius = _radius + delta;
            if (newRadius < MIN_RADIUS)
            {
                newRadius = MIN_RADIUS;
            }
            _radius = newRadius;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Pentagon");
            base.SaveTo(writer);
            writer.WriteLine(_radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _radius = reader.ReadInteger();
        }

    }
}