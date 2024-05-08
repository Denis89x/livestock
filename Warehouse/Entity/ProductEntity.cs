namespace Warehouse.Entity
{
    internal class ProductEntity
    {
        public long productId { get; set; }

        public string title { get; set; }

        public string sort { get; set; }

        public string unit { get; set; }

        public ProductEntity() { }

        public ProductEntity(long productId, string title, string sort, string unit)
        {
            this.productId = productId;
            this.title = title;
            this.sort = sort;
            this.unit = unit;
        }

        public ProductEntity(string title, string sort, string unit)
        {
            this.title = title;
            this.sort = sort;
            this.unit = unit;
        }
    }
}