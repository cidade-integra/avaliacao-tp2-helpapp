using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável por fornecer relatórios de compras.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PurchaseReportController : ControllerBase
    {
        private readonly IPurchaseService _purchaseService;

        public PurchaseReportController(IPurchaseService purchaseService)
        {
            _purchaseService = purchaseService;
        }

        /// <summary>
        /// Obtém o relatório de compras.
        /// </summary>
        /// <returns>Relatório contendo dados agregados de compras.</returns>
        /// <response code="200">Relatório retornado com sucesso.</response>
        /// <response code="404">Relatório não encontrado.</response>
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