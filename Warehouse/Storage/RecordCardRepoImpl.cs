using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal class RecordCardRepoImpl : CrudRepo<RecordCardEntity>
    {
        Database database;

        public RecordCardRepoImpl()
        {
            database = new Database();
        }

        public void create(RecordCardEntity entity)
        {
            string query = $"INSERT INTO record_card(product_id, division_id, employee_id, date, cow_quantity, morning, midday, evening, card_number) VALUES('{entity.productId}', '{entity.divisionId}', '{entity.employeeId}', '{entity.date}', '{entity.cowQuantity}', '{entity.morning}', '{entity.midday}', '{entity.evening}', '{entity.cardNumber}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM record_card WHERE record_card_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = "SELECT record_card_id, finished_product.title, division.division_type, employee.surname, date, cow_quantity, morning, midday, evening, card_number FROM record_card, employee, division, finished_product WHERE record_card.division_id = division.division_id AND record_card.product_id = finished_product.product_id AND record_card.employee_id = employee.employee_id";
        
            database.selectQuery(query, dataGrid);
        }

        public void update(RecordCardEntity entity)
        {
            string query = $"UPDATE record_card SET product_id = '{entity.productId}', division_id = '{entity.divisionId}', employee_id = '{entity.employeeId}', date = '{entity.date}', cow_quantity = '{entity.cowQuantity}', morning = '{entity.morning}', midday = '{entity.midday}', evening = '{entity.evening}' WHERE record_card_id = '{entity.recordCardId}'";

            database.executeQuery(query);
        }
    }
}