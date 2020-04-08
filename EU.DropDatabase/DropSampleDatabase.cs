using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    [Utility(UtilityName)]
    public class DropSampleDatabase : DropDatabase
    {
        const string DbName = "Sample_Sample";
        const string UtilityName = "Drop database - " + DbName;

        public DropSampleDatabase() : base(DbName)
        {
        }
    }
}
