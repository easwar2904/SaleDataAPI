namespace SaleDataAPI.Models
{
    public class TopProductDto
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; } = default!;
        public int TotalQuantitySold { get; set; }
    }

}
