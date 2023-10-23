namespace ManageSellProduct.Models
{
    public struct Product
    {
        public string Code;
        public string Name;
        public int NumberDaysUse;
        public DateTime ExpiryDate => ManufactureDate.AddDays(NumberDaysUse);
        public string Manufacturer;
        public DateTime ManufactureDate;
        public string CategoryCode;
        public string CategoryName;
        public decimal Price;
        public int Quantity;
    }
}
