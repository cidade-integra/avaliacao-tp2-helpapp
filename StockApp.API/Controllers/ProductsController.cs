using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;

namespace StockApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
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
    }
}