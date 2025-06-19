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

        public TaxController(ITaxCalculatorService calculator, IMapper mapper)
        {
            _calculator = calculator;
            _mapper = mapper;
        }

        [HttpPost("calculate")]
        public IActionResult Calculate([FromBody] TaxCalculationRequestDTO request)
        {
            if (request == null || request.Taxes == null)
                return BadRequest("Invalid payload.");

            var domainTaxes = _mapper.Map<IEnumerable<Tax>>(request.Taxes);

            var total = _calculator.Calculate(request.BaseAmount, domainTaxes);

            return Ok(new { TotalTax = total });
        }
    }
}