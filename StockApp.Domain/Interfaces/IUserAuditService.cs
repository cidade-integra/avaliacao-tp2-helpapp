using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Interfaces
{
    public interface IUserAuditService
    {
        public void LogUserAction(string username, string action, string? details = null);
    }
}
