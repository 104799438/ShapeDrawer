using SplashKitSDK;
using System;

namespace ShapeDrawer
{
    /// <summary>
    /// A regular pentagon centered at (X, Y) with a configurable radius and rotation.
    /// - Draw(): fills the pentagon
    /// - DrawOutline(): draws the polygon outline
    /// - IsAt(): point-in-polygon (ray casting)
    /// - ResizeBy(): adjusts radius, clamped to MIN_RADIUS
    /// </summary>
    public class MyPentagon : Shape
    {
        // Radius from center to each vertex
        private int _radius;

        // Minimum allowed radius to avoid collapsing
        private const int MIN_RADIUS = 10;

        // Outline padding for the selection outline
        // You can tune this to match your other shapes
        private const int OUTLINE_PAD = 10;

        private float _rotationDegrees;

        public int Radius
        {
            get { return _radius; }
            set { _radius = value; }
        }

        public float RotationDegrees
        {
            get { return _rotationDegrees; }
            set { _rotationDegrees = value; }
        }

        /// Default pentagon: blue, radius 60, no rotation.
        public MyPentagon() : this(Color.Blue, 60, 0.0f)
        {
        }

        /// Construct a pentagon with a color, radius, and rotation (degrees).
        public MyPentagon(Color color, int radius, float rotationDegrees) : base(color)
        {
            _radius = radius;
            _rotationDegrees = rotationDegrees;
        }

        /// Compute the 5 vertices of a regular pentagon centered at (X,Y).
        /// Order is counter-clockwise so the polygon is defined properly.
        private Point2D[] ComputeVertices()
        {
            int sides = 5;
            Point2D[] pts = new Point2D[sides];

            // Convert rotation to radians
            double rotRad = _rotationDegrees * Math.PI / 180.0;

            int i = 0;
            while (i < sides)
            {
                // Angle for each vertex
                // Start at rotation, then step by 2π/5
                double angle = rotRad + (2.0 * Math.PI * i) / (double)sides;

                // Compute vertex position
                float vx = _x + (float)((double)_radius * Math.Cos(angle));
                float vy = _y + (float)((double)_radius * Math.Sin(angle));

                Point2D p = new Point2D();
                p.X = vx;
                p.Y = vy;
                pts[i] = p;

                i = i + 1;
            }
            return pts;
        }

        /// Fill the pentagon by triangulating fan (center point + edges).
        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }

            Point2D[] v = ComputeVertices();

            // Fan triangulation: pick a fixed vertex (v[0]) and make triangles (v[0], v[i], v[i+1])
            // This works because the regular pentagon is convex.
            int i = 1;
            while (i < v.Length - 1)
            {
                SplashKit.FillTriangle(_color, v[0].X, v[0].Y, v[i].X, v[i].Y, v[i + 1].X, v[i + 1].Y);
                i = i + 1;
            }
        }

        /// Draw polygon outline by connecting vertices and closing the loop.
        /// Also draws a padded outline polygon to mimic your rectangle outline behavior.
        public override void DrawOutline()
        {
            Point2D[] v = ComputeVertices();

            // 1) Draw the polygon edges
            int i = 0;
            while (i < v.Length)
            {
                int j = i + 1;
                if (j == v.Length) j = 0; // wrap last->first

                SplashKit.DrawLine(Color.Black, v[i].X, v[i].Y, v[j].X, v[j].Y);
                i = i + 1;
            }

            // 2) Optional: draw a second, slightly "expanded" outline
            //    We expand each vertex from the center by OUTLINE_PAD.
            //    This visually matches the idea of a selection outline.
            Point2D[] v2 = new Point2D[v.Length];
            int k = 0;
            while (k < v.Length)
            {
                // Vector from center to vertex
                float dx = v[k].X - _x;
                float dy = v[k].Y - _y;

                // Length of that vector
                double len = Math.Sqrt((double)dx * (double)dx + (double)dy * (double)dy);
                if (len == 0.0)
                {
                    // Degenerate, just copy
                    v2[k] = v[k];
                }
                else
                {
                    // Unit vector * (radius + OUTLINE_PAD)
                    double ux = (double)dx / len;
                    double uy = (double)dy / len;
                    float px = _x + (float)((double)_radius + (double)OUTLINE_PAD) * (float)ux;
                    float py = _y + (float)((double)_radius + (double)OUTLINE_PAD) * (float)uy;

                    Point2D p = new Point2D();
                    p.X = px;
                    p.Y = py;
                    v2[k] = p;
                }
                k = k + 1;
            }

            int m = 0;
            while (m < v2.Length)
            {
                int n = m + 1;
                if (n == v2.Length) n = 0;
                SplashKit.DrawLine(Color.Black, v2[m].X, v2[m].Y, v2[n].X, v2[n].Y);
                m = m + 1;
            }
        }

        /// Point-in-polygon using ray casting (odd-even rule).
        /// Works fine for convex polygons like a regular pentagon.
        public override bool IsAt(Point2D pt)
        {
            Point2D[] v = ComputeVertices();
            bool inside = false;

            int count = v.Length;
            int i = 0;
            int j = count - 1; // previous vertex index

            while (i < count)
            {
                float xi = v[i].X;
                float yi = v[i].Y;
                float xj = v[j].X;
                float yj = v[j].Y;

                // Check if the ray horizontally to the right from pt crosses edge (j->i)
                bool intersect = ((yi > pt.Y) != (yj > pt.Y));
                if (intersect)
                {
                    // Compute x-coordinate of intersection of edge with horizontal line at pt.Y
                    float t = (pt.Y - yi) / (yj - yi);
                    float xCross = xi + t * (xj - xi);

                    if (xCross >= pt.X)
                    {
                        inside = !inside;
                    }
                }

                j = i;
                i = i + 1;
            }

            return inside;
        }

        /// Increase/decrease radius with clamping to MIN_RADIUS.
        public override void ResizeBy(int delta)
        {
            int newRadius = _radius + delta;
            if (newRadius < MIN_RADIUS)
            {
                newRadius = MIN_RADIUS;
            }
            _radius = newRadius;
        }
    }
}
