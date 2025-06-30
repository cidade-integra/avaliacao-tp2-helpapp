using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace StockApp.Application.Services
{
    public class PurchaseService : IPurchaseService
    {
        private readonly IPurchaseRepository _purchaseRepository;
        public PurchaseService(IPurchaseRepository purchaseRepository)
        {
            _purchaseRepository = purchaseRepository;
        }
        public async Task<IEnumerable<Purchase>> GetPurchaseReportAsync()
        {
            return await _purchaseRepository.GetPurchaseReportAsync();
        }
    }
}