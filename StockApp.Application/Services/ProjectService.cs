using AutoMapper;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IMapper _mapper;
        private readonly IProjectRepository _repository;

        public ProjectService(IProjectRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public async Task<ProjectDTO> CreateAsync(CreateProjectDTO createProjectDTO)
        {
            var project = new Project(createProjectDTO.Name, createProjectDTO.Description, createProjectDTO.StartDate, createProjectDTO.EndDate);
            await _repository.AddAsync(project);
            return _mapper.Map<ProjectDTO>(project);
        }

        public async Task<IEnumerable<ProjectDTO>> GetAllAsync()
        {
            var projects = await _repository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProjectDTO>>(projects);
        }
    }
}
