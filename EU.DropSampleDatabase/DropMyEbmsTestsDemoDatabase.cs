using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    [Utility(UtilityName)]
    public class DropMyEbmsTestsDemoDatabase : DropDatabase
    {
        const string DbName = "MyEbms.Tests_Demo";
        const string UtilityName = "Drop database - " + DbName;

        public DropMyEbmsTestsDemoDatabase() : base(DbName)
        {
        }
    }
}
