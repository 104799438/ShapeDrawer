using SplashKitSDK;
// Subclass of Shape.
// Adds Radius.
// Overrides Draw, DrawOutline, IsAt, and ResizeBy.
namespace ShapeDrawer
{
    public class MyCircle : Shape
    {
        private int _radius;
        private const int MIN_RADIUS = 10;
        private const int ID_LAST_TWO = 38;  

        // Radius property
        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        // Default constructor: creates a blue circle with radius = 50 + ID
        public MyCircle() : this(Color.Blue, 50 + ID_LAST_TWO)
        {
        }

        // Constructor that takes color and radius
        public MyCircle(Color color, int radius) : base(color)
        {
            _radius = radius;
        }

        // Draw circle (with outline if selected)
        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillCircle(_color, _x, _y, _radius);
        }

        // Outline is a black circle slightly larger than the actual circle
        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, _x, _y, _radius + 2);
        }

        // Checks if a point is inside the circle
        public override bool IsAt(Point2D pt)
        {
            Circle circle = SplashKit.CircleAt(_x, _y, _radius);
            return SplashKit.PointInCircle(pt, circle);
        }

        // Resize circle by adjusting radius
        public override void ResizeBy(int delta)
        {
            int newRadius = _radius + delta;
            if (newRadius < MIN_RADIUS) newRadius = MIN_RADIUS;
            _radius = newRadius;
        }
    }
}
