using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface CattleRepo
    {
        void fetchCattleToGrid(DataGrid dataGrid);

        void createCattle(CattleEntity cattle);

        void updateCattle(CattleEntity cattle);

        void deleteCattle(long cattleId);
    }
}
