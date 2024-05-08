using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface DivisionRepo
    {
        void fetchDivisionToGrid(DataGrid dataGrid);

        void createDivision(DivisionEntity division);

        void updateDivision(DivisionEntity division);

        void deleteDivision(long divisionId);
    }
}
