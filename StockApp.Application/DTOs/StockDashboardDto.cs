using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class StockDashboardDto
    {
        public int TotalProducts { get; set; }
        public int TotalItemsInStock { get; set; }
        public decimal TotalStockValue { get; set; }
        public Dictionary<string, int> ProductsPerCategory { get; set; } = new();
        public List<string> OutOfStockProducts { get; set; } = new();
    }
}
