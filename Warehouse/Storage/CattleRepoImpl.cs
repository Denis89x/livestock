using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal class CattleRepoImpl : CrudRepo<CattleEntity>
    {
        Database database;

        public CattleRepoImpl()
        {
            this.database = new Database();
        }

        public void create(CattleEntity entity)
        {
            string query = $"INSERT INTO cattle(cattle_type) VALUES(N'{entity.cattleType}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM cattle WHERE cattle_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM cattle";

            database.selectQuery(query, dataGrid);
        }

        public void update(CattleEntity entity)
        {
            string query = $"UPDATE cattle SET cattle_type = N'{entity.cattleType}' WHERE cattle_id = '{entity.cattleId}'";

            database.executeQuery(query);
        }
    }
}