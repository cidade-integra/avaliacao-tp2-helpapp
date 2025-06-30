using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IStockDashboardService
    {
        Task<StockDashboardDto> GetDashboardDataAsync();
    }   
}
