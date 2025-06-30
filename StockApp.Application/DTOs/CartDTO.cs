using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class CartDTO
    {
        public List<CartItemDTO> Items { get; set; }
        public Decimal TotalPrice { get; set; }
    }
}
