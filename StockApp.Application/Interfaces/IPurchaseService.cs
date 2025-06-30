using StockApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IPurchaseService
    {
        Task<IEnumerable<Purchase>> GetPurchaseReportAsync();
    }
}
