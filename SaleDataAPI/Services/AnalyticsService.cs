using Microsoft.EntityFrameworkCore;
using SaleDataAPI.Models;

namespace SaleDataAPI.Services
{
    public class AnalyticsService : IAnalyticsService
    {
        private readonly SalesDbContext _context;

        public AnalyticsService(SalesDbContext context)
        {
            _context = context;
        }

        public async Task<List<TopProductDto>> GetTopProductsOverallAsync(TopProductsRequest request)
        {
            var query = _context.OrderDetails
          .Include(od => od.Product)
          .Include(od => od.Order)
          .Where(od => od.Order.DateOfSale >= request.StartDate && od.Order.DateOfSale <= request.EndDate);

            if (!string.IsNullOrEmpty(request.Category))
            {
                query = query.Where(od => od.Product.Category == request.Category);
            }

            if (!string.IsNullOrEmpty(request.Region))
            {
                query = query.Where(od => od.Order.Region == request.Region);
            }

            var result = await query
                .GroupBy(od => new { od.Product.Id, od.Product.Name })
                .Select(g => new TopProductDto
                {
                    ProductId = g.Key.Id,
                    ProductName = g.Key.Name,
                    TotalQuantitySold = g.Sum(x => x.QuantitySold)
                })
                .OrderByDescending(x => x.TotalQuantitySold)
                .Take(request.TopN)
                .ToListAsync();

            return result;
        }

        Task<List<TopProductDto>> IAnalyticsService.GetTopProductsByCategoryAsync(TopProductsRequest request)
        {
            throw new NotImplementedException();
        }

        Task<List<TopProductDto>> IAnalyticsService.GetTopProductsByRegionAsync(TopProductsRequest request)
        {
            throw new NotImplementedException();
        }


    }

}
