using System.Windows.Controls;

namespace Warehouse.Storage
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
            string query = $"SELECT waybill_id, CAST(document_number AS VARCHAR) AS document_number FROM waybill";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertDeliveryIntoComboBox(ComboBox box)
        {
            string query = "SELECT delivery_note_id, CAST(delivery_note_id AS VARCHAR) AS delivery_note_id FROM delivery_note";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertRecordCardIntoComboBox(ComboBox box)
        {
            string query = "SELECT record_card_id, CAST(record_card_id AS VARCHAR) AS record_card_id FROM record_card";

            database.insertValuesIntoComboBox(query , box);
        }

        public void insertStatementIntoComboBox(ComboBox box)
        {
            string query = "SELECT statement_id, CAST(statement_number AS VARCHAR) as number FROM statement";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertValidWaybillIntoComboBox(ComboBox box)
        {
            string query = "SELECT waybill.waybill_id, CAST(waybill.document_number AS VARCHAR) AS document_number FROM waybill, waybill_composition WHERE waybill.waybill_id = waybill_composition.waybill_id";

            database.insertValuesIntoComboBox(query, box);
        }

        public void insertValidDeliveryIntoComboBox(ComboBox box)
        {
            string query = "SELECT DISTINCT delivery_note.delivery_note_id, CAST(delivery_note.delivery_note_id AS VARCHAR) AS delivery_note_id FROM delivery_note, delivery_note_composition WHERE delivery_note.delivery_note_id = delivery_note_composition.delivery_note_id";

            database.insertValuesIntoComboBox(query, box);
        }
    }
}