namespace ManageSellProduct.Models
{
    public class DetailImportProductModel : BaseModel
    {
        public string? ImportInvoiceCode{ get; set; }
        public decimal Price{ get; set; }
        public decimal SumPrice => Quantity * Price;
    }
}
