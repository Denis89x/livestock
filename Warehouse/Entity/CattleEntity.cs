namespace Warehouse.Entity
{
    internal class CattleEntity
    {
        public long cattleId {  get; set; }

        public string cattleType {  get; set; }

        public CattleEntity() { }

        public CattleEntity(long cattleId, string cattleType)
        {
            this.cattleId = cattleId;
            this.cattleType = cattleType;
        }

        public CattleEntity(string cattleType)
        {
            this.cattleType = cattleType;
        }
    }
}
