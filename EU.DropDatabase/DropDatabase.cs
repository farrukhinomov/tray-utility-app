using Common;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;
using System;

namespace EU.DropDatabase
{
    public class DropDatabase : UtilityBase
    {
        string _dbName;
        string _dbNameWithTemporaryPostfix;
        string _connectionString;
        string _resultString;


        public DropDatabase(string dbName)
        {
            _dbName = dbName;
            _dbNameWithTemporaryPostfix = $"{dbName}_Temporary";
            _connectionString = @"data source=localhost;integrated security=True";
            _resultString = string.Empty;
        }
        public override string Run()
        {
            DropDataBase(_dbName);
            DropDataBase(_dbNameWithTemporaryPostfix);
            var result = _resultString != string.Empty ? _resultString.TrimEnd() : "Nothing was done";
            _resultString = string.Empty;
            return result;
        }
        public override string Help()
        {
            return $"Drops '{_dbName}' and '{_dbNameWithTemporaryPostfix}' databases";
        }

        private void DropDataBase(string dbName)
        {
            if (DatabaseExists(dbName))
            {
                using (var sqlConnection = GetConnection())
                {
                    string sqlCommandText = @"ALTER DATABASE " + dbName + @" SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
                                              DROP DATABASE [" + dbName + "]";
                    SqlCommand sqlCommand = new SqlCommand(sqlCommandText, sqlConnection);
                    sqlCommand.ExecuteNonQuery();
                    sqlConnection.Close();
                }
                _resultString += $"'{dbName}' database has been successfully dropped\n";
            }
        }

        bool DatabaseExists(string databaseName)
        {
            using (var sqlConnection = GetConnection())
            {
                var getDatabases = new SqlCommand("SELECT name FROM sys.databases", sqlConnection);
                using (SqlDataReader reader = getDatabases.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (string.Equals(reader[0].ToString(), databaseName, StringComparison.InvariantCultureIgnoreCase))
                        {
                            reader.Close();
                            sqlConnection.Close();
                            return true;
                        }
                    }
                    reader.Close();
                }
                sqlConnection.Close();
            }
            return false;
        }

        SqlConnection GetConnection()
        {
            SqlConnection connectionWithEmptyCatalog = default;
            try
            {
                var connectionStringToEmptyCatalog = new SqlConnectionStringBuilder(_connectionString);
                connectionStringToEmptyCatalog.InitialCatalog = string.Empty;
                connectionWithEmptyCatalog = new SqlConnection(connectionStringToEmptyCatalog.ToString());
                connectionWithEmptyCatalog.Open();
            }
            catch (Exception ex)
            {
                throw new Exception("Cannot get connection to the server with empty catalog", ex);
            }
            return connectionWithEmptyCatalog;
        }

        /*
         * TODO: For future if we wanna delete some specific table in db
         * SqlConnection GetConnection(string databaseName)
        {
            SqlConnection connection = default;
            try
            {
                var connectionString = new SqlConnectionStringBuilder(ConnectionString);
                connectionString.InitialCatalog = databaseName;
                connection = new SqlConnection(connectionString.ToString());
                connection.Open();
            }
            catch (Exception ex)
            {
                throw new Exception($"Cannot get connection to '{databaseName}' database", ex);
            }
            return connection;
        }*/
    }
}
