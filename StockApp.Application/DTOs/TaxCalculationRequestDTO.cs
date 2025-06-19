namespace StockApp.Application.DTOs
{
    public class TaxCalculationRequestDTO
    {
        #region Atributos

        public decimal BaseAmount { get; set; }
        public IEnumerable<TaxDTO> Taxes { get; set; }

        #endregion
    }
}
