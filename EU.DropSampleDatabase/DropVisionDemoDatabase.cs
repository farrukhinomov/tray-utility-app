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
        const string DbName = "Vision_Demo";
        const string UtilityName = "Drop database - " + DbName;

        public DropVisionDemoDatabase() : base(DbName)
        {
        }
    }
}
