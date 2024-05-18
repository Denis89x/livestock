using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class ProductRepoImpl : CrudRepo<ProductEntity>
    {
        Database database;

        public ProductRepoImpl()
        {
            this.database = new Database();
        }

        public void create(ProductEntity entity)
        {
            string query = $"INSERT INTO finished_product(title, sort, unit) VALUES(N'{entity.title}', N'{entity.sort}', N'{entity.unit}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM finished_product WHERE product_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM finished_product";

            database.selectQuery(query, dataGrid);
        }

        public void update(ProductEntity entity)
        {
            string query = $"UPDATE finished_product SET title = N'{entity.title}', sort = N'{entity.sort}', unit = N'{entity.unit}' WHERE product_id = '{entity.productId}'";

            database.executeQuery(query);
        }
    }
}