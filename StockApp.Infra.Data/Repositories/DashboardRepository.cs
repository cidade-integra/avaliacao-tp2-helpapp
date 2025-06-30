using Microsoft.EntityFrameworkCore;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.Infra.Data.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly ApplicationDbContext _context;

        public DashboardRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> GetTotalSalesAsync()
        {
            return await _context.Sales.SumAsync(s => s.Quantity);
        }

        public async Task<decimal> GetTotalRevenueAsync()
        {
            return await _context.Sales.SumAsync(s => s.TotalPrice);
        }

        public async Task<string> GetTopSellingProductAsync()
        {
            return await _context.Sales
                .GroupBy(s => s.ProductId)
                .OrderByDescending(g => g.Sum(x => x.Quantity))
                .Select(g => g.First().Product.Name)
                .FirstOrDefaultAsync();
        }

        public async Task<Dictionary<string, int>> GetSalesByCategoryAsync()
        {
            return await _context.Sales
                .Include(s => s.Product)
                .ThenInclude(p => p.Category)
                .GroupBy(s => s.Product.Category.Name)
                .Select(g => new { Category = g.Key, Quantity = g.Sum(x => x.Quantity) })
                .ToDictionaryAsync(g => g.Category, g => g.Quantity);
        }
    }
}
