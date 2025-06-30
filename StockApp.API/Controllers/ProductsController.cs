using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Text;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pela gestão de produtos no sistema.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductImportService _productImportService;

        public ProductsController(IProductService productService, IProductImportService productImportService)
        {
            _productService = productService;
            _productImportService = productImportService;
        }

        /// <summary>
        /// Cadastra um novo produto.
        /// </summary>
        /// <param name="productDto">Dados do produto.</param>
        /// <returns>Produto criado.</returns>
        [HttpPost]
        public async Task<ActionResult<ProductDTO>> Create(ProductDTO productDto)
        {
            if (productDto == null)
            {
                return BadRequest("Invalid data.");
            }

            var product = await _productService.Add(productDto);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        /// <summary>
        /// Retorna um produto pelo ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <returns>Produto encontrado ou erro 404.</returns>
        [HttpGet("{id}", Name = "GetById")]
        public async Task<ActionResult<ProductDTO>> GetProductById(int id)
        {
            var product = await _productService.GetProductById(id);

            if (product == null)
            {
                return NotFound("Product not found");
            }
            return Ok(product);
        }

        /// <summary>
        /// Atualiza os dados de um produto.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="productDto">Dados atualizados.</param>
        /// <returns>Status da operação.</returns>
        [HttpPut]
        public async Task<IActionResult> Update(int id, ProductDTO productDto)
        {
            if (id != productDto.Id)
            {
                return BadRequest();
            }
            if (productDto == null)
            {
                return BadRequest("Invalid data.");
            }
            await _productService.Update(productDto);
            return NoContent();
        }

        /// <summary>
        /// Retorna todos os produtos.
        /// </summary>
        /// <returns>Lista de produtos.</returns>
        [HttpGet]
        [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        /// <summary>
        /// Realiza uma busca simples com filtros.
        /// </summary>
        /// <param name="filter">Parâmetros de filtragem.</param>
        /// <returns>Lista de produtos filtrados.</returns>
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] ProductFilterDto filter)
        {
            var result = await _productService.SearchAsync(filter);
            return Ok(result);
        }

        /// <summary>
        /// Importa produtos a partir de um arquivo CSV.
        /// </summary>
        /// <param name="file">Arquivo CSV contendo os produtos.</param>
        /// <returns>Quantidade de produtos importados.</returns>
        [HttpPost("import")]
        public async Task<IActionResult> ImportFromCsv(IFormFile file)
        {
            if (file == null || file.Length == 0)
                return BadRequest("Arquivo inválido.");

            var count = await _productImportService.ImportFromCsvAsync(file.OpenReadStream());

            return Ok($"{count} produtos importados com sucesso.");
        }

        /// <summary>
        /// Retorna produtos com estoque abaixo de um limite informado.
        /// </summary>
        /// <param name="threshold">Valor limite do estoque.</param>
        /// <returns>Lista de produtos com estoque baixo.</returns>
        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetLowStock([FromQuery] int threshold)
        {
            var products = await _productService.GetLowStockAsync(threshold);
            return Ok(products);
        }

        /// <summary>
        /// Realiza o upload de imagem de um produto.
        /// </summary>
        /// <param name="dto">Objeto contendo a imagem e o ID do produto.</param>
        /// <returns>Status da operação.</returns>
        [HttpPost("upload-image")]
        public async Task<IActionResult> UploadImage([FromForm] ProductImageUploadDto dto)
        {
            try
            {
                await _productService.UploadProductImageAsync(dto);
                return Ok(new { message = "Imagem enviada com sucesso!" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }

        /// <summary>
        /// Realiza uma busca avançada com ordenação personalizada.
        /// </summary>
        /// <param name="query">Texto de busca.</param>
        /// <param name="sortBy">Campo de ordenação (padrão: name).</param>
        /// <param name="descending">Indica se a ordenação será decrescente.</param>
        /// <returns>Lista de produtos encontrados.</returns>
        [HttpGet("advanced-search")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> AdvancedSearch(
            [FromQuery] string query,
            [FromQuery] string sortBy = "name",
            [FromQuery] bool descending = false)
        {
            var products = await _productService.SearchProductsAsync(query, sortBy, descending);
            return Ok(products);
        }

        /// <summary>
        /// Exporta todos os produtos para um arquivo CSV.
        /// </summary>
        /// <returns>Arquivo CSV com os produtos.</returns>
        [HttpGet("export")]
        public async Task<IActionResult> ExportToCsv()
        {
            var csvData = await _productService.ExportProductsToCsvAsync();
            var bytes = Encoding.UTF8.GetBytes(csvData);
            return File(bytes, "text/csv", "products.csv");
        }
    }
}