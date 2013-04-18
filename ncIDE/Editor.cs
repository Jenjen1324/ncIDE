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
using System.CodeDom.Compiler;

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
            if(treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is Projects.FileStructure.Directory)
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
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is Projects.FileStructure.Directory)
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
            if (treeView1.SelectedNode != null && treeView1.SelectedNode.Tag is Projects.FileStructure.Directory)
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

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Tag is Projects.FileStructure.File)
            {
                if (!tabControl1.Controls.ContainsKey((e.Node.Tag as Projects.FileStructure.File).Name))
                {
                    TabPage tb = new TabPage((e.Node.Tag as Projects.FileStructure.File).Name);
                    tb.Tag = e.Node.Tag;
                    RichTextBox rt = new RichTextBox();
                    rt.Name = "textBox";
                    rt.AcceptsTab = true;
                    rt.Dock = DockStyle.Fill;
                    rt.Font = new System.Drawing.Font("Consolas", 14);
                    rt.ContextMenuStrip = this.editorMenu;
                    if (System.IO.File.Exists((e.Node.Tag as Projects.FileStructure.File).Path))
                    {
                        rt.Text = System.IO.File.ReadAllText((e.Node.Tag as Projects.FileStructure.File).Path);
                    }
                    tb.Controls.Add(rt);
                    tabControl1.TabPages.Add(tb);
                }
            }
        }

        private void saveProjectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Program.project.RootDir.Save();
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            (tabControl1.SelectedTab.Tag as Projects.FileStructure.File).Data = tabControl1.SelectedTab.Controls[0].Text;
            (tabControl1.SelectedTab.Tag as Projects.FileStructure.File).Save();
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompilerErrorCollection errors = Program.project.Compile();
            textBox1.Clear();
            textBox1.AppendText("===========BUILD=========");
            foreach (CompilerError error in errors)
            {
                textBox1.AppendText(error.ToString() + "\r\n");
            }
        }

        private void saveProjectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Program.project.Save();
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right)
            {
                compileCombo.Visible = e.Node.Tag is Projects.FileStructure.CodeFile;
                if (e.Node.Tag is Projects.FileStructure.CodeFile)
                {
                    compileCombo.SelectedIndex = ((e.Node.Tag as Projects.FileStructure.CodeFile).Compile ? 0 : 1);
                }
            }
        }

        private void compileCombo_Click(object sender, EventArgs e)
        {
            if (treeView1.SelectedNode != null)
            {
                (treeView1.SelectedNode.Tag as Projects.FileStructure.CodeFile).Compile = (compileCombo.SelectedIndex == 0);
                UpdateTreeView();
            }
        }
    }
}
