using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IUserAuditLogRepository
    {
        Task SaveAsync(UserAuditLog log);
    }
}
