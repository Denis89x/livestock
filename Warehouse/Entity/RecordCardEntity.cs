namespace Warehouse.Entity
{
    internal class RecordCardEntity
    {
        public long recordCardId { get; set; }

        public long productId { get; set; }

        public long divisionId { get; set; }

        public long employeeId { get; set; }

        public string date { get; set; }

        public RecordCardEntity(long productId, long divisionId, long employeeId, string date)
        {
            this.productId = productId;
            this.divisionId = divisionId;
            this.employeeId = employeeId;
            this.date = date;
        }

        public RecordCardEntity(long recordCardId, long productId, long divisionId, long employeeId, string date)
        {
            this.recordCardId = recordCardId;
            this.productId = productId;
            this.divisionId = divisionId;
            this.employeeId = employeeId;
            this.date = date;
        }
    }
}