using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class DivisionRepoImpl : CrudRepo<DivisionEntity>
    {
        Database database;

        public DivisionRepoImpl()
        {
            this.database = new Database();
        }

        public void create(DivisionEntity entity)
        {
            string query = $"INSERT INTO division(division_type) VALUES(N'{entity.divisionType}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM division WHERE division_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM division";

            database.selectQuery(query, dataGrid);
        }

        public void update(DivisionEntity entity)
        {
            string query = $"UPDATE division SET division_type = N'{entity.divisionType}' WHERE division_id = '{entity.divisionId}'";

            database.executeQuery(query);
        }
    }
}