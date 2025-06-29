using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProducts();

        Task<Product> GetById(int? id);
        Task<Product> Create(Product product);
        Task<Product> Update(Product product);
        Task<Product> Remove(Product product);
        IQueryable<Product> Query();
        Task<IEnumerable<Product>> GetLowStockAsync(int threshold);
        Task UpdateAsync(Product product); 
        Task<Product> GetByIdAsync(int productId);
        Task<IEnumerable<Product>> SearchAsync(string query, string sortBy, bool descending);
    }
}
