using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class DashboardPurchasesDto
    {
        public int TotalPurchases { get; set; }
        public decimal TotalSpent { get; set; }
        public IEnumerable<SupplierPurchasesDto> TopSuppliers { get; set; }
    }
}
