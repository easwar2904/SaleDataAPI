using Microsoft.AspNetCore.Mvc;
using SaleDataAPI.BusinesslogicLayer;

namespace SaleDataAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RefreshController : ControllerBase
    {
        private readonly ICsvRefreshService _csvRefreshService;

        public RefreshController(ICsvRefreshService csvRefreshService)
        {
            _csvRefreshService = csvRefreshService;
        }

        [HttpPost("trigger")]
        public async Task<IActionResult> TriggerRefresh()
        {
            try
            {
                await _csvRefreshService.RefreshDataAsync();
                return Ok("Data refresh triggered successfully.");
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
           
        }
    }
}
