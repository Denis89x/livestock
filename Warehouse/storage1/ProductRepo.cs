using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface ProductRepo
    {
        void fetchProductToGrid(DataGrid dataGrid);

        void createProduct(ProductEntity product);

        void updateProduct(ProductEntity product);

        void deleteProduct(long productId);
    }
}
