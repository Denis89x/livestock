using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class WaybillRepoImpl : WaybillRepo
    {
        Database database;

        public WaybillRepoImpl()
        {
            this.database = new Database();
        }

        public void createWaybill(WaybillEntity waybill)
        {
            string query = $"INSERT INTO waybill(contractor_id, employee_id, document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, treaty, vehicle_number, guide_list_number, route_number) VALUES('{waybill.contractorId}', '{waybill.employeeId}', '{waybill.documentNumber}', N'{waybill.carOwner}', '{waybill.date}', N'{waybill.vehicle}', N'{waybill.shipper}', N'{waybill.consignor}', N'{waybill.loadingPoint}', N'{waybill.unloadingPoint}', N'{waybill.treaty}', N'{waybill.vehicleNumber}', '{waybill.guideListNumber}', '{waybill.routeNumber}')";
        
            database.executeQuery(query);
        }

        public void deleteWaybill(long waybillId)
        {
            string query = $"DELETE FROM waybill WHERE waybill_id = '{waybillId}'";

            database.executeQuery(query);
        }

        public void fetchWaybillToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT waybill_id, contractor.title, employee.surname, document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, treaty, vehicle_number, guide_list_number, route_number FROM waybill, employee, contractor WHERE waybill.employee_id = employee.employee_id AND waybill.contractor_id = contractor.contractor_id";

            database.selectQuery(query, dataGrid);
        }

        public void updateWaybill(WaybillEntity waybill)
        {
            string query = $"UPDATE waybill SET contractor_id = '{waybill.contractorId}', employee_id = '{waybill.employeeId}', car_owner = N'{waybill.carOwner}', date = '{waybill.date}', vehicle = N'{waybill.vehicle}', shipper = N'{waybill.shipper}', consignor = N'{waybill.consignor}', loading_point = N'{waybill.loadingPoint}', unloading_point = N'{waybill.unloadingPoint}', treaty = N'{waybill.treaty}', vehicle_number = '{waybill.vehicleNumber}', guide_list_number = '{waybill.guideListNumber}', route_number = '{waybill.routeNumber}'";

            database.executeQuery(query);
        }
    }
}
