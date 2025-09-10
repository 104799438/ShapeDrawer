using SplashKitSDK;

// Stores a list of shapes.
// Knows how to AddShape, RemoveShape, SelectShapesAt, ResizeSelectedShapes, and Draw them.
// Does not care what kind of shape (polymorphism handles that).
namespace ShapeDrawer
{
    public class Drawing
    {
        private readonly List<Shape> _shapes;
        private Color _background;

        public Drawing(Color background)
        {
            _shapes = new List<Shape>();
            _background = background;
        }

        public Drawing() : this(Color.White)
        {
        }

        public Color Background
        {
            get
            {
                return _background;
            }
            set
            {
                _background = value;
            }
        }

        public int ShapeCount
        {
            get
            {
                return _shapes.Count;
            }
        }

        public void AddShape(Shape shape)
        {
            if (shape != null)
            {
                _shapes.Add(shape);
            }
        }

        public void RemoveShape(Shape shape)
        {
            if (shape != null)
            {
                _shapes.Remove(shape);
            }
        }

        public void Draw()
        {
            SplashKit.ClearScreen(_background);
            foreach (Shape shape in _shapes)
            {
                shape.Draw();
            }
        }

        public void SelectShapesAt(Point2D point)
        {
            foreach (Shape shape in _shapes)
            {
                shape.Selected = shape.IsAt(point);
            }
        }

        public List<Shape> SelectedShapes
        {
            get
            {
                List<Shape> result = new List<Shape>();
                foreach (Shape shape in _shapes)
                {
                    if (shape.Selected)
                    {
                        result.Add(shape);
                    }
                }
                return result;
            }
        }

        public void ResizeSelectedShapes(int meow)
        {
            List<Shape> selected = SelectedShapes;
            foreach (Shape shape in selected)
            {
                shape.ResizeBy(meow);
            }
        }
    }
}
