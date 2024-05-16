namespace Warehouse.Entity
{
    internal class RecordCardCompositionEntity
    {
        public long recordCardCompositionId {  get; set; }

        public long recordCardId { get; set; }

        public string date {  get; set; }

        public string cowQuantity {  get; set; }

        public string milkMorning {  get; set; }

        public string milkMidday {  get; set; }

        public string milkEvening { get; set; }

        public RecordCardCompositionEntity(long recordCardId, string date, string cowQuantity, string milkMorning, string milkMidday, string milkEvening)
        {
            this.recordCardId = recordCardId;
            this.date = date;
            this.cowQuantity = cowQuantity;
            this.milkMorning = milkMorning;
            this.milkMidday = milkMidday;
            this.milkEvening = milkEvening;
        }

        public RecordCardCompositionEntity(long recordCardCompositionId, long recordCardId, string date, string cowQuantity, string milkMorning, string milkMidday, string milkEvening)
        {
            this.recordCardCompositionId = recordCardCompositionId;
            this.recordCardId = recordCardId;
            this.date = date;
            this.cowQuantity = cowQuantity;
            this.milkMorning = milkMorning;
            this.milkMidday = milkMidday;
            this.milkEvening = milkEvening;
        }
    }
}