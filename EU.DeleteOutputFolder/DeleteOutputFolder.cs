using Common;
using System.IO;
using System;

namespace EU.DeleteOutputFolder
{
    [Utility(UtilityName)]
    public class DeleteOutputFolder : UtilityBase
    {
        const string UtilityName = "Delete output folder";
        const string DeletingFolderPath = @"C:\Repos\Output";
        public override string Run()
        {
            // If directory does not exist, don't even try
            if (Directory.Exists(DeletingFolderPath))
            {
                Directory.Delete(DeletingFolderPath, true);
                return "Output folder of the project has been deleted succesfully!";
            }
            else
                return "The directory doesn't exists or the path is not set correctly";
        }

        public override string Help()
        {
            return $"Deletes '{DeletingFolderPath}'";
        }
    }
}
