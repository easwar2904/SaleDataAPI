namespace SaleDataAPI.Models
{
    public class TopProductsRequest
    {
        public int TopN { get; set; } = 5;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string? Category { get; set; }
        public string? Region { get; set; }
    }

}
