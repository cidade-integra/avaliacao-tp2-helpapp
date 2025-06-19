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
        private readonly ITaxCalculatorService _calculator;
        private readonly IMapper _mapper;

        public TaxController(ITaxCalculatorService calculadora)
        {
            _calculator = calculadora;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] TaxCalculationRequestDTO request)
        {
            var impostosDomain = _mapper.Map<IEnumerable<Tax>>(request.Impostos);

            var total = _calculator.Calculate(request.ValorBase, impostosDomain);

            return Ok(new { TotalImposto = total });
        }
    }
}
