using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class DivisionRepoImpl : DivisionRepo
    {
        Database database;

        public DivisionRepoImpl()
        {
            this.database = new Database();
        }

        public void createDivision(DivisionEntity division)
        {
            string query = $"INSERT INTO division(division_type) VALUES(N'{division.divisionType}')";

            database.executeQuery(query);
        }

        public void deleteDivision(long divisionId)
        {
            string query = $"DELETE FROM division WHERE division_id = '{divisionId}'";

            database.executeQuery(query);
        }

        public void fetchDivisionToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM division";

            database.selectQuery(query, dataGrid);
        }

        public void updateDivision(DivisionEntity division)
        {
            string query = $"UPDATE division SET division_type = N'{division.divisionType}' WHERE division_id = '{division.divisionId}'";

            database.executeQuery(query);
        }
    }
}
