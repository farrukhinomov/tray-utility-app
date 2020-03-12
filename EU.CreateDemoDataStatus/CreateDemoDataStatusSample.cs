using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusSample : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameSample;

        public CreateDemoDataStatusSample() : base(Constants.PathSample, Constants.NameSample)
        {
        }
    }
}
