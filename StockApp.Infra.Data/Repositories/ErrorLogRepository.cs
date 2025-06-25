using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;

namespace StockApp.Infra.Data.Repositories
{
    public class ErrorLogRepository : IErrorLogRepository
    {
        #region Atributos

        private readonly ApplicationDbContext _context;

        #endregion

        #region Construtor

        public ErrorLogRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        #endregion

        #region Métodos

        public async Task SaveAsync(ErrorLog log)
        {
            _context.ErrorLogs.Add(log);
            await _context.SaveChangesAsync();
        }

        #endregion
    }
}
