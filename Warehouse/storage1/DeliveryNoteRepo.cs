using System.Windows.Controls;
using Warehouse.Entity;

namespace Warehouse.storage1
{
    internal interface DeliveryNoteRepo
    {
        void fetchDeliveryNoteToGrid(DataGrid dataGrid);

        void createDeliveryNote(DeliveryNoteEntity deliveryNote);

        void updateDeliveryNote(DeliveryNoteEntity deliveryNote);

        void deleteDeliveryNote(long deliveryNoteId);
    }
}
