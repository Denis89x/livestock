using System.Windows.Controls;

namespace Warehouse.Storage
{
    internal interface CrudRepo<TEntity>
    {
        void fetchToGrid(DataGrid dataGrid);

        void create(TEntity entity);

        void update(TEntity entity);

        void delete(long entityId);
    }
}