using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockApp.Infra.Data.Context;
using System.Linq;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável por fornecer dados estatísticos e analíticos para o painel de controle.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class DashboardController : ControllerBase
    {
        private readonly IStockDashboardService _dashboardService;
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Construtor utilizando o contexto do banco de dados diretamente.
        /// </summary>
        /// <param name="context">Contexto da aplicação.</param>
        public DashboardController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Construtor utilizando o serviço de dashboard.
        /// </summary>
        /// <param name="dashboardService">Serviço de dashboard.</param>
        public DashboardController(IStockDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        /// <summary>
        /// Retorna dados gerais do estoque para o painel principal.
        /// </summary>
        /// <returns>Dados resumidos do estoque.</returns>
        [HttpGet]
        public async Task<ActionResult<StockDashboardDto>> Get()
        {
            var result = await _dashboardService.GetDashboardDataAsync();
            return Ok(result);
        }

        /// <summary>
        /// Retorna dados detalhados sobre as compras para exibição no dashboard.
        /// </summary>
        /// <returns>Dados agregados de compras, incluindo total gasto e principais fornecedores.</returns>
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