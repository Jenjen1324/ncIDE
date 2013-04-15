using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCompiler
{
    public class CsCompiler
    {
        CSharpCodeProvider csc = new CSharpCodeProvider(new Dictionary<string, string>() { { "CompilerVersion", "v3.5" } });

        public string[] Referances { get; set; }
        public string Name { get;set; }
        public string[] Files { get; set; }

        public CompilerErrorCollection Errors { get; set; }

        public void Compile()
        {
            var pars = new CompilerParameters(Referances, Name, true);
            pars.GenerateExecutable = true;
            CompilerResults result = csc.CompileAssemblyFromFile(pars, Files);
            Errors = result.Errors;
        }

    }
}
