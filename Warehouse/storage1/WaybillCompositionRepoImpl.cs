using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class WaybillCompositionRepoImpl : CrudRepo<WaybillCompositionEntity>
    {
        Database database;

        public WaybillCompositionRepoImpl()
        {
            this.database = new Database();
        }

        public void create(WaybillCompositionEntity entity)
        {
            if (isWaybillCompositionExist(entity.waybillId))
            {
                MessageBox.Show("Данная ТТН уже заполнена!");
            }
            else
            {
                string query = $"INSERT INTO waybill_composition(waybill_id, product_id, waybill_type, fat, mass, acidity, temperature, cleaning_group, density, sort, packaging_type, quantity, brutto, tara, netto, grade) VALUES('{entity.waybillId}', '{entity.productId}', N'{entity.waybillType}', N'{entity.fat}', N'{entity.mass}', N'{entity.acidity}', N'{entity.temperature}', N'{entity.cleaningGroup}', N'{entity.density}', '{entity.sort}', '{entity.packagingType}', N'{entity.quantity}', N'{entity.brutto}', N'{entity.tara}', N'{entity.netto}', N'{entity.grade}')";

                database.executeQuery(query);
            }
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM waybill_composition WHERE waybill_composition_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT waybill_composition_id, waybill.document_number, finished_product.title, waybill_type, fat, mass, acidity, temperature, cleaning_group, density, waybill_composition.sort, packaging_type, quantity, brutto, tara, netto, grade FROM waybill, waybill_composition, finished_product WHERE waybill_composition.waybill_id = waybill.waybill_id AND waybill_composition.product_id = finished_product.product_id";

            database.selectQuery(query, dataGrid);
        }

        public void update(WaybillCompositionEntity entity)
        {
            string query = $"UPDATE waybill_composition SET product_id = '{entity.productId}', fat = N'{entity.fat}', mass = N'{entity.mass}', acidity = N'{entity.acidity}', temperature = N'{entity.temperature}', cleaning_group = N'{entity.cleaningGroup}', density = N'{entity.density}', sort = N'{entity.sort}', packaging_type = N'{entity.packagingType}', quantity = N'{entity.quantity}', brutto = N'{entity.brutto}', tara = N'{entity.tara}', netto = N'{entity.tara}', grade = N'{entity.grade}' WHERE waybill_composition_id = '{entity.waybillId}'";

            database.executeQuery(query);
        }

        private bool isWaybillCompositionExist(long waybillId)
        {
            database.checkConnection();
            SqlCommand command = new SqlCommand($"SELECT COUNT(*) FROM Waybill_Composition WHERE waybill_id = '{waybillId}'", database.getSqlConnection());
            int result = (int) command.ExecuteScalar();
            database.checkConnection();

            return result > 0;
        }
    }
}