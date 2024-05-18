using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
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
            string query = $"INSERT INTO statement(division_id, cattle_id, employee_id, product_id, date, foundation, statement_number, title, unit, supply_rate, animal_quantity, feed_quantity) VALUES (N'{entity.division}', N'{entity.cattle}', N'{entity.employee}', N'{entity.productTitle}', N'{entity.date}', N'{entity.foundation}', N'{entity.statementNumber}', N'{entity.title}', N'{entity.unit}', N'{entity.supplyRate}', N'{entity.animalQuantity}', N'{entity.feedQuantity}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM statement WHERE statement_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT statement_id, division.division_type, cattle.cattle_type, employee.surname, finished_product.title as product_title, date, foundation, statement_number, statement.title, statement.unit, supply_rate, animal_quantity, feed_quantity FROM statement, division, cattle, employee, finished_product WHERE statement.division_id = division.division_id AND statement.cattle_id = cattle.cattle_id AND statement.employee_id = employee.employee_id AND statement.product_id = finished_product.product_id";

            database.selectQuery(query, dataGrid);
        }

        public void update(StatementEntity entity)
        {
            string query = $"UPDATE statement SET division_id = '{entity.division}', cattle_id = '{entity.cattle}', employee_id = '{entity.employee}', product_id = '{entity.productTitle}', date = '{entity.date}', foundation = N'{entity.foundation}', title = N'{entity.title}', unit = N'{entity.unit}', supply_rate = N'{entity.supplyRate}', animal_quantity = N'{entity.animalQuantity}', feed_quantity = N'{entity.feedQuantity}' WHERE statement_id = '{entity.statementId}'";

            database.executeQuery(query);
        }

    }
}