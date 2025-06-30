using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo cálculo de impostos.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class TaxController : ControllerBase
    {
        #region Atributos

        private readonly ITaxCalculatorService _calculator;
        private readonly IMapper _mapper;

        #endregion

        #region Construtor

        public TaxController(ITaxCalculatorService calculator, IMapper mapper)
        {
            _calculator = calculator;
            _mapper = mapper;
        }

        #endregion

        #region Métodos

        /// <summary>
        /// Calcula o valor total de impostos baseado no valor base e nas taxas informadas.
        /// </summary>
        /// <param name="request">Objeto contendo o valor base e as taxas a serem aplicadas.</param>
        /// <returns>Retorna o valor total dos impostos calculados.</returns>
        /// <response code="200">Cálculo realizado com sucesso.</response>
        /// <response code="400">Payload inválido ou incompleto.</response>
        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] TaxCalculationRequestDTO request)
        {
            if (request == null || request.Taxes == null)
                return BadRequest("Invalid payload.");

            var domainTaxes = _mapper.Map<IEnumerable<Tax>>(request.Taxes);

            var total = _calculator.Calculate(request.BaseAmount, domainTaxes);

            return Ok(new { TotalTax = total });
        }

        #endregion
    }
}