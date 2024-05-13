using System.Windows.Controls;

namespace Warehouse.storage1
{
    internal class ComboBoxRepoImpl : ComboBoxRepo
    {
        Database database;

        public ComboBoxRepoImpl()
        {
            this.database = new Database();
        }

        public void insertCattleIntoComboBox(ComboBox box)
        {
            string query = $"SELECT * FROM cattle";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertDivisionsIntoComboBox(ComboBox box)
        {
            string query = $"SELECT * FROM division";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertEmployeesIntoComboBox(ComboBox box)
        {
            string query = $"SELECT employee_id, surname FROM employee";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertProductsIntoComboBox(ComboBox box)
        {
            string query = $"SELECT product_id, title FROM finished_product";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertContractorIntoComboBox(ComboBox box)
        {
            string query = $"SELECT contractor_id, title FROM contractor";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertWaybillIntoComboBox(ComboBox box)
        {
            string query = $"SELECT waybill_id, document_number FROM waybill";

            database.insertValuesIntoComboBox(query, box);
        }
    }
}
