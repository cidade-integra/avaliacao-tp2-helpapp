using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceQuoteController : ControllerBase
    {
        #region Atributos

        private readonly IPriceQuoteService _quoteService;

        #endregion

        #region Construtor

        public PriceQuoteController(IPriceQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        #endregion

        #region Métodos

        [HttpGet("cryptoId")]
        public async Task<IActionResult> GetQuote(string cryptoId, [FromQuery] string currency = "usd")
        {
            try
            {
                var price = await _quoteService.GetPriceAsync(cryptoId, currency);
                return Ok( new {cryptoId, currency, price});
            }
            catch (HttpRequestException)
            {
                return StatusCode(503, "CoinGecko service unavailable.");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        #endregion
    }
}
