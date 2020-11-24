using Common;
using System.IO;

namespace EU.DeleteCache
{
    [Utility("DeleteCache.dll")]
    public class DeleteCache : UtilityBase
    {
        public override string Help()
        {
            return "Copies Microsoft.SqlServer.BatchParser.dll to Model Designer output";
        }

        public override string Run()
        {
            var path = @"C:\ProgramData\Eagle\Cache";
            if (Directory.Exists(path))
            {
                Directory.Delete(path, true);
                return "Cache has been deleted!";
            }
            else
            {
                return "Already deleted!";
            }
        }
    }
}
