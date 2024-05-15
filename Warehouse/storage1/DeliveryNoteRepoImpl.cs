using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal class DeliveryNoteRepoImpl : DeliveryNoteRepo
    {
        Database database;

        public DeliveryNoteRepoImpl()
        {
            this.database = new Database();
        }

        public void createDeliveryNote(DeliveryNoteEntity deliveryNote)
        {
            string query = $"INSERT INTO delivery_note(division_id, date, assignment) VALUES ('{deliveryNote.divisionId}', '{deliveryNote.date}', N'{deliveryNote.assignment}')";
        
            database.executeQuery(query);
        }

        public void deleteDeliveryNote(long deliveryNoteId)
        {
            string query = $"DELETE FROM delivery_note WHERE delivery_note_id = '{deliveryNoteId}'";

            database.executeQuery(query);
        }

        public void fetchDeliveryNoteToGrid(DataGrid dataGrid)
        {
            string query = $"SELECT delivery_note_id, division_type, date, assignment FROM delivery_note, division WHERE delivery_note.division_id = division.division_id";

            database.selectQuery(query, dataGrid);
        }

        public void updateDeliveryNote(DeliveryNoteEntity deliveryNote)
        {
            string query = $"UPDATE delivery_note SET division_id = '{deliveryNote.divisionId}', date = '{deliveryNote.date}', assignment = N'{deliveryNote.assignment}' WHERE delivery_note_id = '{deliveryNote.deliveryNoteId}'";

            database.executeQuery(query);
        }
    }
}