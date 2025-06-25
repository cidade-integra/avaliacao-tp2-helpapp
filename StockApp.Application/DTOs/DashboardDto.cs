using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockApp.Application.DTOs
{
    public class DashboardDto
    {
        public int TotalVendas { get; set; }
        public decimal ReceitaTotal { get; set; }
        public string ProdutoMaisVendido { get; set; }
        public Dictionary<string, int> VendasPorCategoria { get; set; }
    }
}
