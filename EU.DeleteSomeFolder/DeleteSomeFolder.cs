using Common;
using System.IO;

namespace EU.DeleteSomeFolder
{
    [Utility(UtilityName)]
    public class DeleteSomeFolder : UtilityBase
    {
        const string UtilityName = "Delete some folder";
        const string DeletingFolderPath = @"C:\SomeFolder";
        public override string Run()
        {
            if (Directory.Exists(DeletingFolderPath))
            {
                Directory.Delete(DeletingFolderPath, true);
                return "'SomeFolder' has been deleted succesfully!";
            }
            else
                return "The directory doesn't exists or the path is not set correctly.";
        }

        public override string Help()
        {
            return $"Deletes '{DeletingFolderPath}'";
        }
    }
}
