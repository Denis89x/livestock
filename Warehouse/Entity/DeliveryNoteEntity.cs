namespace Warehouse.Entity
{
    internal class DeliveryNoteEntity
    {
        public long deliveryNoteId {  get; set; }

        public long divisionId {  get; set; }

        public string date {  get; set; }

        public string assignment {  get; set; }

        public DeliveryNoteEntity(long divisionId, string date, string assignment)
        {
            this.divisionId = divisionId;
            this.date = date;
            this.assignment = assignment;
        }

        public DeliveryNoteEntity(long deliveryNoteId, long divisionId, string date, string assignment)
        {
            this.deliveryNoteId = deliveryNoteId;
            this.divisionId = divisionId;
            this.date = date;
            this.assignment = assignment;
        }
    }
}