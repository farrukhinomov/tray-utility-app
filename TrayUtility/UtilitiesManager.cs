using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesHandler
{
    public class UtilitiesManager
    {
        public string OutputFolder { get; private set; } = System.IO.Directory.GetCurrentDirectory();
        public List<Utility> Utilities { get; private set; } = new List<Utility>();

        public UtilitiesManager()
        {

        }
        public void LoadUtilities()
        {
            foreach (var file in GetUtilitiesFiles())
                foreach (var type in GetFileTypes(file.FullName))
                    Utilities.Add(new Utility(type));
        }

        private Type[] GetFileTypes(string fileFullPath)
        {
            Assembly assembly = Assembly.LoadFile(fileFullPath);
            return assembly.GetTypes();
        }

        private FileInfo[] GetUtilitiesFiles()
        {
            DirectoryInfo utilitiesDirectory = new DirectoryInfo(OutputFolder);
            return utilitiesDirectory.GetFiles("EU.*.dll", SearchOption.AllDirectories);
        }

    }
}
