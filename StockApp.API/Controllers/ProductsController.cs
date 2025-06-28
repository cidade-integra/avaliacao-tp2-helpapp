using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
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

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] ProductFilterDto filter)
        {
            var result = await _productService.SearchAsync(filter);
            return Ok(result);

            [HttpPost("import")]
            async Task<IActionResult> ImportFromCsv(IFormFile file)
            {
                if (file == null || file.Length == 0)
                    return BadRequest("Arquivo inválido.");

                var count = await _productImportService.ImportFromCsvAsync(file.OpenReadStream());

                return Ok($"{count} produtos importados com sucesso.");
            }
        }

        [HttpGet("low-stock")]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetLowStock([FromQuery] int threshold)
        {
            var products = await _productService.GetLowStockAsync(threshold);
            return Ok(products);
        }
    }
}