using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class StatementRepoImpl : StatementRepo
    {
        Database database;

        public StatementRepoImpl()
        {
            this.database = new Database();
        }

        public void createStatement(StatementEntity statement)
        {
            string query = $"INSERT INTO statement(division_id, cattle_id, employee_id, product_id, date, foundation, statement_number, title, unit, supply_rate, animal_quantity, feed_quantity) VALUES (N'{statement.division}', N'{statement.cattle}', N'{statement.employee}', N'{statement.productTitle}', N'{statement.date}', N'{statement.foundation}', N'{statement.statementNumber}', N'{statement.title}', N'{statement.unit}', N'{statement.supplyRate}', N'{statement.animalQuantity}', N'{statement.feedQuantity}')";

            database.executeQuery(query);
        }

        public void deleteStatement(long statementId)
        {
            string query = $"DELETE FROM statement WHERE statement_id = '{statementId}'";

            database.executeQuery(query);
        }

        public void fetchStatementToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT statement_id, division.division_type, cattle.cattle_type, employee.surname, finished_product.title as product_title, date, foundation, statement_number, statement.title, statement.unit, supply_rate, animal_quantity, feed_quantity FROM statement, division, cattle, employee, finished_product WHERE statement.division_id = division.division_id AND statement.cattle_id = cattle.cattle_id AND statement.employee_id = employee.employee_id AND statement.product_id = finished_product.product_id";

            database.selectQuery(query, dataGrid);
        }

        public void updateStatement(StatementEntity statement)
        {
            MessageBox.Show("STATEMENT: " + statement);

            string query = $"UPDATE statement SET division_id = '{statement.division}', cattle_id = '{statement.cattle}', employee_id = '{statement.employee}', product_id = '{statement.productTitle}', date = '{statement.date}', foundation = N'{statement.foundation}', title = N'{statement.title}', unit = N'{statement.unit}', supply_rate = N'{statement.supplyRate}', animal_quantity = N'{statement.animalQuantity}', feed_quantity = N'{statement.feedQuantity}' WHERE statement_id = '{statement.statementId}'";

            database.executeQuery(query);
        }
    }
}
