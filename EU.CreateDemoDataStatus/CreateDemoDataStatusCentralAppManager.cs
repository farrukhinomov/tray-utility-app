using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusCentralAppManager : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameCentralAppManager;

        public CreateDemoDataStatusCentralAppManager() : base(Constants.PathCentralAppManager, Constants.NameCentralAppManager)
        {
        }
    }
}
