using SplashKitSDK;
// Subclass of Shape.
// Adds Width and Height.
// Overrides Draw, DrawOutline, IsAt, and ResizeBy.
namespace ShapeDrawer
{
    public class MyRectangle : Shape
    {
        private int _width;
        private int _height;

        private const int MIN_SIZE = 10;   // Rectangle can't shrink below this
        private const int ID_LAST_TWO = 38; 
        private const int OUTLINE_PAD = 5 + ID_LAST_TWO; // Padding for outline

        // Width property
        public int Width
        {
            get { return _width; }
            set { _width = value; }
        }

        // Height property
        public int Height
        {
            get { return _height; }
            set { _height = value; }
        }

        // Default constructor: creates a green rectangle
        public MyRectangle() : this(Color.Green, 0.0f, 0.0f, 100 + ID_LAST_TWO, 100 + ID_LAST_TWO)
        {
        }

        // Constructor that takes color, position, width, and height
        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        // Draws the rectangle. If selected, also draw its outline
        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }
            SplashKit.FillRectangle(_color, _x, _y, _width, _height);
        }

        // Draws a black outline slightly bigger than the rectangle
        public override void DrawOutline()
        {
            float ox = _x - OUTLINE_PAD;
            float oy = _y - OUTLINE_PAD;
            float ow = _width + OUTLINE_PAD * 2;
            float oh = _height + OUTLINE_PAD * 2;
            SplashKit.DrawRectangle(Color.Black, ox, oy, ow, oh);
        }

        // Checks if a point is inside the rectangle
        public override bool IsAt(Point2D pt)
        {
            bool withinX = (pt.X >= _x) && (pt.X <= _x + _width);
            bool withinY = (pt.Y >= _y) && (pt.Y <= _y + _height);
            return withinX && withinY;
        }

        // Resize rectangle by increasing/decreasing width and height
        public override void ResizeBy(int delta)
        {
            int newWidth = _width + delta;
            int newHeight = _height + delta;

            // Prevent rectangle from disappearing
            if (newWidth < MIN_SIZE) newWidth = MIN_SIZE;
            if (newHeight < MIN_SIZE) newHeight = MIN_SIZE;

            _width = newWidth;
            _height = newHeight;
        }
    }
}
