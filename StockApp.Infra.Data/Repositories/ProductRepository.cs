using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using StockApp.Infra.Data.Context;
using System.Linq;

namespace StockApp.Infra.Data.Repositories
{
    public class ProductRepository : IProductRepository
    {
        ApplicationDbContext _productContext;
        private object _context;

        public ProductRepository(ApplicationDbContext context)
        {
            _productContext = context;
        }

        public ProductRepository()
        {
        }

        public async Task<Product> Create(Product product)
        {
            _productContext.Add(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> GetById(int? id)
        {
            return await _productContext.Products.FindAsync(id);
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            return await _productContext.Products.ToListAsync();
        }

        public async Task<Product> Remove(Product product)
        {
            _productContext.Remove(product);
            await _productContext.SaveChangesAsync();
            return product;
        }

        public async Task<Product> Update(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
            return product;
        }
        
        public IQueryable<Product> Query()
        {
            return _productContext.Products.Include(p => p.Category);
        }

        public async Task<IEnumerable<Product>> GetLowStockAsync(int threshold)
        {
            return await _productContext.Products.Where(p=>p.Stock<=threshold).ToListAsync();
        }
        
        public async Task UpdateAsync(Product product)
        {
            _productContext.Update(product);
            await _productContext.SaveChangesAsync();
        }

        public async Task<Product> GetByIdAsync(int productId)
        {
            return await _productContext.Products
                .Include(p => p.Category)
                .FirstOrDefaultAsync(p => p.Id == productId);
        }

        public async Task<IEnumerable<Product>> SearchAsync(string query, string sortBy, bool descending)
        {
            var productsQuery = _productContext.Products.AsQueryable();

            if (!string.IsNullOrWhiteSpace(query))
            {
                productsQuery = productsQuery.Where(p =>
                    p.Name.Contains(query) ||
                    p.Description.Contains(query));
            }

            // ordenação dinâmica
            productsQuery = (sortBy?.ToLower()) switch
            {
                "name" => descending ? productsQuery.OrderByDescending(p => p.Name) : productsQuery.OrderBy(p => p.Name),
                "price" => descending ? productsQuery.OrderByDescending(p => p.Price) : productsQuery.OrderBy(p => p.Price),
                "stock" => descending ? productsQuery.OrderByDescending(p => p.Quantity) : productsQuery.OrderBy(p => p.Quantity),
                _ => productsQuery.OrderBy(p => p.Name)
            };

            return await productsQuery.ToListAsync();
        }
    }
}