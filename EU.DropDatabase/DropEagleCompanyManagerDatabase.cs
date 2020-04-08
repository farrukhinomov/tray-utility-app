using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    [Utility(UtilityName)]
    public class DropEagleCompanyManagerDatabase : DropDatabase
    {
        const string DbName = "EagleCompanyManager_EagleCompanyManager";
        const string UtilityName = "Drop database - " + DbName;

        public DropEagleCompanyManagerDatabase() : base(DbName)
        {
        }
    }
}
