using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações relacionadas às categorias de produtos.
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        /// <summary>
        /// Retorna todas as categorias cadastradas.
        /// </summary>
        /// <returns>Lista de categorias.</returns>
        [HttpGet(Name = "GetCategories")]
        public async Task<ActionResult<IEnumerable<CategoryDTO>>> Get()
        {
            var categories = await _categoryService.GetCategories();
            if (categories == null)
            {
                return NotFound("Categories not found");
            }
            return Ok(categories);
        }

        /// <summary>
        /// Retorna uma categoria específica pelo ID.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <returns>Categoria correspondente ao ID.</returns>
        [HttpGet("{id:int}", Name = "GetCategory")]
        public async Task<ActionResult<CategoryDTO>> Get(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }
            return Ok(category);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        /// <param name="categoryDTO">Dados da nova categoria.</param>
        /// <returns>Categoria criada com status 201.</returns>
        [HttpPost(Name = "Create Category")]
        public async Task<ActionResult> Post([FromBody] CategoryDTO categoryDTO)
        {
            if (categoryDTO == null)
            {
                return BadRequest("Invalid data");
            }

            await _categoryService.Add(categoryDTO);

            return new CreatedAtRouteResult("GetCategory",
                new { id = categoryDTO.Id }, categoryDTO);
        }

        /// <summary>
        /// Atualiza os dados de uma categoria existente.
        /// </summary>
        /// <param name="id">ID da categoria.</param>
        /// <param name="categoryDTO">Novos dados da categoria.</param>
        /// <returns>Categoria atualizada.</returns>
        [HttpPut(Name = "Update Category")]
        public async Task<ActionResult> Put(int id, [FromBody] CategoryDTO categoryDTO)
        {
            if (id != categoryDTO.Id)
            {
                return BadRequest("Inconsistent ID");
            }

            if (categoryDTO == null)
            {
                return BadRequest("Update data invalid");
            }

            await _categoryService.Update(categoryDTO);

            return Ok(categoryDTO);
        }

        /// <summary>
        /// Remove uma categoria com base no ID.
        /// </summary>
        /// <param name="id">ID da categoria a ser removida.</param>
        /// <returns>Categoria removida.</returns>
        [HttpDelete("{id:int}", Name = "Delete Category")]
        public async Task<ActionResult<CategoryDTO>> Detele(int id)
        {
            var category = await _categoryService.GetCategoryById(id);
            if (category == null)
            {
                return NotFound("Category not found");
            }

            await _categoryService.Remove(id);

            return Ok(category);
        }
    }
}