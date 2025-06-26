using StockApp.Domain.Entities;
using System.Threading.Tasks;

namespace StockApp.Application.Interfaces.IRepository
{
    public interface IFeedbackRepository
    {
        Task AdicionarAsync(Feedback feedback);

    }
}
