namespace ManageSellProduct.Models
{
    public class SellInvoiceModel
    {
        public string? SellInvoiceCode { get; set; }
        public string? CustomerName { get; set; }
        public string? Address { get; set; }
        public DateTime InvoiceDate { get; set; }
        public List<DetailSellProductModel>? DetailSellProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
