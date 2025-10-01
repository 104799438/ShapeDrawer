using System.Collections.Generic;
using System.IO;                 
using SplashKitSDK;

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
        public Drawing() : this(Color.White) { }

        public Color Background { get => _background; set => _background = value; }
        public int ShapeCount => _shapes.Count;

        public void AddShape(Shape shape) { if (shape != null) _shapes.Add(shape); }
        public void RemoveShape(Shape shape) { if (shape != null) _shapes.Remove(shape); }

        public void Draw()
        {
            SplashKit.ClearScreen(_background);
            foreach (Shape shape in _shapes) shape.Draw();
        }

        public void SelectShapesAt(Point2D point)
        {
            foreach (Shape shape in _shapes) shape.Selected = shape.IsAt(point);
        }

        public List<Shape> SelectedShapes
        {
            get
            {
                List<Shape> result = new List<Shape>();
                foreach (Shape shape in _shapes) if (shape.Selected) result.Add(shape);
                return result;
            }
        }

        public void ResizeSelectedShapes(int meow)
        {
            foreach (Shape shape in SelectedShapes) shape.ResizeBy(meow);
        }

        public void Save(string filename)
        {
            StreamWriter writer = null;
            try
            {
                writer = new StreamWriter(filename);
                writer.WriteColor(_background);         
                writer.WriteLine(ShapeCount);           
                foreach (Shape s in _shapes) s.SaveTo(writer);
            }
            finally
            {
                if (writer != null) writer.Close();
            }
        }

        public void Load(string filename)
        {
            StreamReader reader = null;
            try
            {
                reader = new StreamReader(filename);

                _background = reader.ReadColor();
                int count = reader.ReadInteger();
                _shapes.Clear();

                for (int i = 0; i < count; i++)
                {
                    string kind = reader.ReadLine();
                    Shape s;

                    switch (kind)
                    {
                        case "Rectangle": s = new MyRectangle(); break;
                        case "Circle":    s = new MyCircle();    break;
                        case "Line":      s = new MyLine();      break;
                        case "Pentagon":  s = new MyPentagon();  break;
                        default:
                            throw new InvalidDataException($"Unknown shape kind: {kind}");
                    }

                    s.LoadFrom(reader);
                    AddShape(s);
                }
            }
            finally
            {
                if (reader != null) reader.Close();
            }
        }
    }
}
