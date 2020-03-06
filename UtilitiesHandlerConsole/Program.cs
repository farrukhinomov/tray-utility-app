using Common;
using System;
using System.IO;
using System.Reflection;

namespace TestProject
{
    class Program
    {
        private static string _outputFolderPath { get; set; } = System.IO.Directory.GetCurrentDirectory();
        //public string _outputFilePath { get; set; }
        static void Main(string[] args)
        {
            DirectoryInfo extensionsDirectory = new DirectoryInfo(_outputFolderPath);
            var extensionsFiles = extensionsDirectory.GetFiles("EU.*.dll", SearchOption.AllDirectories);
            foreach (var file in extensionsFiles)
            {
                Assembly assembly = Assembly.LoadFile(file.ToString());
                var types = assembly.GetTypes();

                foreach (var type in types)
                {
                    var attr = type.GetCustomAttribute<UtilityAttribute>();
                    if (attr != null)
                    {
                        var utilName = attr.Name;

                        var method = type.GetMethod("Run", BindingFlags.Public | BindingFlags.Instance);
                        if (method != null)
                        {
                            var typeInstance = Activator.CreateInstance(type);
                            var obj = method.Invoke(typeInstance, null);
                            Console.WriteLine(obj as string);
                        }
                    }
                }
            }

        }
    }


}
