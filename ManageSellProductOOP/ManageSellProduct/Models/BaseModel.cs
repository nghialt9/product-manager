namespace ManageSellProduct.Models
{
    public class BaseModel
    {
        public string? ProductName { get; set; }
        public string? ProductCode { get; set; }
        public int Quantity { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
