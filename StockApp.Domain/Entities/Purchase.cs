﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Domain.Entities
{
    public class Purchase
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int SupplierId { get; set; }
 
        public Product Product { get; set; }
        public Supplier Supplier { get; set; }
    }
}
