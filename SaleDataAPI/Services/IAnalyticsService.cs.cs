using SaleDataAPI.Models;

namespace SaleDataAPI.Services
{
    public interface IAnalyticsService
    {
        Task<List<TopProductDto>> GetTopProductsOverallAsync(TopProductsRequest request);
        Task<List<TopProductDto>> GetTopProductsByCategoryAsync(TopProductsRequest request);
        Task<List<TopProductDto>> GetTopProductsByRegionAsync(TopProductsRequest request);
    }

}
