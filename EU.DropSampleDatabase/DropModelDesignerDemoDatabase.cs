using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    [Utility(UtilityName)]
    public class DropModelDesignerDemoDatabase : DropDatabase
    {
        const string DbName = "ModelDesigner_Demo";
        const string UtilityName = "Drop database - " + DbName;

        public DropModelDesignerDemoDatabase() : base(DbName)
        {
        }
    }
}
