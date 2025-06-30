using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pela criação e listagem de projetos.
    /// </summary>
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

        /// <summary>
        /// Cria um novo projeto.
        /// </summary>
        /// <param name="createProjectDTO">Dados do projeto a ser criado.</param>
        /// <returns>Projeto criado com status 201.</returns>
        /// <response code="201">Projeto criado com sucesso.</response>
        /// <response code="400">Dados inválidos para criação do projeto.</response>
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

        /// <summary>
        /// Retorna todos os projetos cadastrados.
        /// </summary>
        /// <returns>Lista de projetos.</returns>
        /// <response code="200">Lista retornada com sucesso.</response>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProjectDTO>>> GetAll()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        #endregion
    }
}