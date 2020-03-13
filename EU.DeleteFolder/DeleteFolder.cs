using Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace EU.DeleteFolder
{
    [Utility("Delete Output")]
    public class DeleteFolder : UtilityBase
    {
        public static string dirName = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\DeleteOutput";

        public override string Help()
        {
            return $"Deletes the folder that is specified in the config file. Path: {dirName}\\config.txt";
        }

        public override string Run()
        {

            var dir = new DirectoryInfo(dirName);
            if (!dir.Exists)
            {
                dir.Create();
            }
            FileInfo file = new FileInfo(dir.FullName + "\\config.txt");
            if (!file.Exists)
            {
                var sw = File.CreateText(file.FullName);
                sw.Close();
            }
        comeHere: var possiblePath = File.ReadAllText(file.FullName).Trim();
            if (string.IsNullOrEmpty(possiblePath))
            {
                var result = MessageBox.Show("Path to delete the folder is not specified. Would you like to set it?", "Path is not set", MessageBoxButton.YesNo, MessageBoxImage.Exclamation);
                if (result == MessageBoxResult.Yes)
                {
                    var x = new Settings();
                    x.ShowDialog();
                    goto comeHere;
                }
                else
                {
                    return $"Path is not specified in the config file. Please go to {file.FullName} and specify the path to the folder you like to delete";
                }
            }
            DirectoryInfo dirToDelete = new DirectoryInfo(possiblePath);
            if (dirToDelete.Exists)
            {
                string status = "Folder \"" + possiblePath + "\" deleted successfully!";
                try
                {
                    dirToDelete.Delete(true);
                }
                catch (Exception ex)
                {
                    status = $"There was a problem with deleting the folder:{possiblePath}. Exception: {ex.Message}";
                }
                return status;
            }
            else
            {
                return $"Folder {possiblePath} is either doesn't exist or already deleted.";
            }
        }
    }
}
