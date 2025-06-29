using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        [HttpGet]
        public async Task<ActionResult<CartDTO>> GetCart()
        {
            var userId = 1;
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        [HttpPost("items")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDTO item)
        {
            var userId = 1;
            await _cartService.AddToCartAsync(userId, item);
            return Ok();
        }

        [HttpPut("items")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDTO item)
        {
            var userId = 1;
            await _cartService.UpdateCartItemAsync(userId, item);
            return Ok();
        }

        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> RemoveFromCart (int productId)
        {
            var userId = 1;
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok();
        }

        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = 1;
            await _cartService.ClearCartAsync(userId);
            return Ok();
        }
    }
}
