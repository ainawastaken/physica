using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
    }
}
