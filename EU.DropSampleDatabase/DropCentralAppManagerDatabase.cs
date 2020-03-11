using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    [Utility(UtilityName)]
    public class DropCentralAppManagerDatabase : DropDatabase
    {
        const string DbName = "CentralAppManager_CentralAppManager";
        const string UtilityName = "Drop database - " + DbName;

        public DropCentralAppManagerDatabase() : base(DbName)
        {
        }
    }
}
