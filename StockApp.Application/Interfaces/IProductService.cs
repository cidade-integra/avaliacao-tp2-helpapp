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
    }
}
