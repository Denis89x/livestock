using System;

namespace Warehouse.Entity
{
    internal class StatementEntity
    {
        public long statementId {  get; set; }

        public long division { get; set; }

        public long cattle {  get; set; }

        public long employee { get; set; }

        public long productTitle { get; set; }

        public string date { get; set; }

        public int statementNumber {  get; set; }

        public string supplyRate {  get; set; }

        public string animalQuantity {  get; set; }

        public string feedQuantity { get; set; }

        public string actualRate;

        private Random random;

        public StatementEntity(long division, long cattle, long employee, long productTitle, string date, string supplyRate, string animalQuantity, string actualRate)
        {
            this.division = division;
            this.cattle = cattle;
            this.employee = employee;
            this.productTitle = productTitle;
            this.date = date;

            this.supplyRate = supplyRate;
            this.animalQuantity = animalQuantity;
            this.actualRate = actualRate;

            random = new Random();

            statementNumber = random.Next(100000, 999999);

            if (animalQuantity != null && animalQuantity != "" && supplyRate != null && supplyRate != "")
            {
                this.feedQuantity = (Convert.ToInt32(animalQuantity) * Convert.ToInt32(supplyRate)).ToString();
            }
        }

        public StatementEntity(long statementId, long division, long cattle, long employee, long productTitle, string date, string supplyRate, string animalQuantity, string actualRate)
        {
            this.statementId = statementId;
            this.division = division;
            this.cattle = cattle;
            this.employee = employee;
            this.productTitle = productTitle;
            this.date = date;
            this.supplyRate = supplyRate;
            this.animalQuantity = animalQuantity;
            this.actualRate = actualRate;

            if (animalQuantity != null && animalQuantity != "" && supplyRate != null && supplyRate != "")
            {
                this.feedQuantity = (Convert.ToInt32(animalQuantity) * Convert.ToInt32(supplyRate)).ToString();
            }
        }
    }
}