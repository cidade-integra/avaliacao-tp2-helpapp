using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.Infra.Data.Services
{public class DashboardService : IDashboardService
    {
        private readonly ApplicationDbContext _context;

        public DashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var totalVendas = await _context.Sales.SumAsync(s => s.Quantity);
            var receitaTotal = await _context.Sales.SumAsync(s => s.TotalPrice);

            var produtoMaisVendido = await _context.Sales
                .GroupBy(s => s.ProductId)
                .OrderByDescending(g => g.Sum(x => x.Quantity))
                .Select(g => g.First().Product.Name)
                .FirstOrDefaultAsync();

            var vendasPorCategoria = await _context.Sales
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .GroupBy(s => s.Product.Category.Name)
                .Select(g => new { Categoria = g.Key, Quantidade = g.Sum(x => x.Quantity) })
                .ToDictionaryAsync(g => g.Categoria, g => g.Quantidade);

            return new DashboardDto
            {
                TotalVendas = totalVendas,
                ReceitaTotal = receitaTotal,
                ProdutoMaisVendido = produtoMaisVendido,
                VendasPorCategoria = vendasPorCategoria
            };
        }
    }
}
