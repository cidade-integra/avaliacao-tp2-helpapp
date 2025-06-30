using Microsoft.Extensions.Logging;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class UserAuditService : IUserAuditService
    {
        private readonly ILogger<UserAuditService> _logger;
        private readonly IUserAuditLogRepository _repository;

        public UserAuditService(ILogger<UserAuditService> logger, IUserAuditLogRepository repository)
        {
            _logger = logger;
            _repository = repository;
        }

        public async void LogUserAction(string username, string action, string? details = null)
        {
            var logEntry = new UserAuditLog
            {
                Username = username,
                Action = action,
                Details = details,
                Timestamp = DateTime.UtcNow
            };

            await _repository.SaveAsync(logEntry);

            _logger.LogInformation(
                "[AUDIT] User: {Username}, Action: {Action}, Details: {Details}",
                username, action, details);
        }
    }
}
