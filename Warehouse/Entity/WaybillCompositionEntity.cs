namespace Warehouse.Entity
{
    internal class WaybillCompositionEntity
    {
        public long waybillCompositionId { get; set; }

        public long waybillId { get; set; }

        public long productId {  get; set; }

        public string waybillType { get; set; }

        public string fat { get; set; }

        public string mass { get; set; }

        public string acidity {  get; set; }

        public string temperature {  get; set; }

        public string cleaningGroup {  get; set; }

        public string density {  get; set; }

        public string sort {  get; set; }

        public string packagingType {  get; set; }

        public string quantity {  get; set; }

        public string brutto {  get; set; }

        public string tara {  get; set; }

        public string netto {  get; set; }

        public string grade {  get; set; }

        public WaybillCompositionEntity(long waybillId, long productId, string waybillType, string fat, string mass, string acidity, string temperature, string cleaningGroup, string density, string sort, string packagingType, string quantity, string brutto, string tara, string netto, string grade)
        {
            this.waybillId = waybillId;
            this.productId = productId;
            this.waybillType = waybillType;
            this.fat = fat;
            this.mass = mass;
            this.acidity = acidity;
            this.temperature = temperature;
            this.cleaningGroup = cleaningGroup;
            this.density = density;
            this.sort = sort;
            this.packagingType = packagingType;
            this.quantity = quantity;
            this.brutto = brutto;
            this.tara = tara;
            this.netto = netto;
            this.grade = grade;
        }

        public WaybillCompositionEntity(long waybillCompositionId, long waybillId, long productId, string waybillType, string fat, string mass, string acidity, string temperature, string cleaningGroup, string density, string sort, string packagingType, string quantity, string brutto, string tara, string netto, string grade)
        {
            this.waybillCompositionId = waybillCompositionId;
            this.waybillId = waybillId;
            this.productId = productId;
            this.waybillType = waybillType;
            this.fat = fat;
            this.mass = mass;
            this.acidity = acidity;
            this.temperature = temperature;
            this.cleaningGroup = cleaningGroup;
            this.density = density;
            this.sort = sort;
            this.packagingType = packagingType;
            this.quantity = quantity;
            this.brutto = brutto;
            this.tara = tara;
            this.netto = netto;
            this.grade = grade;
        }
    }
}