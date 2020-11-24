using Common;
using System.IO;

namespace EU.ModelDesignerActions
{
    [Utility("CopyBatchParser.dll")]
    public class CopyBatchParser : UtilityBase
    {
        public override string Help()
        {
            return "Copies Microsoft.SqlServer.BatchParser.dll to Model Designer output";
        }

        public override string Run()
        {
            if (!File.Exists(@"C:\Repos\Eagle\Output\Debug\ModelDesigner\Demo\Microsoft.SqlServer.BatchParser.dll"))
            {
                File.Copy("Microsoft.SqlServer.BatchParser.dll", @"C:\Repos\Eagle\Output\Debug\ModelDesigner\Demo\Microsoft.SqlServer.BatchParser.dll");
                return "Copied!";
            }
            else
            {
                return "File Already exists!";
            }
        }
    }
}
