using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using physica.editor;
using System.Windows.Forms;

namespace physica
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 
        public static console.Console c;
        [STAThread]

        

        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(true);
            /*using (mgPreview.Game1 game = new mgPreview.Game1())
            {
                //game.Run();
            }*/

            editorWindow w = new editorWindow();
            if (Environment.GetCommandLineArgs().Length >= 2 & Environment.GetCommandLineArgs()[1] == "-dev")
            {
                c = new console.Console();
                c.print(string.Join(", ", Environment.GetCommandLineArgs()));
                c.Show();
            }
            Application.Run(w);
        }
    }
}
