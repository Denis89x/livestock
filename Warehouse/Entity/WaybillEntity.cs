using System;

namespace Warehouse.Entity
{
    internal class WaybillEntity
    {
        public long waybillId { get; set; }

        public long contractorId { get; set; }

        public long employeeId { get; set; }

        public long productId {  get; set; }

        public int documentNumber { get; set; }

        public string carOwner { get; set; }

        public string date { get; set; }

        public string vehicle { get; set; }

        public string shipper {  get; set; }

        public string consignor {  get; set; }

        public string loadingPoint { get; set; }

        public string unloadingPoint { get; set; }

        public string treaty { get; set; }

        public string vehicleNumber { get; set; }

        public string guideListNumber { get; set; }

        public string driver {  get; set; }

        public string transType { get; set; }

        public string routeNumber { get; set; }

        private Random random;

        public WaybillEntity(long contractorId, long employeeId, string carOwner, string date, string vehicle, string shipper, string consignor, string loadingPoint, string unloadingPoint, string treaty, string vehicleNumber, string guideListNumber, string routeNumber, long productId, string driver, string transType)
        {
            this.contractorId = contractorId;
            this.employeeId = employeeId;
            this.carOwner = carOwner;
            this.date = date;
            this.vehicle = vehicle;
            this.shipper = shipper;
            this.consignor = consignor;
            this.loadingPoint = loadingPoint;
            this.unloadingPoint = unloadingPoint;
            this.treaty = treaty;
            this.vehicleNumber = vehicleNumber;
            this.guideListNumber = guideListNumber;
            this.routeNumber = routeNumber;
            this.productId = productId;
            this.driver = driver;
            this.transType = transType;

            random = new Random();

            documentNumber = random.Next(100000, 999999);
        }

        public WaybillEntity(long waybillId, long contractorId, long employeeId, string carOwner, string date, string vehicle, string shipper, string consignor, string loadingPoint, string unloadingPoint, string treaty, string vehicleNumber, string guideListNumber, string routeNumber, string driver, string transType)
        {
            this.waybillId = waybillId;
            this.contractorId = contractorId;
            this.employeeId = employeeId;
            this.carOwner = carOwner;
            this.date = date;
            this.vehicle = vehicle;
            this.shipper = shipper;
            this.consignor = consignor;
            this.loadingPoint = loadingPoint;
            this.unloadingPoint = unloadingPoint;
            this.treaty = treaty;
            this.vehicleNumber = vehicleNumber;
            this.guideListNumber = guideListNumber;
            this.routeNumber = routeNumber;
            this.driver = driver;
            this.transType = transType;
        }
    }
}