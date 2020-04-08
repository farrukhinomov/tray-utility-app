using Common;
using System.IO;

namespace EU.DeleteOutputFolder
{
    [Utility(UtilityName)]
    public class DeleteOutputFolder : UtilityBase
    {
        const string UtilityName = "Delete output folder";
        const string DeletingFolderPath = @"C:\Repos\Eagle\Output"; //set folder to delete
        public override string Run()
        {
            if (Directory.Exists(DeletingFolderPath))
            {
                Directory.Delete(DeletingFolderPath, true);
                return "Output folder of the project has been deleted succesfully!";
            }
            else
                return "The directory doesn't exists or the path is not set correctly(in DeleteOutputFolder project).";
        }

        public override string Help()
        {
            return $"Deletes '{DeletingFolderPath}'";
        }
    }
}
