using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IErrorLogRepository
    {
        Task SaveAsync(ErrorLog log);
    }
}
