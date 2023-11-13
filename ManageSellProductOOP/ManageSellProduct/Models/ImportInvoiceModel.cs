namespace ManageSellProduct.Models
{
    public class ImportInvoiceModel
    {
        public string? ImportInvoiceCode { get; set; }
        public DateTime InvoiceDate { get; set; }
        public string? Supplier { get; set; }
        public string? Address { get; set; }
        public List<DetailImportProductModel>? DetailImportProducts { get; set; }
        public decimal TotalPrice { get; set; }
    }
}
