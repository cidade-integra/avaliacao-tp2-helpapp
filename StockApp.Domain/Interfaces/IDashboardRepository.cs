namespace StockApp.Domain.Interfaces
{
    public interface IDashboardRepository
    {
        Task<int> GetTotalSalesAsync();
        Task<decimal> GetTotalRevenueAsync();
        Task<string> GetTopSellingProductAsync();
        Task<Dictionary<string, int>> GetSalesByCategoryAsync();
    }
}
