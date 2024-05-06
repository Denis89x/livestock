using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace Warehouse.storage1
{
    internal class Database
    {
        private string connectionString;
        private SqlConnection sqlConnection;

        public Database()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
            this.sqlConnection = new SqlConnection(this.connectionString);
        }

        public SqlConnection getSqlConnection()
        {
            return this.sqlConnection;
        }

        public void checkConnection()
        {
            if (sqlConnection.State == ConnectionState.Open)
            {
                sqlConnection.Close();
            }
            else if (sqlConnection.State == ConnectionState.Closed)
            {
                sqlConnection.Open();
            }
        }
    }
}
