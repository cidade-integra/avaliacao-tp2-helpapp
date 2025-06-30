using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável por consultar cotações de criptomoedas.
    /// </summary>
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

        /// <summary>
        /// Consulta a cotação atual de uma criptomoeda em uma moeda específica.
        /// </summary>
        /// <param name="cryptoId">Identificador da criptomoeda (ex: bitcoin).</param>
        /// <param name="currency">Moeda desejada para conversão (padrão: usd).</param>
        /// <returns>Retorna o preço da criptomoeda na moeda solicitada.</returns>
        /// <response code="200">Cotação retornada com sucesso.</response>
        /// <response code="400">Erro na requisição (ex: parâmetros inválidos).</response>
        /// <response code="503">Serviço externo (CoinGecko) indisponível.</response>
        [HttpGet("cryptoId")]
        public async Task<IActionResult> GetQuote(string cryptoId, [FromQuery] string currency = "usd")
        {
            try
            {
                var price = await _quoteService.GetPriceAsync(cryptoId, currency);
                return Ok(new { cryptoId, currency, price });
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