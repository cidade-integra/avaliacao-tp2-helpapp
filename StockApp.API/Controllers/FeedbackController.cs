using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> EnviarFeedback([FromBody] FeedbackDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Mensagem))
                return BadRequest("Mensagem é obrigatória.");

            await _service.EnviarFeedbackAsync(dto);
            return Ok("Feedback enviado com sucesso!");
        }
    }
}
