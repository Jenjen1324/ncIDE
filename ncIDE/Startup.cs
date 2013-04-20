using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ncIDE
{
    public partial class Startup : Form
    {
        public Startup()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Editor ed = new Editor();
            ed.Show();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Input prName = new Input();
            prName.ShowDialog();
            if (prName.Value != "")
            {
                Projects.CsProject proj = new Projects.CsProject();
                proj.Name = prName.Value;
                proj.Referances = new string[] { "System.dll", "System.Windows.Forms.dll" };
                string dirname = Program.projectsFolder + "\\" + prName.Value;
                System.IO.Directory.CreateDirectory(dirname);
                proj.RootDir = new Projects.FileStructure.Directory() { Name = prName.Value , Path = dirname, SubEntries = new List<Projects.FileStructure.FileEntry>(), Parent = null };
                proj.Save();
                Program.project = proj;
                Editor ed = new Editor();
                ed.Show();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog opf = new OpenFileDialog() {
                Filter = "Project data files|project.xml",
                InitialDirectory = Program.projectsFolder,
                Multiselect = false,
                CheckFileExists = true,
                CheckPathExists = true,
                DefaultExt = ".xml"
            };
            opf.ShowDialog();
            if (opf.FileName != "")
            {
                Program.project = new Projects.CsProject();
                Program.project.Load(opf.FileName);
                Editor ed = new Editor();
                ed.Show();
            }
        }
    }
}
