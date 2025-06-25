using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace StockApp.Infra.Data.Repositories
{
    public class SupplierRepository : ISupplierRepository
    {
        private readonly ApplicationDbContext _supplierContext;

        public SupplierRepository(ApplicationDbContext _context)
        {
            _supplierContext = _context;
        }
        public async Task<Supplier> Create(Supplier supplier)
        {
            _supplierContext.Add(supplier);
            await _supplierContext.SaveChangesAsync();
            return supplier;
        }
        public async Task<Supplier> GetById(int id)
        {
            return await _supplierContext.Suppliers.FindAsync(id);
        }
        public async Task<IEnumerable<Supplier>> GetSuppliers()
        {
            return await _supplierContext.Suppliers.ToListAsync();
        }
        public async Task<Supplier> Remove(int id)
        {
            var supplier = await _supplierContext.Suppliers.FindAsync();
            if (supplier != null)
            {
                _supplierContext.Remove(supplier);
                await _supplierContext.SaveChangesAsync();
                return supplier;
            }
            throw new KeyNotFoundException($"Supplier with id {id} not found.");
        }
        public async Task<Supplier> Update(Supplier supplier)
        {
            _supplierContext.Update(supplier);
            await _supplierContext.SaveChangesAsync();
            return supplier;
        }
    }
}
