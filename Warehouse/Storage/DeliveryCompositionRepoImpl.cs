using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.Storage
{
    internal class DeliveryCompositionRepoImpl : CrudRepo<DeliveryCompositionEntity>
    {
        private Database database;

        public DeliveryCompositionRepoImpl()
        {
            this.database = new Database();
        }

        public void create(DeliveryCompositionEntity entity)
        {
            string query = $"INSERT INTO delivery_note_composition(delivery_note_id, product_id, requested, released, price, amount) VALUES('{entity.deliveryId}', '{entity.productId}', '{entity.requested}', '{entity.released}', '{entity.price}', '{entity.amount}')";
        
            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM delivery_note_composition WHERE delivery_note_composition_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = "SELECT delivery_note_composition_id, delivery_note_composition.delivery_note_id, finished_product.title as name, requested, released, price, amount FROM delivery_note_composition, delivery_note, finished_product WHERE delivery_note_composition.product_id = finished_product.product_id AND delivery_note_composition.delivery_note_id = delivery_note.delivery_note_id";

            database.selectQuery(query, dataGrid);
        }

        public void update(DeliveryCompositionEntity entity)
        {
            string query = $"UPDATE delivery_note_composition SET requested = '{entity.requested}', released = '{entity.released}', price = '{entity.price}', amount = '{entity.amount}' WHERE delivery_note_composition_id = '{entity.deliveryCompositionId}'";

            database.executeQuery(query);
        }
    }
}