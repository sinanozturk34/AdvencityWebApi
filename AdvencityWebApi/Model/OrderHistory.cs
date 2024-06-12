namespace AdvencityWebApi.Model
{
    public class OrderHistory
    {
        public string ORDER_NUMBER { get; set; }
        public string PRODUCT_CODE { get; set; }
        public string PRODUCT_NAME { get; set; }
        public double QUANTITY { get; set; }
        public double UNIT_PRICE { get; set; }
        public double TOTAL_LINE_PRICE { get; set; }
        public DateTime CREATION_DATE { get; set; }
    }
}
