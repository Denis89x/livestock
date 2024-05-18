using System.Windows.Controls;

namespace Warehouse.Service
{
    internal interface GridUtility
    {
        void applyFilter(string field, DataGrid dataGrid);

        void searchAndSort(string searchValue, DataGrid dataGrid);
    }
}
