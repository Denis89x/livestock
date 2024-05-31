namespace Warehouse.Storage
{
    internal class DeliveryDelete
    {
        private Database database;
        private long deliveryId;

        public DeliveryDelete(long deliveryId)
        {
            this.deliveryId = deliveryId;

            database = new Database();
        }

        public void deleteDelivery()
        {
            deleteDeliveryComposition();
            deleteDeliveryById();
        }

        private void deleteDeliveryById()
        {
            string query = $"DELETE FROM delivery_note WHERE delivery_note_id = '{deliveryId}'";

            database.executeQuery(query);
        }

        private void deleteDeliveryComposition()
        {
            string query = $"DELETE FROM delivery_note_composition WHERE delivery_note_id = '{deliveryId}'";

            database.executeQuery(query);
        }
    }
}
