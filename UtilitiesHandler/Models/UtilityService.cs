using Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UtilitiesHandler
{
    public class UtilityService : IUtilityService
    {
        private readonly string OutputFolderPath;
        private readonly string JsonFileDirectoryPath;
        private readonly string JsonFileName;

        public UtilityService()
        {
            OutputFolderPath = Directory.GetCurrentDirectory();

            JsonFileDirectoryPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), Process.GetCurrentProcess().MainModule.FileName.Split('\\').Last().Split('.').First());
            JsonFileName = "DisabledUtilities.json";
        }

        public IEnumerable<Utility> GetUtilities()
        {
            var utilities = new List<Utility>();
            foreach (var file in GetUtilitiesFiles())
                foreach (var type in GetFileTypes(file.FullName))
                    if (type.GetCustomAttribute<UtilityAttribute>() != null)
                        utilities.Add(new Utility(type));
            return ReadDisabledUtilitiesNameFromFile(utilities);
        }

        public void SaveDisabledUtilitiesNameToFile(IEnumerable<Utility> utilities)
        {
            if (!Directory.Exists(JsonFileDirectoryPath))
                Directory.CreateDirectory(JsonFileDirectoryPath);

            File.WriteAllText($"{JsonFileDirectoryPath}\\{JsonFileName}", JsonConvert.SerializeObject(utilities.Where(item => item.Enabled == false).Select(item => item.Name)));
        }

        public IEnumerable<Utility> ReadDisabledUtilitiesNameFromFile(List<Utility> utilities)
        {
            if (!Directory.Exists(JsonFileDirectoryPath))
                Directory.CreateDirectory(JsonFileDirectoryPath);

            string file = "";
            if (File.Exists($"{JsonFileDirectoryPath}\\{JsonFileName}"))
                try
                {
                    file = File.ReadAllText($"{JsonFileDirectoryPath}\\{JsonFileName}");
                }
                finally
                {
                    if (file != "" || file != null)
                    {
                        var utlitiesNamesFromFile = JsonConvert.DeserializeObject<IEnumerable<string>>(file);

                        foreach (var fileItem in utlitiesNamesFromFile)
                            foreach (var item in utilities.ToList())
                                if (fileItem == item.Name) item.Enabled = false;
                    }
                }
            else
                SaveDisabledUtilitiesNameToFile(utilities);
            return utilities;
        }

        private Type[] GetFileTypes(string fileFullPath)
        {
            Assembly assembly = Assembly.LoadFile(fileFullPath);
            return assembly.GetTypes();
        }

        private FileInfo[] GetUtilitiesFiles()
        {
            DirectoryInfo utilitiesDirectory = new DirectoryInfo(OutputFolderPath);
            return utilitiesDirectory.GetFiles("EU.*.dll", SearchOption.AllDirectories);
        }
    }
}
