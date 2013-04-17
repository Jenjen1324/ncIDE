using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public class CsProject : Project
    {
        FileStructure.FileEntry rootDir;

        public void Save(string file)
        {
            throw new NotImplementedException();
        }

        public void Load(string file)
        {
            throw new NotImplementedException();
        }

        public FileStructure.FileEntry RootDir
        {
            get
            {
                return rootDir;
            }
            set
            {
                rootDir = value;
            }
        }
    }
}
