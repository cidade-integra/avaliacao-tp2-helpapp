namespace StockApp.Domain.Interfaces
{
    public interface IStockDashboardRepository
    {
        Task<int> GetTotalProductsAsync();
        Task<int> GetTotalItemsInStockAsync();
        Task<decimal> GetTotalStockValueAsync();
        Task<Dictionary<string, int>> GetProductsPerCategoryAsync();
        Task<List<string>> GetOutOfStockProductNamesAsync();
    }

}
