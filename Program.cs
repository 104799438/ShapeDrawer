using SplashKitSDK;
using System.Collections.Generic;


// Entry point (Main)
// Handles window creation, keyboard/mouse input, and chooses which shape to add.
// Uses Drawing to manage all shapes. 
namespace ShapeDrawer
{
    public class Program
    {
        private const int DEFAULT_SIZE = 60;

        // Enum helps decide which shape to add
        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line,
            pentagon
        }

        public static void Main()
        {
            Window window = new Window("Shape Drawer", 800, 600);
            Drawing myDrawing = new Drawing();

            ShapeKind kindToAdd = ShapeKind.Circle; // Default shape

            // ID digit requirement: number of parallel lines
            const int PARALLEL_LINES = 8;

            do
            {
                SplashKit.ProcessEvents();

                // Switch active shape type by pressing keys
                if (SplashKit.KeyTyped(KeyCode.RKey)) kindToAdd = ShapeKind.Rectangle;
                if (SplashKit.KeyTyped(KeyCode.CKey)) kindToAdd = ShapeKind.Circle;
                if (SplashKit.KeyTyped(KeyCode.LKey)) kindToAdd = ShapeKind.Line;
                if (SplashKit.KeyTyped(KeyCode.PKey)) kindToAdd = ShapeKind.Pentagon;

                // Left-click → add shape
                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    float mx = SplashKit.MouseX();
                    float my = SplashKit.MouseY();

                    if (kindToAdd == ShapeKind.Rectangle)
                    {
                        MyRectangle rect = new MyRectangle(Color.Chocolate, mx, my, DEFAULT_SIZE, DEFAULT_SIZE);
                        myDrawing.AddShape(rect);
                    }
                    else if (kindToAdd == ShapeKind.Circle)
                    {
                        MyCircle circle = new MyCircle(Color.CadetBlue, DEFAULT_SIZE / 2);
                        circle.X = mx;
                        circle.Y = my;
                        myDrawing.AddShape(circle);
                    }
                    else if (kindToAdd == ShapeKind.Line)
                    {
                        // Draw N parallel lines, offset by 10 pixels
                        int i = 0;
                        while (i < PARALLEL_LINES)
                        {
                            float offsetY = my + (float)(i * 10);
                            MyLine line = new MyLine(Color.Red, mx - 60.0f, offsetY, mx + 60.0f, offsetY);
                            myDrawing.AddShape(line);
                            i = i + 1;
                        }
                    }
                    else if (kindToAdd == ShapeKind.Pentagon)
                    {
                        // Center at mouse; radius ~ DEFAULT_SIZE; zero rotation
                        MyPentagon pent = new MyPentagon(Color.MediumPurple, DEFAULT_SIZE, 0.0f);
                        pent.X = mx;
                        pent.Y = my;
                        myDrawing.AddShape(pent);
                    }
                }

                // Right-click -> select shapes at mouse position
                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    Point2D point = SplashKit.MousePosition();
                    myDrawing.SelectShapesAt(point);
                }

                // Press space -> random background color
                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    Color random = SplashKit.RandomColor();
                    myDrawing.Background = random;
                }

                // Delete/backspace -> remove all selected shapes
                if (SplashKit.KeyTyped(KeyCode.DeleteKey) || SplashKit.KeyTyped(KeyCode.BackspaceKey))
                {
                    List<Shape> selected = myDrawing.SelectedShapes;
                    int idx = 0;
                    while (idx < selected.Count)
                    {
                        myDrawing.RemoveShape(selected[idx]);
                        idx = idx + 1;
                    }
                }

                // Resize selected (+ and - keys)
                if (SplashKit.KeyTyped(KeyCode.EqualsKey) || SplashKit.KeyTyped(KeyCode.KeypadPlus))
                {
                    myDrawing.ResizeSelectedShapes(38);
                }
                if (SplashKit.KeyTyped(KeyCode.MinusKey) || SplashKit.KeyTyped(KeyCode.KeypadMinus))
                {
                    myDrawing.ResizeSelectedShapes(-38);
                }

                // Draw all shapes
                myDrawing.Draw();
                SplashKit.RefreshScreen();
            }
            while (!window.CloseRequested);
        }
    }
}
