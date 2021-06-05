using System;
using System.Collections.Generic;

namespace Setc.Models
{
    public class OrdenModel
    {
        public int orderNo { get; set; }
        public int cnscOrder { get; set; }
        public int storeNum { get; set; }
        public string ueNo { get; set; }
        public int statusUe { get; set; }
        public DateTime orderDate { get; set; }
        public object orderTime { get; set; }
        public object deliveryType { get; set; }
        public object ueType { get; set; }
        public bool isPickingManual { get; set; }
        public int customerNo { get; set; }
        public object customerName { get; set; }
        public object phone { get; set; }
        public object address1 { get; set; }
        public object address2 { get; set; }
        public object city { get; set; }
        public object stateCode { get; set; }
        public object postalCode { get; set; }
        public object reference { get; set; }
        public object nameReceives { get; set; }
        public double total { get; set; }
        public object transactionId { get; set; }
        public object methodPayment { get; set; }
        public object cardNumber { get; set; }
        public object shipperName { get; set; }
        public DateTime shippingDate { get; set; }
        public object transactionNo { get; set; }
        public object trackingNo { get; set; }
        public int numBags { get; set; }
        public int numCoolers { get; set; }
        public int numContainers { get; set; }
        public object terminal { get; set; }
        public DateTime deliveryDate { get; set; }
        public object deliveryTime { get; set; }
        public object idReceive { get; set; }
        public object nameReceive { get; set; }
        public object comments { get; set; }
        public int id_Supplier { get; set; }
        public DateTime pickingDateEnd { get; set; }
        public object pickingTimeEnd { get; set; }
        public DateTime paymentDateEnd { get; set; }
        public object paymentTimeEnd { get; set; }
        public DateTime packingDate { get; set; }
        public object packingTime { get; set; }
        public DateTime cancelOrderDate { get; set; }
        public object cancelOrderTime { get; set; }
        public DateTime carrierDate { get; set; }
        public object carrierTime { get; set; }
        public object cancelCause { get; set; }
        public object consignmentType { get; set; }
        public List<DetalleModel> detalle { get; set; }
    }
}