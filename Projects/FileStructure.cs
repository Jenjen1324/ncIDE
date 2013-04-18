using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Projects
{
    public class FileStructure
    {
        public abstract class FileEntry
        {
            public abstract TreeNode Node { get; }

            public abstract string Path { get; set; }

            public FileEntry Parent;

            public abstract void Save();

            internal abstract string[] GetFilePaths();

            public abstract string Xml { get; }
        }

        public class File : FileEntry
        {
            public string Name { set; get; }

            public override string Path { get; set; }

            public string Data { get; set; }

            public override TreeNode Node
            {
                get
                {
                    return new TreeNode(Name) { Tag = this, ToolTipText = "File: " + Name };
                }
            }

            public override void Save()
            {
                System.IO.File.WriteAllText(Path,Data);
            }

            internal override string[] GetFilePaths()
            {
                return new string[] { Path };
            }

            public override string Xml
            {
                get
                {
                    return "<file name=\"" + Name + "\" path=\"" + Path + "\" />";
                }
            }
        }

        public class Directory : FileEntry
        {
            public string Name { set; get; }
            public List<FileEntry> SubEntries { set; get; }

            public override string Path { get; set; }

            public override TreeNode Node
            {
                get
                {
                    TreeNode node = new TreeNode(Name) { Tag = this, ToolTipText = "Directory: " + Name };
                    foreach (FileEntry sub in SubEntries)
                    {
                        node.Nodes.Add(sub.Node);
                    }
                    return node;
                }
            }

            public override void Save()
            {
                if (!System.IO.Directory.Exists(Path))
                {
                    System.IO.Directory.CreateDirectory(Path);
                }
                foreach (FileEntry subs in SubEntries)
                {
                    subs.Save();
                }
            }

            internal override string[] GetFilePaths()
            {
                List<string> subs = new List<string>();
                foreach (FileEntry sfile in SubEntries)
                {
                    subs.AddRange(sfile.GetFilePaths());
                }
                return subs.ToArray();
            }

            public override string Xml
            {
                get
                {
                    StringBuilder strb = new StringBuilder();
                    strb.AppendLine("<directory name=\"" + Name + "\" path=\"" + Path + "\">");
                    foreach (FileEntry sub in SubEntries)
                    {
                        strb.AppendLine(sub.Xml);
                    }
                    strb.AppendLine("</directory>");
                    return strb.ToString();
                }
            }
        }


        public class CodeFile : File
        {
            public bool Compile { set; get; }

            public override TreeNode Node
            {
                get
                {
                    TreeNode n = base.Node;
                    n.ToolTipText = "Code File: " + Name + "\r\nCompile? " + (Compile ? "Yes" : "No");
                    return n;
                }
            }

            public override string Xml
            {
                get
                {
                    return "<cfile name=\"" + Name + "\" path=\"" + Path + "\" compile=\"" + Compile.ToString() + "\" />";
                }
            }

            internal override string[] GetFilePaths()
            {
                if (Compile)
                {
                    return new string[] { Path };
                }
                else
                {
                    return new string[0];
                }
            }
        }

        public class TextFile : File
        {
        }

        public class ProjectDataFile : File
        {
        }
    }
}
