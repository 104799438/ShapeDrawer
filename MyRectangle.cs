using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class MyRectangle : Shape
    {
        private int _width;
        private int _height;

        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        public MyRectangle() : this(Color.Green, 0, 0, 100, 100)
        {
        }

        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            X = x;
            Y = y;
            _width = width;
            _height = height;
        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

        public override void DrawOutline()
        {
            SplashKit.DrawRectangle(Color.Black, _x - 2, _y - 2, _width + 4, _height + 4);
        }

        public override bool IsAt(Point2D pt)
        {
            Rectangle rect = SplashKit.RectangleFrom(_x, _y, _width, _height);
            return SplashKit.PointInRectangle(pt, rect);
        }

        public override void ResizeBy(int delta)
        {
            _width += delta;
            _height += delta;
            if (_width < 1) _width = 1;
            if (_height < 1) _height = 1;
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");
            base.SaveTo(writer);
            writer.WriteLine(Width);
            writer.WriteLine(Height);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            Width = reader.ReadInteger();
            Height = reader.ReadInteger();
        }
    }
}