using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class CattleRepoImpl : CattleRepo
    {
        Database database;

        public CattleRepoImpl()
        {
            this.database = new Database();
        }

        public void createCattle(CattleEntity cattle)
        {
            string query = $"INSERT INTO cattle(cattle_type) VALUES(N'{cattle.cattleType}')";

            database.executeQuery(query);
        }

        public void deleteCattle(long cattleId)
        {
            string query = $"DELETE FROM cattle WHERE cattle_id = '{cattleId}'";

            database.executeQuery(query);
        }

        public void fetchCattleToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM cattle";

            database.selectQuery(query, dataGrid);
        }

        public void updateCattle(CattleEntity cattle)
        {
            string query = $"UPDATE cattle SET cattle_type = N'{cattle.cattleType}' WHERE cattle_id = '{cattle.cattleId}'";

            database.executeQuery(query);
        }
    }
}