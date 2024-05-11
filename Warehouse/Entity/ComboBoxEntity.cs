namespace Warehouse.Entity
{
    internal class ComboBoxEntity
    {
        public long id { get; set; }
        public string name { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}
