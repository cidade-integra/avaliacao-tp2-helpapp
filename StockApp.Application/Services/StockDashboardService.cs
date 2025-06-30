using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class StockDashboardService : IStockDashboardService
    {
        private readonly IStockDashboardRepository _repository;

        public StockDashboardService(IStockDashboardRepository repository)
        {
            _repository = repository;
        }

        public async Task<StockDashboardDto> GetDashboardDataAsync()
        {
            var totalProducts = await _repository.GetTotalProductsAsync();
            var totalItemsInStock = await _repository.GetTotalItemsInStockAsync();
            var totalStockValue = await _repository.GetTotalStockValueAsync();
            var productsPerCategory = await _repository.GetProductsPerCategoryAsync();
            var outOfStockProducts = await _repository.GetOutOfStockProductNamesAsync();

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
