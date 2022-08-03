using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Numerics;
using System.Threading.Tasks;

namespace physica.engine
{
    public class physicaEngine
    {
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

            public static void DrawTool(PaintEventArgs e, PointF mousePos, PointF mouseStart, bool mouseDown, int tool)
            {
                if (tool == SellectTool)
                {
                    e.Graphics.DrawString(names[tool], SystemFonts.DefaultFont, Brushes.Black, mousePos);

                    if (mouseDown)
                    {
                        SizeF size = new SizeF(0, 10);
                        RectangleF rect = new RectangleF(mousePos, size);
                        e.Graphics.DrawRectangle(Pens.Red, Rectangle.Round(rect));

                        size = new SizeF(Math.Abs(mousePos.X - mouseStart.X), Math.Abs(mousePos.Y - mouseStart.Y));
                        rect = new RectangleF(mouseStart, size);
                        e.Graphics.DrawRectangle(Pens.Black, Rectangle.Round(rect));

                        e.Graphics.DrawString($"{rect.Width}", SystemFonts.DefaultFont, Brushes.Black, new PointF((rect.Size.Width/2)+rect.X, rect.Y));
                        e.Graphics.DrawString($"{rect.Height}", SystemFonts.DefaultFont, Brushes.Black, new PointF(rect.X, (rect.Size.Height/2)+rect.Y));
                        e.Graphics.DrawString($"Area:{rect.Width*rect.Height}", SystemFonts.DefaultFont, Brushes.Black, new PointF(rect.X, rect.Y-15));
                    }
                }
                else if (tool == DistanceTool)
                {
                    e.Graphics.DrawString(names[tool], SystemFonts.DefaultFont, Brushes.Black, mousePos);

                    if (mouseDown)
                    {
                        double distance = 0;
                        e.Graphics.DrawLine(Pens.Red, mousePos, mouseStart);

                        SizeF size = new SizeF(0, -5);
                        SizeF size2 = new SizeF(0, 5);
                        e.Graphics.DrawLine(Pens.Black, PointF.Subtract(mouseStart, size), PointF.Subtract(mouseStart, size2));
                        size = new SizeF(-5, 0);
                        size2 = new SizeF(5, 0);
                        e.Graphics.DrawLine(Pens.Black, PointF.Subtract(mouseStart, size), PointF.Subtract(mouseStart, size2));

                        distance = Math.Round(EMath.DistanceBetweenPointFs(mousePos, mouseStart),1);
                        PointF pnt = EMath.midpoint(mouseStart,mousePos);
                        e.Graphics.DrawString($"Distance:{distance}", SystemFonts.DefaultFont, Brushes.DarkRed, pnt);

                        e.Graphics.DrawString($"X:{mouseStart.X} Y:{mouseStart.Y}", SystemFonts.DefaultFont, Brushes.DarkRed,mouseStart);
                        size = new SizeF(0, -10);
                        e.Graphics.DrawString($"X:{mousePos.X} Y:{mousePos.Y}", SystemFonts.DefaultFont, Brushes.DarkRed, PointF.Add(mousePos,size));
                    }
                }
            }
        }

        public class Objects
        {
            public class StaticHitbox
            {
                PointF location;
                RectangleF hitBox;
                PointF centroid;

                public StaticHitbox create(PointF pos, RectangleF rect)
                {
                    StaticHitbox obj = new StaticHitbox();
                    obj.location = pos;
                    obj.hitBox = rect;
                    obj.centroid = new PointF(rect.Left + rect.Width / 2,rect.Top + rect.Height / 2);
                    return obj;
                }
                public StaticHitbox move(PointF pos, StaticHitbox obj)
                {
                    obj.location = pos;
                    obj.centroid = new PointF(obj.hitBox.Left + obj.hitBox.Width / 2, obj.hitBox.Top + obj.hitBox.Height / 2);
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
            public static PointF midpoint(PointF a, PointF b)
            {
                PointF ret = new PointF(0, 0);
                ret.X = (a.X + b.X) / 2;
                ret.Y = (a.Y + b.Y) / 2;
                return ret;
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
                List<Vector2> v2Points = new List<Vector2>();
                foreach (PointF p in ply)
                {
                    v2Points.Add(new Vector2(p.X, p.Y));
                }
                PointF point = ToPoint(Centroid(v2Points));
                center = point;


                int i = 0;
                foreach (PointF p in ply)
                {
                    float minPosX = (float)Math.Sqrt((Math.Pow(center.X - pos.X, 2) + Math.Pow(0 - 0, 2)));
                    float minPosY = (float)Math.Sqrt((Math.Pow(center.Y - pos.Y, 2) + Math.Pow(0 - 0, 2)));
                    ply[i] = new PointF(ply[i].X + pos.X, ply[i].Y + pos.Y);
                    i++;
                }
                v2Points = new List<Vector2>();
                foreach (PointF p in ply)
                {
                    v2Points.Add(new Vector2(p.X, p.Y));
                }
                point = ToPoint(Centroid(v2Points));
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

            public static Vector2 Centroid(List<Vector2> path)
            {
                Vector2 result = path.Aggregate(Vector2.Zero, (current, point) => current + point);
                result /= path.Count;

                return result;
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
