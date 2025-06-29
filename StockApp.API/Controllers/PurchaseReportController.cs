using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using System.Threading.Tasks;
namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseReportController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;
        public PurchaseReportController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }
        [HttpGet]
        public async Task<IActionResult> GetPurchaseReport()
        {
            var report = await _purchaseService.GetPurchaseReportAsync();
            if (report == null)
            {
                return NotFound("Purchase report not found.");
            }
            return Ok(report);
        }
    }
}