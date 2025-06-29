using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDTO>> GetProducts();
        Task<ProductDTO> GetProductById(int? id);
        Task<ProductDTO> Add(ProductDTO productDTO);
        Task Update(ProductDTO productDto);
        Task Remove(int? id);
        Task<IEnumerable<ProductDTO>> SearchAsync(ProductFilterDto filter);
        Task<IEnumerable<ProductDTO>> GetLowStockAsync(int threshold);
        Task UploadProductImageAsync(ProductImageUploadDto dto);
        Task<IEnumerable<ProductDTO>> SearchProductsAsync(string query, string sortBy, bool descending);
        Task<string> ExportProductsToCsvAsync();
    }
}
