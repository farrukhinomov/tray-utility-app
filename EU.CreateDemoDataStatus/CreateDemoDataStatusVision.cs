using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusVision : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameVision;

        public CreateDemoDataStatusVision() : base(Constants.PathVision, Constants.NameVision)
        {
        }
    }
}
