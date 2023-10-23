namespace ManageSellProduct.Models
{
    public struct ImportInvoice
    {
        public string Code;
        public DateTime InvoiceDate;
        public string Supplier;
        public DetailImportProduct[] DetailImportProducts;
        public decimal TotalPrice;
    }

    public struct DetailImportProduct
    {
        public string SellInvoiceCode;
        public string ProductName;
        public string ProductCode;
        public int Quantity;
        public decimal Price;
        public decimal SumPrice => Quantity * Price;
    }
}
