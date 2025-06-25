using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using StockApp.Domain.Entities;
using Microsoft.Extensions.Logging;


namespace StockApp.Infra.Data.Services
{
    public class UserAuditService : IUserAuditService
    {
        private readonly ILogger<UserAuditService> _logger;
        private readonly ApplicationDbContext _context;

        public UserAuditService(ILogger<UserAuditService> logger, ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public void LogUserAction(string username, string action, string? details = null)
        {
            var logEntry = new UserAuditLog
            {
                Username = username,
                Action = action,
                Details = details,
                Timestamp = DateTime.UtcNow,
            };
            _context.UserAuditLog.Add(logEntry);
            _context.SaveChanges();

            _logger.LogInformation(
                "[AUDIT] User: {Username}, Action: {Action}, Details: {Details}",
                username, action, details);
        }
    }
}
