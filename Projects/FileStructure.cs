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

            public abstract void Save();
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
        }

        public class TextFile : File
        {
        }

        public class ProjectDataFile : File
        {
        }
    }
}
