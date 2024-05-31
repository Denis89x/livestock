namespace Warehouse.Entity
{
    internal class WaybillCompositionEntity
    {
        public long waybillCompositionId {  get; set; }

        public long waybillId { get; set; }

        public string waybillType { get; set; }

        public string fat { get; set; }

        public string mass { get; set; }

        public string massResult { get; set; }

        public string sort {  get; set; }

        public string acidity { get; set; }

        public string temperature { get; set; }

        public string cleaningGroup { get; set; }

        public string density { get; set; }

        public string packagingType { get; set; }

        public string brutto { get; set; }

        public string tara { get; set; }

        public string netto { get; set; }

        public string grade { get; set; }

        public string quantity {  get; set; }

        public WaybillCompositionEntity()
        {
        }

        public WaybillCompositionEntity(long waybillId, string waybillType, string fat, string mass, string massResult, string acidity, string temperature, string cleaningGroup, string density, string packagingType, string brutto, string tara, string netto, string grade, string quantity, string sort)
        {
            this.waybillId = waybillId;
            this.waybillType = waybillType;
            this.fat = fat;
            this.mass = mass;
            this.acidity = acidity;
            this.temperature = temperature;
            this.cleaningGroup = cleaningGroup;
            this.density = density;
            this.packagingType = packagingType;
            this.massResult = massResult;
            this.brutto = brutto;
            this.tara = tara;
            this.netto = netto;
            this.grade = grade;
            this.quantity = quantity;
            this.sort = sort;
        }

        public WaybillCompositionEntity(long waybillCompositionId, long waybillId, string waybillType, string fat, string mass, string massResult, string acidity, string temperature, string cleaningGroup, string density, string packagingType, string brutto, string tara, string netto, string grade, string quantity, string sort)
        {
            this.waybillCompositionId = waybillCompositionId;
            this.waybillId = waybillId;
            this.waybillType = waybillType;
            this.fat = fat;
            this.mass = mass;
            this.massResult = massResult;
            this.acidity = acidity;
            this.temperature = temperature;
            this.cleaningGroup = cleaningGroup;
            this.density = density;
            this.packagingType = packagingType;
            this.brutto = brutto;
            this.tara = tara;
            this.netto = netto;
            this.grade = grade;
            this.quantity = quantity;
            this.sort = sort;
        }
    }
}