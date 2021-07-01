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
        public string orderTime { get; set; }
        public string deliveryType { get; set; }
        public string ueType { get; set; }
        public bool isPickingManual { get; set; }
        public int customerNo { get; set; }
        public string customerName { get; set; }
        public string phone { get; set; }
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public string stateCode { get; set; }
        public string postalCode { get; set; }
        public string reference { get; set; }
        public string nameReceives { get; set; }
        public double total { get; set; }
        public string transactionId { get; set; }
        public string methodPayment { get; set; }
        public string cardNumber { get; set; }
        public string shipperName { get; set; }
        public DateTime shippingDate { get; set; }
        public string transactionNo { get; set; }
        public string trackingNo { get; set; }
        public int numBags { get; set; }
        public int numCoolers { get; set; }
        public int numContainers { get; set; }
        public string terminal { get; set; }
        public DateTime deliveryDate { get; set; }
        public object deliveryTime { get; set; }
        public string idReceive { get; set; }
        public string nameReceive { get; set; }
        public string comments { get; set; }
        public int id_Supplier { get; set; }
        public DateTime pickingDateEnd { get; set; }
        public string pickingTimeEnd { get; set; }
        public DateTime paymentDateEnd { get; set; }
        public string paymentTimeEnd { get; set; }
        public DateTime packingDate { get; set; }
        public string packingTime { get; set; }
        public DateTime cancelOrderDate { get; set; }
        public string cancelOrderTime { get; set; }
        public DateTime carrierDate { get; set; }
        public string carrierTime { get; set; }
        public string cancelCause { get; set; }
        public string consignmentType { get; set; }
        public List<DetalleModel> detalle { get; set; }
    }
}