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
    }

}
