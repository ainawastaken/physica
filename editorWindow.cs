using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Numerics;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading;
using physica.engine;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace physica.editor
{
    public partial class editorWindow : Form
    {
        bool mouseOnCanvas = false;
        PointF trueMouseLocation;
        PointF trueMouseStart;
        PointF mouseLocation;
        PointF mouseStart;
        int sellectedTool = 0;
        bool mouseDown;

        PointF[] gridPoints = new PointF[1048576];
        PointF closestPoint;

        int gridMul = 32;
        bool gridEnable = false;
        volatile bool gridSnapEnable = false;

        Thread ct;


        public editorWindow()
        {
            InitializeComponent();
        }

        void ComputeThread()
        {
            while (true)
            {
                CalculateGrid(ref gridPoints);
                if (gridSnapEnable)
                {
                    closestPoint = CalculateClosestPoint(mouseLocation, gridPoints);
                    trueMouseLocation = closestPoint;
                }
                else
                {
                    trueMouseLocation = mouseLocation;
                }
            }
        }

        void CalculateGrid(ref PointF[] gridVar)
        {
            int i = 0;
            for (int x = 0; x < canvas.Width; x+=gridMul)
            {
                for (int y = 0; y < canvas.Height; y+=gridMul)
                {
                    try
                    {
                        gridVar[i] = new PointF(x, y);
                        i++;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, i.ToString());
                    }
                }
            }
        }
        PointF CalculateClosestPoint(PointF point, PointF[] points)
        {
            List<float> dist = new List<float>();
            foreach (PointF point2 in points)
            {
                dist.Add(Vector2.Distance(new Vector2(point2.X,point2.Y),new Vector2(point.X,point.Y)));
            }

            return points[dist.IndexOf(dist.Min())];
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            property.SetValue(canvas, true, null);
            CalculateGrid(ref gridPoints);
            ct = new Thread(ComputeThread);
            ct.Start();
        }

        private void mapToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void editorWindow_Resize(object sender, EventArgs e)
        {
            entitiesBox.Height = (this.Height / 2) - menuStrip1.Height;

            placedEntitiesBox.Height = (this.Height / 2) - menuStrip1.Height - 18;
            placedEntitiesBox.Location = new Point(0,(this.Height / 2) - menuStrip1.Height);

            offsetPanel.Location = new Point(tabPage1.Width-77, 3);
        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            if (gridEnable)
            {
                for (int x = 0; x < canvas.Width; x += gridMul)
                {
                    e.Graphics.DrawLine(Pens.DarkGray, x, 0, x, canvas.Height);
                }
                for (int y = 0; y < canvas.Height; y += gridMul)
                {
                    e.Graphics.DrawLine(Pens.DarkGray, 0, y, canvas.Width, y);
                }
            }

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            if (mouseOnCanvas)
            {
                e.Graphics.DrawLine(Pens.Black, mouseLocation.X - 5, mouseLocation.Y, mouseLocation.X + 5, mouseLocation.Y);
                e.Graphics.DrawLine(Pens.Black, mouseLocation.X, mouseLocation.Y - 5, mouseLocation.X, mouseLocation.Y + 5);
                if (gridSnapEnable)
                {
                    e.Graphics.DrawLine(Pens.Red, mouseLocation, closestPoint);
                    e.Graphics.DrawLine(Pens.DarkBlue, closestPoint.X - 2.5f, closestPoint.Y - 2.5f,closestPoint.X+2.5f,closestPoint.Y+2.5f);
                    e.Graphics.DrawLine(Pens.DarkBlue, closestPoint.X + 2.5f, closestPoint.Y - 2.5f, closestPoint.X - 2.5f, closestPoint.Y + 2.5f);
                }
                

                physicaEngine.Tools.DrawTool(e,trueMouseLocation,trueMouseStart,mouseDown,0);
            }
            
        }

        private void canvas_MouseEnter(object sender, EventArgs e)
        {
            mouseOnCanvas = true;
            Cursor.Hide();
        }

        private void canvas_MouseLeave(object sender, EventArgs e)
        {
            mouseOnCanvas = false;   
            Cursor.Show();
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLocation = e.Location;
            canvas.Refresh();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = true;
            }
            trueMouseStart = e.Location;
            if (gridSnapEnable)
            {
                trueMouseStart = closestPoint;
            }
        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                mouseDown = false;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();
        }

        private void canvas_Click(object sender, EventArgs e)
        {
        }



        private void gridMultiplierPlus_Click(object sender, EventArgs e)
        {
            gridMul += 2;
        }

        private void gridMultiplierMin_Click(object sender, EventArgs e)
        {
            gridMul -= 2;
        }

        private void gridToggle1_Click(object sender, EventArgs e)
        {
            gridEnable = !gridEnable;
        }

        private void gridSnapToggle1_Click(object sender, EventArgs e)
        {
            gridSnapEnable = !gridSnapEnable;
        }

        private void editorWindow_FormClosed(object sender, FormClosedEventArgs e)
        {
            ct.Abort();
        }
    }
}
