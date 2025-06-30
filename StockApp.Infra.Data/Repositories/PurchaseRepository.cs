using Microsoft.EntityFrameworkCore;
using StockApp.Application.DTOs;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
namespace StockApp.Infra.Data.Repositories
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly ApplicationDbContext _context;
        public PurchaseRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Purchase>> GetPurchaseReportAsync()
        {
            var result = await _context.Purchases
                .FromSqlRaw("EXEC GetPurchaseReport")
                .ToListAsync();

            return result;
        }
    }
}