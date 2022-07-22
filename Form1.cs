using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace physica
{
    public partial class editorWindow : Form
    {
        bool mouseOnCanvas = false;
        PointF mouseLocation;

        public editorWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var property = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
            property.SetValue(canvas, true, null);
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
            if (mouseOnCanvas)
            {
                e.Graphics.DrawLine(Pens.Black, mouseLocation.X - 5, mouseLocation.Y, mouseLocation.X + 5, mouseLocation.Y);
                e.Graphics.DrawLine(Pens.Black, mouseLocation.X, mouseLocation.Y - 5, mouseLocation.X, mouseLocation.Y + 5);
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
            GC.Collect();
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {

        }

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            canvas.Refresh();
            GC.Collect();
        }
    }
}
