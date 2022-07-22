using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace physica
{
    public partial class editorWindow : Form
    {
        public editorWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
    }
}
