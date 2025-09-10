using SplashKitSDK;
// Abstract base class. 
// You cannot create new Shape() directly.
// It provides a common template (properties + methods) that all shapes (Rectangle, Circle, Line) must implement.
// Defines shared properties (X, Y, Color, Selected).
// Declares abstract methods (Draw, DrawOutline, IsAt).
// Subclasses must override these.
namespace ShapeDrawer
{
    public abstract class Shape
    {
        // Shared fields (all shapes have color, x, y position, and selection status)
        protected Color _color;
        protected float _x;
        protected float _y;
        protected bool _selected;

        // Default constructor: if no color is provided, shape is Yellow by default
        public Shape() : this(Color.Yellow)
        {
        }

        // Overloaded constructor: allows a color to be passed in
        public Shape(Color color)
        {
            _color = color;
            _x = 0.0f;
            _y = 0.0f;
            _selected = false;
        }

        // Properties (with explicit getters and setters)
        public bool Selected
        {
            get { return _selected; }
            set { _selected = value; }
        }

        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        public float X
        {
            get { return _x; }
            set { _x = value; }
        }

        public float Y
        {
            get { return _y; }
            set { _y = value; }
        }

        // Abstract methods: subclasses MUST override these
        public abstract void Draw();
        public abstract void DrawOutline();
        public abstract bool IsAt(Point2D pt);

        // Virtual method: ResizeBy does nothing by default,
        // but subclasses like Rectangle and Circle override it.
        public virtual void ResizeBy(int delta)
        {
            // Left empty here
        }
    }
}

