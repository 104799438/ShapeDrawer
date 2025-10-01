using System.IO;           
using SplashKitSDK;

namespace ShapeDrawer
{
    public class MyCircle : Shape
    {
        private int _radius;
        private const int MIN_RADIUS = 10;
        private const int ID_LAST_TWO = 38;

        public int Radius { get => _radius; set => _radius = value; }

        public MyCircle() : this(Color.Blue, 50 + ID_LAST_TWO) { }
        public MyCircle(Color color, int radius) : base(color) { _radius = radius; }

        public override void Draw()
        {
            if (Selected) { DrawOutline(); }
            SplashKit.FillCircle(_color, _x, _y, _radius);
        }
        public override void DrawOutline() => SplashKit.DrawCircle(Color.Black, _x, _y, _radius + 2);
        public override bool IsAt(Point2D pt) => SplashKit.PointInCircle(pt, SplashKit.CircleAt(_x, _y, _radius));

        public override void ResizeBy(int delta)
        {
            int newRadius = _radius + delta;
            if (newRadius < MIN_RADIUS) newRadius = MIN_RADIUS;
            _radius = newRadius;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
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