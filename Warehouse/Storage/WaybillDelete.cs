using System.Data.SqlClient;
using System.Windows;

namespace Warehouse.Storage
{
    internal class WaybillDelete
    {
        private Database database;
        private long waybillId;

        public WaybillDelete(long waybillId)
        {
            this.waybillId = waybillId;

            database = new Database();
        }

        public void deleteWaybill()
        {
            deleteWaybillComposition();
            deleteWaybillById();
        }

        private void deleteWaybillById()
        {
            try
            {
                string query = $"DELETE FROM waybill WHERE waybill_id = '{waybillId}'";

                database.executeQuery(query);
                MessageBox.Show("2");
            } catch(SqlException ex)
            {
                MessageBox.Show("EX: " + ex);
            }
            
        }

        private void deleteWaybillComposition()
        {
            try
            {
                string query = $"DELETE FROM waybill_composition WHERE waybill_id = '{waybillId}'";

                database.executeQuery(query);
                MessageBox.Show("1");
            }
            catch (SqlException ex)
            {
                MessageBox.Show("EX: " + ex);
            }
        }
    }
}