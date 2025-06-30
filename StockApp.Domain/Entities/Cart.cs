using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class Cart
    {
        public int UserId { get; set; }
        public List<CartItem> Items { get; set;} = new List<CartItem>();
        public decimal TotalPrice => Items.Sum(item => item.Quantity * item.Product.Price);
    }
}
