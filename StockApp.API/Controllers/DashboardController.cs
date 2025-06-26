using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockApp.Infra.Data.Context;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IStockDashboardService _dashboardService;
        private readonly ApplicationDbContext _context;

        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        public DashboardController(IStockDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public async Task<ActionResult<StockDashboardDto>> Get()
        {
            var result = await _dashboardService.GetDashboardDataAsync();
            return Ok(result);
        }

        [HttpGet("dashboard-purchases")]
        public async Task<IActionResult> GetDashboardPurchasesData()
        {
            var dashboardData = new DashboardPurchasesDto
            {
                TotalPurchases = await _context.Purchases.CountAsync(),
                TotalSpent = await _context.Purchases.SumAsync(p => p.Quantity * p.Price),
                TopSuppliers = await _context.Suppliers
                    .OrderByDescending(s => s.Purchases.Sum(p => p.Quantity))
                    .Take(5)
                    .Select(s => new SupplierPurchasesDto
                    {
                        SupplierName = s.Name,
                        TotalPurchased = s.Purchases.Sum(p => p.Quantity)
                    })
                    .ToListAsync()
            };
            return Ok(dashboardData);
        }
    }
}
