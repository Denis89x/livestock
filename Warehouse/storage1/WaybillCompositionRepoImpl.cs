using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class WaybillCompositionRepoImpl : WaybillCompositionRepo
    {
        Database database;

        public WaybillCompositionRepoImpl()
        {
            this.database = new Database();
        }

        public void createWaybillComposition(WaybillCompositionEntity waybillComposition)
        {
            string query = $"INSERT INTO waybill_composition(waybill_id, product_id, waybill_type, fat, mass, acidity, temperature, cleaning_group, density, sort, packaging_type, quantity, brutto, tara, netto, grade) VALUES('{waybillComposition.waybillId}', '{waybillComposition.productId}', N'{waybillComposition.waybillType}', N'{waybillComposition.fat}', N'{waybillComposition.mass}', N'{waybillComposition.acidity}', N'{waybillComposition.temperature}', N'{waybillComposition.cleaningGroup}', N'{waybillComposition.density}', '{waybillComposition.sort}', '{waybillComposition.packagingType}', N'{waybillComposition.quantity}', N'{waybillComposition.brutto}', N'{waybillComposition.tara}', N'{waybillComposition.netto}', N'{waybillComposition.grade}')";

            database.executeQuery(query);
        }

        public void deleteWaybillComposition(long waybillCompositionId)
        {
            string query = $"DELETE FROM waybill_composition WHERE waybill_composition_id = '{waybillCompositionId}'";

            database.executeQuery(query);
        }

        public void fetchWaybillCompositionToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT waybill.document_number, finished_product.title, waybill_type, fat, mass, acidity, temperature, cleaning_group, density, sort, packaging_type, quantity, brutto, tara, netto, grade FROM waybill, waybill_composition, finished_product WHERE waybill_composition.waybill_id = waybill.waybill_id AND waybill_composition.product_id = finished_product.product_id";

            database.selectQuery(query, dataGrid);
        }

        public void updateWaybillComposition(WaybillCompositionEntity waybillComposition)
        {
            string query = $"UPDATE waybill_composition SET waybill_id = '{waybillComposition.waybillId}', product_id = '{waybillComposition.productId}', waybill_type = N'{waybillComposition.waybillType}', fat = N'{waybillComposition.fat}', mass = N'{waybillComposition.mass}', acidity = N'{waybillComposition.acidity}', temperature = N'{waybillComposition.temperature}', cleaning_group = N'{waybillComposition.cleaningGroup}', density = N'{waybillComposition.density}', sort = N'{waybillComposition.sort}', packaging_type = N'{waybillComposition.packagingType}', quantity = N'{waybillComposition.quantity}', brutto = N'{waybillComposition.brutto}', tara = N'{waybillComposition.tara}', netto = N'{waybillComposition.tara}', grade = N'{waybillComposition.grade}'";

            database.executeQuery(query);
        }
    }
}