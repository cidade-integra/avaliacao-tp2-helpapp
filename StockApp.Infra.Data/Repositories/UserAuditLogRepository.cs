using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.Infra.Data.Repositories
{
    public class UserAuditLogRepository : IUserAuditLogRepository
    {
        private readonly ApplicationDbContext _context;

        public UserAuditLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task SaveAsync(UserAuditLog log)
        {
            await _context.UserAuditLog.AddAsync(log);
            await _context.SaveChangesAsync();
        }
    }

}
