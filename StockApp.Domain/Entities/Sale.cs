using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class Sale
    {
        public int Id { get; set; }
        public int ProductId { get; set; } // FK
        public int Quantity { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime SaleDate { get; set; }

        public Product Product { get; set; } // Navigation property
    }
}
