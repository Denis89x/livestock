using System.Data.SqlClient;

namespace Warehouse.storage1
{
    internal class AuthRepoImpl : AuthRepo
    {

        private Database database;

        public AuthRepoImpl()
        {
            this.database = new Database();
        }

        public bool checkExistsUser(string username, string password)
        {
            database.checkConnection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM _user WHERE username='{username}' AND password='{password}'", database.getSqlConnection());
            int result = (int)command.ExecuteScalar();
            database.checkConnection();

            return result > 0;
        }
    }
}
