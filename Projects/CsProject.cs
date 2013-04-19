using NetCompiler;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Projects
{
    public class CsProject : Project
    {
        public string Name { get; set; }
        FileStructure.FileEntry rootDir;

        public void Save()
        {
            StringBuilder xmlb = new StringBuilder();
            xmlb.AppendLine("<project>");
            xmlb.AppendLine("<name>" + Name + "</name>");
            xmlb.AppendLine("<referances>");
            foreach (string referance in Referances)
            {
                xmlb.AppendLine("<referance>" + referance + "</referance>");
            }
            xmlb.AppendLine("</referances>");

            xmlb.AppendLine("<files>");
            xmlb.AppendLine(rootDir.Xml);
            xmlb.AppendLine("</files>");
            xmlb.AppendLine("</project>");

            System.IO.File.WriteAllText(rootDir.Path + "\\project.xml",xmlb.ToString());
        }

        public void Load(string file)
        {
            System.IO.FileStream stream = new System.IO.FileStream(file, System.IO.FileMode.Open);
            XmlReader xmlr = XmlReader.Create(stream);
            List<string> currefs = new List<string>();
            FileStructure.Directory curdir = null;
            while (xmlr.Read())
            {
                if (xmlr.Name == "name" && xmlr.NodeType == XmlNodeType.Element)
                {
                    Name = xmlr.ReadElementContentAsString();
                }
                if (xmlr.Name == "referances" && xmlr.NodeType == XmlNodeType.Element)
                {
                }
                if (xmlr.Name == "referance" && xmlr.NodeType == XmlNodeType.Element)
                {
                    currefs.Add(xmlr.ReadElementContentAsString());
                }
                if (xmlr.Name == "files" && xmlr.NodeType == XmlNodeType.Element)
                {
                }
                if (xmlr.Name == "directory" && xmlr.NodeType == XmlNodeType.Element)
                {
                    var dir = new FileStructure.Directory() { Name = xmlr.GetAttribute("name"), Path = xmlr.GetAttribute("path"), SubEntries = new List<FileStructure.FileEntry>() };
                    if (curdir != null)
                    {
                        curdir.SubEntries.Add(dir);
                        dir.Parent = curdir;
                    }
                    curdir = dir;
                }
                if (xmlr.Name == "directory" & xmlr.NodeType == XmlNodeType.EndElement)
                {
                    if (curdir.Parent != null)
                    {
                        curdir = curdir.Parent as FileStructure.Directory;
                    }
                }
                if (xmlr.Name == "file" && xmlr.NodeType == XmlNodeType.Element)
                {
                    curdir.SubEntries.Add(new FileStructure.File() { Name = xmlr.GetAttribute("name"), Path = xmlr.GetAttribute("path") });
                }
                if (xmlr.Name == "cfile" && xmlr.NodeType == XmlNodeType.Element)
                {
                    curdir.SubEntries.Add(new FileStructure.CodeFile() { Name = xmlr.GetAttribute("name"), Path = xmlr.GetAttribute("path"), Compile = Convert.ToBoolean(xmlr.GetAttribute("compile"))});
                }
            }
            rootDir = curdir;
            xmlr.Close();
            stream.Close();
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


        public CompilerErrorCollection Compile()
        {
            string[] filepaths = rootDir.GetFilePaths();
            CsCompiler csc = new CsCompiler();
            csc.Files = filepaths;
            csc.Name = this.rootDir.Path + "\\" + this.Name.Replace(" ","_") + ".exe";
            csc.Referances = this.Referances;
            csc.Compile();
            return csc.Errors;
        }

        public string[] Referances { get; set; }
    }
}
