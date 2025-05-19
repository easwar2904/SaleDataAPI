using Microsoft.AspNetCore.Mvc;
using SaleDataAPI.Models;
using SaleDataAPI.Services;

namespace SaleDataAPI.Controllers
{

    [ApiController]
    [Route("api/[controller]")]
    public class AnalyticsController : ControllerBase
    {
        private readonly IAnalyticsService _analyticsService;

        public AnalyticsController(IAnalyticsService analyticsService)
        {
            _analyticsService = analyticsService;
        }

        [HttpPost("top-products")]
        public async Task<IActionResult> GetTopProductsOverall([FromBody] TopProductsRequest request)
        {
            var result = await _analyticsService.GetTopProductsOverallAsync(request);
            return Ok(result);
        }

        //[HttpPost("top-products/by-category")]
        //public async Task<IActionResult> GetTopProductsByCategory([FromBody] TopProductsRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Category))
        //        return BadRequest("Category is required.");

        //    var result = await _analyticsService.GetTopProductsByCategoryAsync(request);
        //    return Ok(result);
        //}

        //[HttpPost("top-products/by-region")]
        //public async Task<IActionResult> GetTopProductsByRegion([FromBody] TopProductsRequest request)
        //{
        //    if (string.IsNullOrWhiteSpace(request.Region))
        //        return BadRequest("Region is required.");

        //    var result = await _analyticsService.GetTopProductsByRegionAsync(request);
        //    return Ok(result);
        //}
    }


}
