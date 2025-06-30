using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelo envio de feedbacks dos usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _service;

        public FeedbackController(IFeedbackService service)
        {
            _service = service;
        }

        /// <summary>
        /// Envia um feedback para o sistema.
        /// </summary>
        /// <param name="dto">Objeto contendo a mensagem de feedback.</param>
        /// <returns>Retorna uma mensagem de sucesso ou erro de validação.</returns>
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
