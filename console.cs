using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using physica;
using physica.engine;

namespace physica.console
{
    public partial class Console : Form
    {
        public Console()
        {
            InitializeComponent();
        }

        long num = 1;
        public void print(string txt = "undefined")
        {
            this.richTextBox1.AppendText($"{num}> {txt}\n");
            this.richTextBox1.ScrollToCaret();
            num++;
        }

        private void Console_Load(object sender, EventArgs e)
        {
            foreach (var objec in engine.physicaEngine.Source.environmentVariables)
            {
                comboBox1.Items.Add(objec.Key);
            }
            
        }

        private void comboBox1_SelectedValueChanged(object sender, EventArgs e)
        {
            typeOfLabel.Text = $"Type: {engine.physicaEngine.Source.environmentVariables.ToArray()[comboBox1.SelectedIndex].Value.GetType()}";
            valueLbl.Text = $"Value: {engine.physicaEngine.Source.environmentVariables.ToArray()[comboBox1.SelectedIndex].Value}";
        }
    }
}
