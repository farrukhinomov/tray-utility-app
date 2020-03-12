using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusMyEbms : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameMyEbms;

        public CreateDemoDataStatusMyEbms() : base(Constants.PathMyEbms, Constants.NameMyEbms)
        {
        }
    }
}
