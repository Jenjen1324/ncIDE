using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ncIDE
{
    static class Program
    {
        public static string projectsFolder;

        public static Projects.Project project;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {


            CheckStartupDirectories();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startup());
        }

        private static void CheckStartupDirectories()
        {
        }
    }
}
