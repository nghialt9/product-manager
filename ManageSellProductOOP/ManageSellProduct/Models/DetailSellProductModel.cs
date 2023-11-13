namespace ManageSellProduct.Models
{
    public class DetailSellProductModel: BaseModel
    {
        public DetailSellProductModel()
        {
            SumPrice = Quantity * Price;
        }
        public string? SellInvoiceCode { get; set; }
        public decimal Price { get; set; }
        public decimal SumPrice { get; set; }
    }
}
