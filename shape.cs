using System.IO;            
using SplashKitSDK;

namespace ShapeDrawer
{
    public abstract class Shape
    {
        protected Color _color;
        protected float _x;
        protected float _y;
        protected bool _selected;

        public Shape() : this(Color.Yellow) { }
        public Shape(Color color)
        {
            _color = color;
            _x = 0.0f;
            _y = 0.0f;
            _selected = false;
        }

        public bool Selected { get => _selected; set => _selected = value; }
        public Color Color { get => _color; set => _color = value; }
        public float X { get => _x; set => _x = value; }
        public float Y { get => _y; set => _y = value; }

        public abstract void Draw();
        public abstract void DrawOutline();
        public abstract bool IsAt(Point2D pt);

        public virtual void ResizeBy(int delta) { }


        public virtual void SaveTo(StreamWriter writer)
        {
            writer.WriteColor(_color);
            writer.WriteLine(_x);
            writer.WriteLine(_y);
        }

        public virtual void LoadFrom(StreamReader reader)
        {
            _color = reader.ReadColor();
            _x = reader.ReadSingle();
            _y = reader.ReadSingle();
            _selected = false; 
        }
    }
}
