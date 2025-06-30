using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.Infra.Data.Repositories
{
    public class StockDashboardRepository : IStockDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public StockDashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalProductsAsync()
        {
            return await _context.Products.CountAsync();
        }

        public async Task<int> GetTotalItemsInStockAsync()
        {
            return await _context.Products.SumAsync(p => p.Quantity);
        }

        public async Task<decimal> GetTotalStockValueAsync()
        {
            return await _context.Products.SumAsync(p => p.Quantity * p.Price);
        }

        public async Task<Dictionary<string, int>> GetProductsPerCategoryAsync()
        {
            return await _context.Products
                .Include(p => p.Category)
                .GroupBy(p => p.Category.Name)
                .Select(g => new { Category = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Category, g => g.Count);
        }

        public async Task<List<string>> GetOutOfStockProductNamesAsync()
        {
            return await _context.Products
                .Where(p => p.Quantity == 0)
                .Select(p => p.Name)
                .ToListAsync();
        }
    }

}
