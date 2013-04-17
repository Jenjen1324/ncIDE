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
            project = new Projects.CsProject();
            string rdir =  Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\ncIDE";
            string dir = rdir + "\\root";
            if (!System.IO.Directory.Exists(rdir)) { System.IO.Directory.CreateDirectory(rdir); }
            if (!System.IO.Directory.Exists(dir))
            {
                System.IO.Directory.CreateDirectory(dir);
            }
            project.RootDir = new Projects.FileStructure.Directory() { Name = "Root", Path = dir, SubEntries = new List<Projects.FileStructure.FileEntry>() };
            (project.RootDir as Projects.FileStructure.Directory).SubEntries.Add(
                    new Projects.FileStructure.CodeFile() { Name = "main.cs", Path = dir + "\\main.cs" , Compile = true }
                );
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Startup());
        }
    }
}
