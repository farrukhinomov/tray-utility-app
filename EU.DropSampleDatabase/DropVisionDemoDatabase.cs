using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    [Utility(UtilityName)]
    public class DropVisionDemoDatabase : DropDatabase
    {
        const string DbNameVisionDemo = "Vision_Demo";
        const string UtilityName = "Drop '" + DbNameVisionDemo + "' database";

        public DropVisionDemoDatabase() : base(DbNameVisionDemo)
        {
        }
    }
}
