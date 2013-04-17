<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> update
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
<<<<<<< HEAD
  public class CsProject : Project
  {
    string name;
    
    public override void Save(string file)
    {
        
    }
    
    public static override Project Load(string file)
    {
      
    }
  }
=======
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
>>>>>>> update
}
