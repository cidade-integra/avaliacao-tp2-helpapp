using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.API.Controllers
{
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