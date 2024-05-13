using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface WaybillCompositionRepo
    {
        void fetchWaybillCompositionToGrid(DataGrid dataGrid);

        void createWaybillComposition(WaybillCompositionEntity waybillComposition);

        void updateWaybillComposition(WaybillCompositionEntity waybillComposition);

        void deleteWaybillComposition(long waybillCompositionId);
    }
}
