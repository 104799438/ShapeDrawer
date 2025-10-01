using System.IO;            
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyLine : Shape
    {
        private float _endX;
        private float _endY;

        public float EndX { get => _endX; set => _endX = value; }
        public float EndY { get => _endY; set => _endY = value; }

        public MyLine() : this(Color.Red, 0.0f, 0.0f, 120.0f, 0.0f) { }
        public MyLine(Color color, float startX, float startY, float endX, float endY) : base(color)
        {
            _x = startX;
            _y = startY;
            _endX = endX;
            _endY = endY;
        }

        public override void Draw()
        {
            if (Selected) { DrawOutline(); }
            SplashKit.DrawLine(_color, _x, _y, _endX, _endY);
        }
        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, _x, _y, 5);
            SplashKit.DrawCircle(Color.Black, _endX, _endY, 5);
        }
        public override bool IsAt(Point2D pt)
        {
            Line ln = SplashKit.LineFrom(_x, _y, _endX, _endY);
            return SplashKit.PointOnLine(pt, ln);
        }

        public override void ResizeBy(int delta)
        {
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Line");
            base.SaveTo(writer);
            writer.WriteLine(_endX);
            writer.WriteLine(_endY);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _endX = reader.ReadSingle();
            _endY = reader.ReadSingle();
        }
    }
}
