namespace ManageSellProduct.Models
{
    public class ProductModel : BaseModel
    {
        public ProductModel()
        {
            ExpiryDate = ManufactureDate.AddDays(NumberDaysUse);
        }
        public int NumberDaysUse { get; set; }
        public new DateTime ExpiryDate { get; set; }
        public string? Manufacturer { get; set; }
        public DateTime ManufactureDate { get; set; }
        public string? CategoryCode { get; set; }
        public string? CategoryName { get; set; }
        public decimal Price { get; set; }
    }
}
