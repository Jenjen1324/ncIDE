using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public interface Project
    {
        void Save();

        void Load(string file);

        FileStructure.FileEntry RootDir { get; set; }

        CompilerErrorCollection Compile();
    }
}
