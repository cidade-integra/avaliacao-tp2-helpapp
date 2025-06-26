using StockApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using StockApp.Infra.Data.Context;
using StockApp.Application.Interfaces.IRepository;

namespace StockApp.Infra.Data.Repositories
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly ApplicationDbContext _context;

        public FeedbackRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Feedback feedback)
        {
            await _context.Feedbacks.AddAsync(feedback);
            await _context.SaveChangesAsync();
        }
    }
}
