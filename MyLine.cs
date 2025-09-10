using SplashKitSDK;
// Subclass of Shape.
// Adds EndX and EndY.
// Overrides Draw, DrawOutline, and IsAt.
// (Resize is empty here).
namespace ShapeDrawer
{
        public class MyLine : Shape
    {
        private float _endX;
        private float _endY;

        // EndX property
        public float EndX
        {
            get { return _endX; }
            set { _endX = value; }
        }

        // EndY property
        public float EndY
        {
            get { return _endY; }
            set { _endY = value; }
        }

        // Default constructor: red line from (0,0) to (120,0)
        public MyLine() : this(Color.Red, 0.0f, 0.0f, 120.0f, 0.0f)
        {
        }

        // Constructor with explicit endpoints
        public MyLine(Color color, float startX, float startY, float endX, float endY) : base(color)
        {
            _x = startX;
            _y = startY;
            _endX = endX;
            _endY = endY;
        }

        // Draws the line. If selected, draw small circles at endpoints
        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.DrawLine(_color, _x, _y, _endX, _endY);
        }

        // Outline -> small circles at both ends of the line
        public override void DrawOutline()
        {
            SplashKit.DrawCircle(Color.Black, _x, _y, 5);
            SplashKit.DrawCircle(Color.Black, _endX, _endY, 5);
        }

        // Checks if a point lies on the line
        public override bool IsAt(Point2D pt)
        {
            Line ln = SplashKit.LineFrom(_x, _y, _endX, _endY);
            return SplashKit.PointOnLine(pt, ln);
        }

        // Lines don’t resize in this program
        public override void ResizeBy(int delta)
        {
            // no-op
        }
    }
}
