using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.Storage
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

        public void selectQuery(string query, DataGrid dataGrid)
        {
            checkConnection();
            SqlDataAdapter adapter = new SqlDataAdapter(query, sqlConnection);
            DataTable dataTable = new DataTable();
            adapter.Fill(dataTable);
            dataGrid.ItemsSource = dataTable.DefaultView;
            checkConnection();
        }

        public void executeQuery(string query)
        {
            checkConnection();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            command.ExecuteNonQuery();
            checkConnection();
        }

        public void insertValuesIntoComboBox(string query, ComboBox box)
        {
            checkConnection();
            SqlCommand command = new SqlCommand(query, sqlConnection);
            SqlDataReader reader = command.ExecuteReader();

            box.Items.Clear();
            while (reader.Read())
            {
                ComboBoxEntity comboBox = new ComboBoxEntity();
                comboBox.id = reader.GetInt64(0);
                comboBox.name = reader.GetString(1).ToString();
                box.Items.Add(comboBox);
            }
            reader.Close();
            checkConnection();
        }
    }
}
