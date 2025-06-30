using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IProjectRepository
    {
        Task AddAsync(Project project);
        Task<IEnumerable<Project>> GetAllAsync();
    }
}
