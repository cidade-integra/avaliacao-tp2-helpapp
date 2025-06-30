using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IFeedbackRepository
    {
        Task AdicionarAsync(Feedback feedback);

    }
}
