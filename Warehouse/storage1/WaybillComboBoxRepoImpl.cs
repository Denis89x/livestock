using System.Windows.Controls;

namespace Warehouse.storage1
{
    internal class WaybillComboBoxRepoImpl : WaybillComboBoxRepo
    {
        Database database;

        public WaybillComboBoxRepoImpl()
        {
            this.database = new Database();
        }

        public void insertContractorIntoComboBox(ComboBox box)
        {
            string query = $"SELECT contractor_id, title FROM contractor";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertEmployeeIntoComboBox(ComboBox box)
        {
            string query = $"SELECT employee_id, surname FROM employee";

            database.insertValuesIntoComboBox(query, box);
        }
    }
}
