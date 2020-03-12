using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EU.CreateDemoDataStatus
{
    public class Constants
    {
        public const string PathOutput = @"C:\Repos\Output\Debug\";
        public const string FileName = "config.json";

        public const string PathEagle = "";
        public const string NameEagle = "Eagle";

        public const string PathSample = PathOutput + @"Sample\Sample\" + FileName;
        public const string NameSample = "Sample";

        public const string PathVision = PathOutput + @"Vision\Demo\" + FileName;
        public const string NameVision = "Vision";

        public const string PathModelDesigner = PathOutput + @"ModelDesigner\Demo\" + FileName;
        public const string NameModelDesigner = "ModelDesigner";

        public const string PathMyEbms = PathOutput + @"MyEbms\Demo\" + FileName;
        public const string NameMyEbms = "MyEbms";

        public const string PathMyEbmsTests = PathOutput + @"MyEbms.Tests\Demo\" + FileName;
        public const string NameMyEbmsTests = "MyEbmsTests";

        public const string PathCentralAppManager = PathOutput + @"CentralAppManager\CentralAppManager\" + FileName;
        public const string NameCentralAppManager = "CentralAppManager";
    }

}
