<<<<<<< HEAD
﻿using System;
=======
<<<<<<< HEAD
using System;
=======
﻿using System;
>>>>>>> update
>>>>>>> d9ad147e48472f870598d6d9775255c96967d839
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
<<<<<<< HEAD
=======
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
>>>>>>> d9ad147e48472f870598d6d9775255c96967d839
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
<<<<<<< HEAD
=======
>>>>>>> update
>>>>>>> d9ad147e48472f870598d6d9775255c96967d839
}
