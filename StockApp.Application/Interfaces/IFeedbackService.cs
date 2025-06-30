using StockApp.Application.DTOs;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces
{
    public interface IFeedbackService
    {
        Task EnviarFeedbackAsync(FeedbackDto dto);
    }
}
