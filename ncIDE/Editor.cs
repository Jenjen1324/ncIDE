using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LineNumbers;

namespace ncIDE
{
    public partial class Editor : Form
    {
        public Editor()
        {
            InitializeComponent();
        }

        private void Editor_Load(object sender, EventArgs e)
        {
            UpdateTreeView();
        }

        private void UpdateTreeView()
        {
            treeView1.Nodes.Clear();
            treeView1.Nodes.Add(Program.project.RootDir.Node);
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(treeView1.SelectedNode.Tag is Projects.FileStructure.Directory)
            {
                Input i = new Input();
                i.Message = "Type a name for the file: ";
                i.ShowDialog();
                if(i.DialogResult == DialogResult.OK)
                {
                    string file = i.Value;
                    Projects.FileStructure.CodeFile cf = new Projects.FileStructure.CodeFile() { Name = file, Compile = true };
                    (treeView1.SelectedNode.Tag as Projects.FileStructure.Directory).SubEntries.Add(cf);
                    UpdateTreeView();
                }
            }
        }

        private void textFileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is Projects.FileStructure.Directory)
            {
                Input i = new Input();
                i.Message = "Type a name for the file: ";
                i.ShowDialog();
                if (i.DialogResult == DialogResult.OK)
                {
                    string file = i.Value;
                    Projects.FileStructure.TextFile cf = new Projects.FileStructure.TextFile() { Name = file };
                    (treeView1.SelectedNode.Tag as Projects.FileStructure.Directory).SubEntries.Add(cf);
                    UpdateTreeView();
                }
            }
        }

        private void directoryToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode.Tag is Projects.FileStructure.Directory)
            {
                Input i = new Input();
                i.Message = "Type a name for the folder: ";
                i.ShowDialog();
                if (i.DialogResult == DialogResult.OK)
                {
                    string file = i.Value;
                    Projects.FileStructure.Directory cf = new Projects.FileStructure.Directory() { Name = file, SubEntries = new List<Projects.FileStructure.FileEntry>() };
                    (treeView1.SelectedNode.Tag as Projects.FileStructure.Directory).SubEntries.Add(cf);
                    UpdateTreeView();
                }
            }
        }
    }
}
