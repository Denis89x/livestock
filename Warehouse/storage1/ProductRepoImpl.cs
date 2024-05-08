using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class ProductRepoImpl : ProductRepo
    {
        Database database;

        public ProductRepoImpl()
        {
            this.database = new Database();
        }

        public void createProduct(ProductEntity product)
        {
            string query = $"INSERT INTO finished_product(title, sort, unit) VALUES(N'{product.title}', N'{product.sort}', N'{product.unit}')";

            database.executeQuery(query);
        }

        public void deleteProduct(long productId)
        {
            string query = $"DELETE FROM finished_product WHERE product_id = '{productId}'";

            database.executeQuery(query);
        }

        public void fetchProductToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT * FROM finished_product";

            database.selectQuery(query, dataGrid);
        }

        public void updateProduct(ProductEntity product)
        {
            string query = $"UPDATE finished_product SET title = N'{product.title}', sort = N'{product.sort}', unit = N'{product.unit}' WHERE product_id = '{product.productId}'";

            database.executeQuery(query);
        }
    }
}
