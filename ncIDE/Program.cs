using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ncIDE
{
    static class Program
    {
        public static Projects.Project project;
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            project = new Projects.CsProject() { Name = "main", Referances = new string[] { "System.dll" }};
            string rdir =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ncIDE";
            string dir = rdir + "\\root";
            if (!System.IO.Directory.Exists(rdir)) { System.IO.Directory.CreateDirectory(rdir); }
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            project.Load(dir + "\\project.xml");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startup());
        }
    }
}
