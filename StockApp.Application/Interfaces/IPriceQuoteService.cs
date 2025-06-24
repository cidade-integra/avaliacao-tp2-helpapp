namespace StockApp.Application.Interfaces
{
    public interface IPriceQuoteService
    {
        Task<decimal> GetPriceAsync(string cryptoId, string currency = "usd");
    }
}
