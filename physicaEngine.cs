using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.IO;
using System.Text;
using physica;
using System.Windows.Forms;
using System.Numerics;
using System.Threading.Tasks;

namespace physica.engine
{
    public class physicaEngine
    {
        public class Project
        {
            public string name;
            public string path;

            public bool projectIsOpen;

            public List<string> ObjectPaths = new List<string>();

            public void newProj(string _name, string _path)
            {
                Program.c.print($"Creating project at path: {_path}\nWith name: {_name}");
                try
                {
                    string a;
                    if (_path[_path.Length - 1] == @"\".ToCharArray()[0]) { a = ""; } // gay
                    else { a = @"\"; }

                    Directory.CreateDirectory(_path);
                    File.Create($"{_path}{a}{_name}Prj.pap").Dispose();
                    Directory.CreateDirectory($"{_path}{a}Objects");
                }
                catch (Exception ex)
                {
                    Program.c.print($"\nError creating project with error:\n{ex.HResult}\n{ex.Message}\n{ex.HelpLink}\n");
                    return;
                }
            }

            public void addStaticHitBox(Objects.StaticHitbox obj)
            {
                if (projectIsOpen)
                {
                    List<string> lines = new List<string>();
                    lines.Add(obj.fg.ToString());
                    lines.Add(obj.bg.ToString());
                    lines.Add(obj.visible.ToString());
                    lines.Add(obj.solidity.ToString());
                    lines.Add(obj.hitBox.ToString());
                    lines.Add(obj.location.ToString());
                    File.CreateText(Path.Combine(path, $@"Objects\{obj.name}@STHB.txt")).Dispose();
                    File.WriteAllLines(Path.Combine(path, $@"Objects\{obj.name}@STHB.txt"),lines.ToArray());
                    ObjectPaths.Add($@"Objects\{obj.name}@STHB.txt");
                    Program.c.print($"StaticHitBox created at {Path.Combine(path, $@"Objects\{obj.name}@STHB.txt")}");
                }
                else { Program.c.print("No project opened, couldnt create StaticHitBox"); };
            }

            public void closeProject()
            {
                projectIsOpen = false;
            }

            public void openProject(string path)
            {

            }
        }

        public class Tools
        {
            public static int SellectTool = 0;
            public static int EditTool = 1;
            public static int DistanceTool = 2;
            public static int PolygonTool = 3;
            public static string[] names =
            {
                "Sellect tool",
                "Edit tool",
                "Distance Tool",
                "Polygon tool"
            };

            public static void DrawTool(Graphics e, PointF mousePos, PointF mouseStart, bool mouseDown, int tool)
            {
                if (tool == SellectTool)
                {
                    e.DrawString(names[tool], SystemFonts.DefaultFont, Brushes.Black, mousePos);

                    if (mouseDown)
                    {
                        RectangleF rect = EMath.calculateRectangle(mouseStart, mousePos);

                        e.DrawRectangle(Pens.Black, Rectangle.Round(rect)); 
                        e.DrawString($"{rect.Width}", SystemFonts.DefaultFont, Brushes.Black, new PointF((rect.Size.Width/2)+rect.X, rect.Y));
                        e.DrawString($"{rect.Height}", SystemFonts.DefaultFont, Brushes.Black, new PointF(rect.X, (rect.Size.Height/2)+rect.Y));
                        e.DrawString($"Area:{Math.Abs(rect.Width*rect.Height)}", SystemFonts.DefaultFont, Brushes.Black, new PointF(rect.X, rect.Y-15));
                    }
                }
                else if (tool == DistanceTool)
                {
                    e.DrawString(names[tool], SystemFonts.DefaultFont, Brushes.Black, mousePos);

                    if (mouseDown)
                    {
                        double distance = 0;
                        double angle = 0;
                        e.DrawLine(Pens.Red, mousePos, mouseStart);

                        SizeF size = new SizeF(0, -5);
                        SizeF size2 = new SizeF(0, 5);
                        e.DrawLine(Pens.Black, PointF.Subtract(mouseStart, size), PointF.Subtract(mouseStart, size2));
                        size = new SizeF(-5, 0);
                        size2 = new SizeF(5, 0);
                        e.DrawLine(Pens.Black, PointF.Subtract(mouseStart, size), PointF.Subtract(mouseStart, size2));

                        distance = Math.Round(EMath.DistanceBetweenPointFs(mousePos, mouseStart),1);
                        angle = Math.Round(EMath.AngleBetweenPointFs(mousePos, mouseStart), 1);
                        e.DrawString($"Distance:{distance}", SystemFonts.DefaultFont, Brushes.DarkRed, PointF.Empty);
                        e.DrawString($"Angle:{angle}", SystemFonts.DefaultFont, Brushes.DarkRed, new PointF(0,15));

                        e.DrawString($"X:{mouseStart.X} Y:{mouseStart.Y}", SystemFonts.DefaultFont, Brushes.DarkRed,mouseStart);
                        size = new SizeF(0, -10);
                        e.DrawString($"X:{mousePos.X} Y:{mousePos.Y}", SystemFonts.DefaultFont, Brushes.DarkRed, PointF.Add(mousePos,size));
                    }
                }
            }
        }

        public class Objects
        {
            public class Polygon
            {
                PointF pos;
                PointF[] points;
                PointF centroid;

                bool visible;
                bool useClr;
                Color fg;
                Color bg;

                public Polygon create(PointF pos, PointF[] points)
                {
                    Polygon obj = new Polygon();
                    obj.points = points;
                    obj.pos = pos;
                    obj.centroid = EMath.Centroid(points);

                    return obj;
                }
            }
            public class ValuePolygon
            {
                public PointF pos;
                public PointF[] points;
                public PointF centroid;
                public dynamic[] values;


                public bool visible;
                public bool useClr;
                public Color fg;
                public Color bg;

                public ValuePolygon create(PointF pos, PointF[] points)
                {
                    ValuePolygon obj = new ValuePolygon();
                    obj.points = points;
                    obj.pos = pos;
                    obj.centroid = EMath.Centroid(points);
                    obj.values = new dynamic[points.Length];

                    return obj;
                }

                public ValuePolygon setValues(ValuePolygon obj, dynamic[] values)
                {
                    obj.values = values;

                    return obj;
                }
                public ValuePolygon setValue(ValuePolygon obj, dynamic value, int index)
                {
                    obj.values[index] = value;

                    return obj;
                }
            }

            public class StaticHitbox
            {
                public PointF location;
                public RectangleF hitBox;
                public PointF centroid;

                public bool solidity;
                public string name;

                public bool visible;
                public Color fg;
                public Color bg;
                
                public StaticHitbox create(PointF pos, RectangleF rect, string name)
                {
                    StaticHitbox obj = new StaticHitbox();
                    obj.location = pos;
                    obj.hitBox = rect;
                    obj.name = name;
                    obj.centroid = new PointF(rect.Left + rect.Width / 2,rect.Top + rect.Height / 2);
                    return obj;
                }
                public StaticHitbox move(PointF pos, StaticHitbox obj)
                {
                    obj.location = pos;
                    obj.hitBox.Location = pos;
                    obj.centroid = new PointF(obj.hitBox.Left + obj.hitBox.Width / 2, obj.hitBox.Top + obj.hitBox.Height / 2);
                    return obj;
                }
                public StaticHitbox changeProperty(StaticHitbox obj, bool vis = true, bool sol = true, Color fg = new Color(), Color bg = new Color())
                {
                    if (fg == new Color())
                    {
                        fg = Color.Black;
                    }
                    if (bg == new Color())
                    {
                        bg = Color.HotPink;
                    }

                    obj.fg = fg;
                    obj.bg = bg;
                    obj.visible = vis;
                    obj.solidity = sol;

                    return obj;
                }
            }
        }

        public class Source
        {
            public static Dictionary<string, object> environmentVariables = new Dictionary<string, object>
            {
                {"env_devmode", false},
                {"func_recompileOpenCLScript", false}
            }; 
        }

        public class EMath
        {
            public static RectangleF calculateRectangle(PointF start, PointF to)
            {
                RectangleF rect = new RectangleF();
                SizeF size = new SizeF();

                if (to.X < start.X & to.Y < start.Y)
                {
                    size = new SizeF(start.X - to.X, start.Y - to.Y);
                    rect = new RectangleF(to, size);
                }
                else if (to.X > start.X & to.Y > start.Y)
                {
                    size = new SizeF(to.X - start.X, to.Y - start.Y);
                    rect = new RectangleF(start, size);
                }
                else if (to.X < start.X & to.Y > start.Y)
                {
                    size = new SizeF(start.X - to.X, to.Y - start.Y);
                    rect = new RectangleF(new PointF(to.X, start.Y), size);
                }
                else if (to.X > start.X & to.Y < start.Y)
                {
                    size = new SizeF(to.X - start.X, start.Y - to.Y);
                    rect = new RectangleF(new PointF(start.X, to.Y), size);
                }

                return rect;
            }

            public static PointF midpoint(PointF a, PointF b)
            {
                PointF ret = new PointF(0, 0);
                ret.X = (a.X + b.X) / 2;
                ret.Y = (a.Y + b.Y) / 2;
                return ret;
            }

            public static int nearestMultiple(int value, int multiple)
            {
                int nearestMultiple =
                        (int)Math.Round(
                             (value / (double)multiple),
                             MidpointRounding.AwayFromZero
                         ) * multiple;
                return nearestMultiple;
            }

            public static PointF RotatePointF(PointF PointFToRotate, PointF centerPointF, double angleInDegrees)
            {
                double angleInRadians = angleInDegrees * (Math.PI / 180);
                double cosTheta = Math.Cos(angleInRadians);
                double sinTheta = Math.Sin(angleInRadians);
                return new PointF
                {
                    X =
                        (float)
                        (cosTheta * (PointFToRotate.X - centerPointF.X) -
                        sinTheta * (PointFToRotate.Y - centerPointF.Y) + centerPointF.X),
                    Y =
                        (float)
                        (sinTheta * (PointFToRotate.X - centerPointF.X) +
                        cosTheta * (PointFToRotate.Y - centerPointF.Y) + centerPointF.Y)
                };
            }
            public static PointF[] RotatePolygon(PointF[] poly, PointF center, double degrees)
            {
                int i = 0;
                PointF[] poly2 = new PointF[poly.Length];
                foreach (PointF PointF in poly)
                {
                    poly2[i] = RotatePointF(PointF, center, degrees);

                    i++;
                }
                return poly2;
            }
            public static PointF MoveInDirection(PointF _from, float distance, float direction)
            {
                double new_x = _from.X + distance * Math.Cos(direction * Math.PI / 180);
                double new_y = _from.Y + distance * Math.Sin(direction * Math.PI / 180);
                PointF value = new PointF((int)Math.Round(new_x), (int)Math.Round(new_y));
                return value;
            }

            public static PointF ToPoint(Vector2 vector2)
            {
                PointF pt = new PointF(
                    (int)(vector2.X + 0.5f), (int)(vector2.Y + 0.5f));

                return pt;
            }

            public static int ClosestToPointF(PointF[] points, PointF point, double maxDist)
            {
                double[] distances = new double[points.Length];
                int i = 0;
                foreach (PointF point2 in points)
                {
                    distances[i] = DistanceBetweenPointFs(point2, point);
                    i++;
                }

                int iter = -1;
                double lowestDistance = distances.Min();
                if (maxDist != -1f)
                {
                    if (lowestDistance < maxDist)
                    {
                        iter = distances.ToList().IndexOf(lowestDistance);
                    }
                    else
                    {
                        iter = -1;
                    }
                }
                else
                {
                    iter = distances.ToList().IndexOf(lowestDistance);
                }
                
                return iter;
            }

            public static PointF[] ChangePosition(PointF pos, double rot, PointF[] ply)
            {
                PointF center;
                PointF point = Centroid(ply);
                center = point;

                int i = 0;
                foreach (PointF p in ply)
                {
                    float minPosX = (float)Math.Sqrt((Math.Pow(center.X - pos.X, 2) + Math.Pow(0 - 0, 2)));
                    float minPosY = (float)Math.Sqrt((Math.Pow(center.Y - pos.Y, 2) + Math.Pow(0 - 0, 2)));
                    ply[i] = new PointF(ply[i].X + pos.X, ply[i].Y + pos.Y);
                    i++;
                }

                point = Centroid(ply);
                center = point;
                ply = RotatePolygon(ply, center, rot);

                return ply;
            }

            public static int isWithinRectIndex(PointF point, List<Rectangle> rects)
            {
                int index = -1;
                List<int> pointsWithin = new List<int>();

                int i = 0;
                foreach (RectangleF rect in rects)
                {
                    if (rect.Contains(point))
                    {
                        pointsWithin.Add(i);
                    }
                    i++;
                }
                if (pointsWithin.Count == 0)
                {
                    index = -1;
                }
                else
                {
                    index = pointsWithin[0];
                }

                return index;
            }

            public static PointF MoveToPointF(PointF _from, PointF _to, float distance)
            {
                const double Rad2Deg = -180.0 / Math.PI;
                float direction = (float)Math.Round(Math.Atan2(_from.Y - _to.Y, _to.X - _from.X) * Rad2Deg);
                double new_x = _from.X + distance * Math.Cos(direction * Math.PI / 180);
                double new_y = _from.Y + distance * Math.Sin(direction * Math.PI / 180);
                PointF value = new PointF((int)Math.Round(new_x), (int)Math.Round(new_y));
                return value;
            }
            public static float AngleBetweenPointFs(PointF _from, PointF _to)
            {
                float xDiff = _to.X - _from.X;
                float yDiff = _to.Y - _from.Y;
                float value = (float)Math.Round(Math.Atan2(yDiff, xDiff) * -180.0 / Math.PI);
                return value;
            }
            public static double DistanceBetweenPointFs(PointF _from, PointF _to)
            {
                float x1 = _from.X;
                float x2 = _to.X;

                float y1 = _from.Y;
                float y2 = _to.Y;
                return Math.Sqrt((Math.Pow(Math.Abs(x1 - x2), 2) + Math.Pow(Math.Abs(y1 - y2), 2)));
            }

            public static PointF Centroid(PointF[] points)
            {
                List<Vector2> path = new List<Vector2>();
                foreach (PointF point in points)
                {
                    path.Add(new Vector2(point.X, point.Y));
                }

                Vector2 result = path.Aggregate(Vector2.Zero, (current, point) => current + point);
                result /= path.Count;
                PointF result2 = ToPoint(result);

                return result2;
            }

            public static bool IsWithinCircle(PointF circleCenter, PointF circleOuter, PointF coordinate)
            {
                float rad = (float)Math.Sqrt((Math.Pow(circleCenter.X - circleOuter.X, 2) + Math.Pow(circleCenter.X - circleOuter.X - circleCenter.X - circleOuter.X, 2)));

                float x = coordinate.X;
                float y = coordinate.Y;

                float circle_x = circleCenter.X;
                float circle_y = circleCenter.Y;

                bool value;
                if ((x - circle_x) * (x - circle_x) + (y - circle_y) * (y - circle_y) <= rad * rad)
                {
                    value = true;
                }
                else
                {
                    value = false;
                }

                return value;
            }
        }
    }
}
