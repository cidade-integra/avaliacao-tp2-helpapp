using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectsController : ControllerBase
    {
        #region Atributos

        private readonly IProjectService _service;

        #endregion

        #region Construtor

        public ProjectsController(IProjectService service)
        {
            _service = service;
        }

        #endregion

        #region Métodos/Endpoints

        [HttpPost]
        public async Task<ActionResult<ProjectDTO>> Post([FromBody] CreateProjectDTO createProjectDTO)
        {
            try
            {
                var result = await _service.CreateAsync(createProjectDTO);
                return CreatedAtAction(nameof(GetAll), new { id = result.Id }, result);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        #endregion
    }
}
