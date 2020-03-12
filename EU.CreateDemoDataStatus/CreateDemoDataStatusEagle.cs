using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusEagle : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameEagle;

        public CreateDemoDataStatusEagle() : base(Constants.PathEagle, Constants.NameEagle)
        {
        }
    }
}
