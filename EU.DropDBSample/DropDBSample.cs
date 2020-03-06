using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDBSample
{
    [Utility("Drop '" + DBNAME + "' database")]
    public class DropDBSample : UtilityBase
    {
        const string DBNAME = "Sample_Sample";
        const string DBNAME_Temporary = "Sample_Sample_Temporary";

        public override string Run()
        {
            
            //    String Connectionstring = CCMMUtility.CreateConnectionString(false, txt_DbDataSource.Text, "master", "sa", "happytimes", 1000);

            //    using (SqlConnection con = new SqlConnection(Connectionstring))
            //    {
            //        con.Open();
            //        String sqlCommandText = @"
            //ALTER DATABASE " + DbName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
            //DROP DATABASE [" + DbName + "]";
            //        SqlCommand sqlCommand = new SqlCommand(sqlCommandText, con);
            //        sqlCommand.ExecuteNonQuery();
            //    }
            //    result = 1;
            return $"'{DBNAME}' and '{DBNAME_Temporary}' database has been successfully dropped";
        }

        public override string Help()
        {
            return $"Drops '{DBNAME}' database";
        }
    }
}
