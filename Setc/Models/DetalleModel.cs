using System.Globalization;

namespace Setc.Models
{
    public class DetalleModel
    {
        public int orderNo { get; set; }
        public int cnscOrder { get; set; }
        public int itemCnsc { get; set; }
        public int productId { get; set; }
        public object barcode { get; set; }
        public string productName { get; set; }
        public double quantity { get; set; }
        public double price { get; set; }
        public string observations { get; set; }
        public string unitMeasure { get; set; }
        public double priceOrigin { get; set; }
        public double assignedWeight { get; set; }
        public int surtido { get; set; }
        public string ProductNameToTitleCase()
        {
            return (CultureInfo.InvariantCulture.TextInfo.ToTitleCase(productName.ToLower()));
        }
        public string Surtido()
        {
            string surtudoVal = string.Empty;
            switch (surtido)
            {
                case 0:
                    surtudoVal = "No Surtido";
                    break;
                case 1:
                    surtudoVal = "Surtido";
                    break;
            }
            return surtudoVal;
        }
    }
}