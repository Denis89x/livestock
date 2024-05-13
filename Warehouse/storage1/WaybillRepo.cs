using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface WaybillRepo
    {
        void fetchWaybillToGrid(DataGrid dataGrid);

        void createWaybill(WaybillEntity waybill);

        void updateWaybill(WaybillEntity waybill);

        void deleteWaybill(long waybillId);
    }
}