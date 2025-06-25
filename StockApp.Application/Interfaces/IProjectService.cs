using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IProjectService
    {
        Task<ProjectDTO> CreateAsync(CreateProjectDTO createProjectDTO);
        Task<IEnumerable<ProjectDTO>> GetAllAsync();
    }
}
