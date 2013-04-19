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
using System.Diagnostics;

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
                    Projects.FileStructure.CodeFile cf = new Projects.FileStructure.CodeFile() { Name = file, Parent = treeView1.SelectedNode.Tag as Projects.FileStructure.Directory, Compile = true };
                    cf.Path = cf.Parent.Path + "\\" + file;
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
                    tb.Name = tb.Text;
                    RichTextBox rt = new RichTextBox();
                    rt.Name = "textBox";
                    rt.AcceptsTab = true;
                    rt.Dock = DockStyle.Fill;
                    rt.Font = new System.Drawing.Font("Consolas", 14);
                    rt.ContextMenuStrip = this.editorMenu;
                    rt.KeyDown += rt_KeyCheck;
                    if (System.IO.File.Exists((e.Node.Tag as Projects.FileStructure.File).Path))
                    {
                        rt.Text = System.IO.File.ReadAllText((e.Node.Tag as Projects.FileStructure.File).Path);
                    }
                    tb.Controls.Add(rt);
                    tabControl1.TabPages.Add(tb);
                }
            }
        }

        void rt_KeyCheck(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && !e.Shift)
            {
                e.SuppressKeyPress = true;

                bool count = true;
                int i = ((RichTextBox)sender).GetFirstCharIndexOfCurrentLine();
                StringBuilder sb = new StringBuilder();
                while (count)
                {
                    if (i < ((RichTextBox)sender).Text.Length)
                    {
                        count = ((RichTextBox)sender).Text[i] == '\t';
                        i++;
                        if (count)
                        {
                            sb.Append("\t");
                        }
                    }
                    else
                    {
                        count = false;
                    }
                }

                ((RichTextBox)sender).SelectedText = "\n" + sb.ToString();
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (tabControl1.SelectedTab != null)
            {
                (tabControl1.SelectedTab.Tag as Projects.FileStructure.File).Data = tabControl1.SelectedTab.Controls[0].Text;
                (tabControl1.SelectedTab.Tag as Projects.FileStructure.File).Save();
                textBox1.Text = String.Format("File {0} saved!", (tabControl1.SelectedTab.Tag as Projects.FileStructure.File).Name);
            }
        }

        private void compileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompilerErrorCollection errors = Program.project.Compile();
            textBox1.Clear();
            textBox1.AppendText("Compile results: \r\n");
            if (errors.Count <= 0)
            {
                textBox1.AppendText("No Errors! :)");
            }
            else
            {
                foreach (CompilerError error in errors)
                {
                    textBox1.AppendText(error.ToString() + "\r\n");
                }
            }
        }

        private void saveProjectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            Program.project.Save();
            textBox1.Text = String.Format("Project {0} saved!", Program.project.Name);
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

        private void runToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Process.Start(Program.project.RootDir.Path + "\\" + Program.project.Name + ".exe");
        }

        private void closeThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Remove(tabControl1.SelectedTab);
        }

        private void closeAllButThisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            foreach (TabPage tab in tabControl1.TabPages)
            {
                if (tab != tabControl1.SelectedTab) { tabControl1.TabPages.Remove(tab); }
            }
        }

        private void closeAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tabControl1.TabPages.Clear();
        }
    }
}
