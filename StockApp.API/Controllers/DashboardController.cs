using StockApp.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController
    {
        private readonly IStockDashboardService _dashboardService;

        public DashboardController(IStockDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<ActionResult<StockDashboardDto>> Get()
        {
            var result = await _dashboardService.GetDashboardDataAsync();
            return (result);
        }
    }
}
