using System;
using System.IO;
using System.Collections.Generic;
using SplashKitSDK;

namespace ShapeDrawer
{
    public class Program
    {
        private const int DEFAULT_SIZE = 60;

        private enum ShapeKind
        {
            Rectangle,
            Circle,
            Line,
            Pentagon
        }

        public static void Main()
        {
            Window window = new Window("Shape Drawer", 800, 600);
            Drawing myDrawing = new Drawing();
            ShapeKind kindToAdd = ShapeKind.Circle;
            const int PARALLEL_LINES = 8;


            string desktop  = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory);
            string saveFile = Path.Combine(desktop, "TestDrawing.txt");

            do
            {
                SplashKit.ProcessEvents();

                if (SplashKit.KeyTyped(KeyCode.RKey)) kindToAdd = ShapeKind.Rectangle;
                if (SplashKit.KeyTyped(KeyCode.CKey)) kindToAdd = ShapeKind.Circle;
                if (SplashKit.KeyTyped(KeyCode.LKey)) kindToAdd = ShapeKind.Line;
                if (SplashKit.KeyTyped(KeyCode.PKey)) kindToAdd = ShapeKind.Pentagon;

                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    try
                    {
                        myDrawing.Save(saveFile);
                        Console.WriteLine($"Saved drawing to: {saveFile}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Save failed: {ex.Message}");
                    }
                }

                if (SplashKit.KeyTyped(KeyCode.OKey))
                {
                    try
                    {
                        myDrawing.Load(saveFile);
                        Console.WriteLine($"Loaded drawing from: {saveFile}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Error loading file: " + ex.Message);
                    }
                }

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
                        MyPentagon pent = new MyPentagon(Color.Purple, DEFAULT_SIZE);
                        pent.X = mx;
                        pent.Y = my;
                        myDrawing.AddShape(pent);
                    }
                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))
                {
                    Point2D point = SplashKit.MousePosition();
                    myDrawing.SelectShapesAt(point);
                }

                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    Color random = SplashKit.RandomColor();
                    myDrawing.Background = random;
                }

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

                if (SplashKit.KeyTyped(KeyCode.EqualsKey) || SplashKit.KeyTyped(KeyCode.KeypadPlus))
                {
                    myDrawing.ResizeSelectedShapes(38);
                }

                if (SplashKit.KeyTyped(KeyCode.MinusKey) || SplashKit.KeyTyped(KeyCode.KeypadMinus))
                {
                    myDrawing.ResizeSelectedShapes(-38);
                }

                myDrawing.Draw();
                SplashKit.RefreshScreen();
            }
            while (!window.CloseRequested);

            window.Close();
        }
    }
}
