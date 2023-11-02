namespace ManageSellProduct.Models
{
    public struct SellInvoice
    {
        public string Code;
        public string CustomerName;
        public string Address;
        public DateTime InvoiceDate;
        public DetailSellProduct[] DetailSellProducts;
        public decimal TotalPrice;
    }

    public struct DetailSellProduct
    {
        public string SellInvoiceCode;
        public string ProductName;
        public string ProductCode;
        public int Quantity;
        public decimal Price;
        public decimal SumPrice => Quantity * Price;
    }
}
