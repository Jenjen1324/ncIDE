using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Projects
{
    public class FileStructure
    {
        public abstract class FileEntry
        {
            public abstract TreeNode Node { get; }
        }

        public class File : FileEntry
        {
            public string Name { set; get; }

            public override TreeNode Node
            {
                get
                {
                    return new TreeNode(Name) { Tag = this, ToolTipText = "File: " + Name };
                }
            }
        }

        public class Directory : FileEntry
        {
            public string Name { set; get; }
            public List<FileEntry> SubEntries { set; get; }

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
