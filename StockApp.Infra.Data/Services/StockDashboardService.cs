using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;


namespace StockApp.Infra.Data.Services
{
    public class StockDashboardService : IStockDashboardService
    {
        private readonly ApplicationDbContext _context;

        public StockDashboardService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<StockDashboardDto> GetDashboardDataAsync()
        {
            var totalProducts = await _context.Products.CountAsync();

            var totalItemsInStock = await _context.Products.SumAsync(p => p.Quantity);

            var totalStockValue = await _context.Products.SumAsync(p => p.Quantity * p.Price);

            var productsPerCategory = await _context.Products
                .Include(p => p.Category)
                .GroupBy(p => p.Category.Name)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Category, g => g.Count);

            var outOfStockProducts = await _context.Products
                .Where(p => p.Quantity == 0)
                .Select(p => p.Name)
                .ToListAsync();

            return new StockDashboardDto
            {
                TotalProducts = totalProducts,
                TotalItemsInStock = totalItemsInStock,
                TotalStockValue = totalStockValue,
                ProductsPerCategory = productsPerCategory,
                OutOfStockProducts = outOfStockProducts
            };
        }
    }
}
