using System;
using System.Windows;

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

        public string foundation {  get; set; }

        public int statementNumber {  get; set; }

        public string title { get; set; }

        public string unit { get; set; }

        public string supplyRate {  get; set; }

        public string animalQuantity {  get; set; }

        public string feedQuantity { get; set; }

        private Random random;

        public StatementEntity(long division, long cattle, long employee, long productTitle, string date, string foundation, string title, string unit, string supplyRate, string animalQuantity, string feedQuantity)
        {
            this.division = division;
            this.cattle = cattle;
            this.employee = employee;
            this.productTitle = productTitle;
            this.date = date;
            this.foundation = foundation;
            this.title = title;
            this.unit = unit;
            this.supplyRate = supplyRate;
            this.animalQuantity = animalQuantity;
            this.feedQuantity = feedQuantity;

            random = new Random();

            statementNumber = random.Next(100000, 999999);
        }

        public StatementEntity(long statementId, long division, long cattle, long employee, long productTitle, string date, string foundation, string title, string unit, string supplyRate, string animalQuantity, string feedQuantity)
        {
            this.statementId = statementId;
            this.division = division;
            this.cattle = cattle;
            this.employee = employee;
            this.productTitle = productTitle;
            this.date = date;
            this.foundation = foundation;
            this.title = title;
            this.unit = unit;
            this.supplyRate = supplyRate;
            this.animalQuantity = animalQuantity;
            this.feedQuantity = feedQuantity;
        }

        public override string ToString()
        {
            return $"statementId: {statementId}, division: {division}, cattle: {cattle}, employee: {employee}, " +
                   $"productTitle: {productTitle}, date: {date}, foundation: {foundation}, statementNumber: {statementNumber}, " +
                   $"title: {title}, unit: {unit}, supplyRate: {supplyRate}, animalQuantity: {animalQuantity}, feedQuantity: {feedQuantity}";
        }

    }
}
