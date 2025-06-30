using StockApp.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    public interface ISupplierRepository
    {
        Task<IEnumerable<Supplier>> GetSuppliers();
        Task<Supplier> GetById(int id);
        Task<Supplier> Create(Supplier supplier);
        Task<Supplier> Update(Supplier supplier);
        Task<Supplier> Remove(int id);
        Task<IEnumerable<Supplier>> Search(string name, string contactEmail);

    }
}
