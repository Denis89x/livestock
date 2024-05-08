namespace Warehouse.Entity
{
    internal class DivisionEntity
    {
        public long divisionId {  get; set; }

        public string divisionType {  get; set; }

        public DivisionEntity() { }

        public DivisionEntity(long divisionId, string divisionType)
        {
            this.divisionId = divisionId;
            this.divisionType = divisionType;
        }

        public DivisionEntity(string divisionType)
        {
            this.divisionType = divisionType;
        }
    }
}
