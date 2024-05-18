using System;
using System.Data.SqlClient;
using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal class AuthRepoImpl : AuthRepo
    {

        private Database database;

        public AuthRepoImpl()
        {
            this.database = new Database();
        }

        public bool isUserCredentialsValid(string username, string password)
        {
            database.checkConnection();

            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM _user WHERE username='{username}' AND password='{password}'", database.getSqlConnection());
            int result = (int) command.ExecuteScalar();

            database.checkConnection();

            return result > 0;
        }

        public string fetchRoleByUsername(string username)
        {
            database.checkConnection();

            SqlCommand command = new SqlCommand($"SELECT role FROM _user WHERE username = '{username}'", database.getSqlConnection());
            string result = (string) command.ExecuteScalar();

            database.checkConnection();

            return result;
        }

        public bool isUserExistsByUsername(string username)
        {
            database.checkConnection();

            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM _user WHERE username='{username}'", database.getSqlConnection());
            int result = (int) command.ExecuteScalar();

            database.checkConnection();

            return result > 0;
        }

        public void createUser(UserEntity userEntity)
        {
            string query = $"INSERT INTO _user(username, password, role, creation_date) VALUES('{userEntity.username}', '{AuthSession.getSHA256Hash(userEntity.password)}', '{userEntity.role}', '{DateTime.Today.ToString("yyyy-MM-dd")}')";

            database.executeQuery(query);
        }
    }
}