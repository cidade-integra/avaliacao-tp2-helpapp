namespace StockApp.Application.DTOs
{
    public class TaxCalculationRequestDTO
    {
        public decimal ValorBase { get; set; }
        public IEnumerable<TaxDTO> Impostos { get; set; }
    }
}
