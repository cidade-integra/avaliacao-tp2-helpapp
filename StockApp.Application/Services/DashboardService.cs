using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public async Task<DashboardDto> GetDashboardDataAsync()
        {
            var totalSales = await _dashboardRepository.GetTotalSalesAsync();
            var totalRevenue = await _dashboardRepository.GetTotalRevenueAsync();
            var topProduct = await _dashboardRepository.GetTopSellingProductAsync();
            var salesByCategory = await _dashboardRepository.GetSalesByCategoryAsync();

            return new DashboardDto
            {
                TotalVendas = totalSales,
                ReceitaTotal = totalRevenue,
                ProdutoMaisVendido = topProduct,
                VendasPorCategoria = salesByCategory
            };
        }
    }
}
