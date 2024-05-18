using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class WaybillRepoImpl : CrudRepo<WaybillEntity>
    {
        Database database;

        public WaybillRepoImpl()
        {
            this.database = new Database();
        }

        public void create(WaybillEntity entity)
        {
            string query = $"INSERT INTO waybill(contractor_id, employee_id, document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, treaty, vehicle_number, guide_list_number, route_number) VALUES('{entity.contractorId}', '{entity.employeeId}', '{entity.documentNumber}', N'{entity.carOwner}', '{entity.date}', N'{entity.vehicle}', N'{entity.shipper}', N'{entity.consignor}', N'{entity.loadingPoint}', N'{entity.unloadingPoint}', N'{entity.treaty}', N'{entity.vehicleNumber}', '{entity.guideListNumber}', '{entity.routeNumber}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM waybill WHERE waybill_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT waybill_id, contractor.title, employee.surname, document_number, car_owner, date, vehicle, shipper, consignor, loading_point, unloading_point, treaty, vehicle_number, guide_list_number, route_number FROM waybill, employee, contractor WHERE waybill.employee_id = employee.employee_id AND waybill.contractor_id = contractor.contractor_id";

            database.selectQuery(query, dataGrid);
        }

        public void update(WaybillEntity entity)
        {
            string query = $"UPDATE waybill SET contractor_id = '{entity.contractorId}', employee_id = '{entity.employeeId}', car_owner = N'{entity.carOwner}', date = '{entity.date}', vehicle = N'{entity.vehicle}', shipper = N'{entity.shipper}', consignor = N'{entity.consignor}', loading_point = N'{entity.loadingPoint}', unloading_point = N'{entity.unloadingPoint}', treaty = N'{entity.treaty}', vehicle_number = '{entity.vehicleNumber}', guide_list_number = '{entity.guideListNumber}', route_number = '{entity.routeNumber}'";

            database.executeQuery(query);
        }
    }
}