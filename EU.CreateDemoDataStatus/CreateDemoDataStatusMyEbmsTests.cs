using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusMyEbmsTests : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameMyEbmsTests;

        public CreateDemoDataStatusMyEbmsTests() : base(Constants.PathMyEbmsTests, Constants.NameMyEbmsTests)
        {
        }
    }
}
