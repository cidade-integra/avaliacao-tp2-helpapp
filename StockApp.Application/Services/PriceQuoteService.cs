using StockApp.Application.Interfaces;
using System.Text.Json;

namespace StockApp.Application.Services
{
    public class PriceQuoteService : IPriceQuoteService
    {
        #region Atributos

        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://api.coingecko.com/api/v3/simple/price";

        #endregion

        #region Construtor 
        public PriceQuoteService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        #endregion

        #region Métodos

        public async Task<decimal> GetPriceAsync(string cryptoId, string currency = "usd")
        {
            var url = $"{BaseUrl}?ids={cryptoId}&vs_currencies={currency}";
            var response = await _httpClient.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();

            using var json = JsonDocument.Parse(content);

            var price = json.RootElement
                            .GetProperty(cryptoId.ToLower())
                            .GetProperty(currency.ToLower())
                            .GetDecimal();

            return price;

        }

        #endregion
    }
}
