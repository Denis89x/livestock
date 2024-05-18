using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class DeliveryNoteRepoImpl : CrudRepo<DeliveryNoteEntity>
    {
        Database database;

        public DeliveryNoteRepoImpl()
        {
            this.database = new Database();
        }

        public void create(DeliveryNoteEntity entity)
        {
            string query = $"INSERT INTO delivery_note(division_id, date, assignment) VALUES ('{entity.divisionId}', '{entity.date}', N'{entity.assignment}')";

            database.executeQuery(query);
        }

        public void delete(long entityId)
        {
            string query = $"DELETE FROM delivery_note WHERE delivery_note_id = '{entityId}'";

            database.executeQuery(query);
        }

        public void fetchToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT delivery_note_id, division_type, date, assignment FROM delivery_note, division WHERE delivery_note.division_id = division.division_id";

            database.selectQuery(query, dataGrid);
        }

        public void update(DeliveryNoteEntity entity)
        {
            string query = $"UPDATE delivery_note SET division_id = '{entity.divisionId}', date = '{entity.date}', assignment = N'{entity.assignment}' WHERE delivery_note_id = '{entity.deliveryNoteId}'";

            database.executeQuery(query);
        }
    }
}