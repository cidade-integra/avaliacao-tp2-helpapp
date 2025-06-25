using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class UserAuditLog
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Action {  get; set; }
        public string? Details { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
