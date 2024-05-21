using System;

namespace Warehouse.Entity
{
    internal class RecordCardEntity
    {
        public long recordCardId { get; set; }

        public long productId { get; set; }

        public long divisionId { get; set; }

        public long employeeId { get; set; }

        public string date { get; set; }

        public string cowQuantity { get; set; }

        public string morning { get; set; }

        public string midday { get; set; }

        public string evening { get; set; }

        public string cardNumber { get; set; }

        private Random random;

        public RecordCardEntity(long productId, long divisionId, long employeeId, string date, string cowQuantity, string morning, string midday, string evening)
        {
            this.productId = productId;
            this.divisionId = divisionId;
            this.employeeId = employeeId;
            this.date = date;
            this.cowQuantity = cowQuantity;
            this.morning = morning;
            this.midday = midday;
            this.evening = evening;

            random = new Random();

            cardNumber = random.Next(100000, 999999).ToString();
        }

        public RecordCardEntity(long recordCardId, long productId, long divisionId, long employeeId, string date, string cowQuantity, string morning, string midday, string evening)
        {
            this.recordCardId = recordCardId;
            this.productId = productId;
            this.divisionId = divisionId;
            this.employeeId = employeeId;
            this.date = date;
            this.cowQuantity = cowQuantity;
            this.morning = morning;
            this.midday = midday;
            this.evening = evening;
        }
    }
}