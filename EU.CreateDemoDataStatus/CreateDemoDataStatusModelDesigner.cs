using Common;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace EU.CreateDemoDataStatus
{
    [Utility(UtilityName)]
    public class CreateDemoDataStatusModelDesigner : CreateDemoDataStatus
    {
        const string UtilityName = "Demo data creation status - " + Constants.NameModelDesigner;

        public CreateDemoDataStatusModelDesigner() : base(Constants.PathModelDesigner, Constants.NameModelDesigner)
        {
        }
    }
}
