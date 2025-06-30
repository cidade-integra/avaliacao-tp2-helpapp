using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class ProductFilterDto
    {
        public string? Name { get; set; }
        public string? Category { get; set; }
        public int? MinQuantity { get; set; }
        public int? MaxQuantity { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
    }
}
