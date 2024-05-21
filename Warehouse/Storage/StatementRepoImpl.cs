using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal class StatementRepoImpl : CrudRepo<StatementEntity>
    {
        Database database;

        public StatementRepoImpl()
        {
            this.database = new Database();
        }

        public void create(StatementEntity entity)
        {
            string query = $"INSERT INTO statement(division_id, cattle_id, employee_id, product_id, date, statement_number, supply_rate, animal_quantity, feed_quantity, actual_rate) VALUES (N'{entity.division}', N'{entity.cattle}', N'{entity.employee}', N'{entity.productTitle}', N'{entity.date}', N'{entity.statementNumber}', N'{entity.supplyRate}', N'{entity.animalQuantity}', N'{entity.feedQuantity}', '{entity.actualRate}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM statement WHERE statement_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT statement_id, division.division_type, cattle.cattle_type, employee.surname, finished_product.title as product_title, date, statement_number, supply_rate, animal_quantity, feed_quantity, actual_rate FROM statement, division, cattle, employee, finished_product WHERE statement.division_id = division.division_id AND statement.cattle_id = cattle.cattle_id AND statement.employee_id = employee.employee_id AND statement.product_id = finished_product.product_id";

            database.selectQuery(query, dataGrid);
        }

        public void update(StatementEntity entity)
        {
            string query = $"UPDATE statement SET division_id = '{entity.division}', cattle_id = '{entity.cattle}', employee_id = '{entity.employee}', date = '{entity.date}', supply_rate = N'{entity.supplyRate}', animal_quantity = N'{entity.animalQuantity}', feed_quantity = N'{entity.feedQuantity}', actual_rate = '{entity.actualRate}' WHERE statement_id = '{entity.statementId}'";

            database.executeQuery(query);
        }
    }
}