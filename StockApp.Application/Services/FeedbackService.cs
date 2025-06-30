using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _repository;

        public FeedbackService(IFeedbackRepository repository)
        {
            _repository = repository;
        }

        public async Task EnviarFeedbackAsync(FeedbackDto dto)
        {
            var feedback = new Feedback
            {
                Mensagem = dto.Mensagem,
                DataEnvio = DateTime.UtcNow
            };

            await _repository.AdicionarAsync(feedback);
        }
    }
}

