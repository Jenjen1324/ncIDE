using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projects
{
    public class Project
    {
        virtual public void Save(string file);

        virtual public static Project Load(string file);
    }
}
