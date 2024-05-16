﻿using System;

namespace Warehouse.Entity
{
    internal class DeliveryCompositionEntity
    {
        public long deliveryCompositionId {  get; set; }

        public long deliveryId { get; set; }

        public long productId {  get; set; }

        public string unit {  get; set; }

        public string requested {  get; set; }

        public string released {  get; set; }

        public string price { get; set; }

        public string amount { get; set; }

        public DeliveryCompositionEntity(long deliveryId, long productId, string unit, string requested, string released, string price)
        {
            this.deliveryId = deliveryId;
            this.productId = productId;
            this.unit = unit;
            this.requested = requested;
            this.released = released;
            this.price = price;
            this.amount = (Convert.ToInt64(price) * Convert.ToInt64(requested)).ToString();
        }

        public DeliveryCompositionEntity(long deliveryCompositionId, long deliveryId, long productId, string unit, string requested, string released, string price)
        {
            this.deliveryCompositionId = deliveryCompositionId;
            this.deliveryId = deliveryId;
            this.productId = productId;
            this.unit = unit;
            this.requested = requested;
            this.released = released;
            this.price = price;
            this.amount = (Convert.ToInt64(price) * Convert.ToInt64(requested)).ToString();
        }
    }
}
