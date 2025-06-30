using Microsoft.AspNetCore.Mvc;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pelas operações do carrinho de compras.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CartsController : ControllerBase
    {
        private readonly ICartService _cartService;

        public CartsController(ICartService cartService)
        {
            _cartService = cartService;
        }

        /// <summary>
        /// Retorna os itens do carrinho de compras do usuário.
        /// </summary>
        /// <returns>Objeto contendo os itens do carrinho.</returns>
        [HttpGet]
        public async Task<ActionResult<CartDTO>> GetCart()
        {
            var userId = 1;
            var cart = await _cartService.GetCartAsync(userId);
            return Ok(cart);
        }

        /// <summary>
        /// Adiciona um item ao carrinho de compras do usuário.
        /// </summary>
        /// <param name="item">Informações do item a ser adicionado.</param>
        /// <returns>Retorna sucesso se o item for adicionado corretamente.</returns>
        [HttpPost("items")]
        public async Task<IActionResult> AddToCart([FromBody] CartItemDTO item)
        {
            var userId = 1;
            await _cartService.AddToCartAsync(userId, item);
            return Ok();
        }

        /// <summary>
        /// Atualiza a quantidade de um item no carrinho de compras.
        /// </summary>
        /// <param name="item">Item com a nova quantidade.</param>
        /// <returns>Retorna sucesso se o item for atualizado corretamente.</returns>
        [HttpPut("items")]
        public async Task<IActionResult> UpdateCartItem([FromBody] CartItemDTO item)
        {
            var userId = 1;
            await _cartService.UpdateCartItemAsync(userId, item);
            return Ok();
        }

        /// <summary>
        /// Remove um item do carrinho de compras com base no ID do produto.
        /// </summary>
        /// <param name="productId">ID do produto a ser removido.</param>
        /// <returns>Retorna sucesso se o item for removido.</returns>
        [HttpDelete("items/{productId}")]
        public async Task<IActionResult> RemoveFromCart(int productId)
        {
            var userId = 1;
            await _cartService.RemoveFromCartAsync(userId, productId);
            return Ok();
        }

        /// <summary>
        /// Remove todos os itens do carrinho de compras do usuário.
        /// </summary>
        /// <returns>Retorna sucesso após limpar o carrinho.</returns>
        [HttpDelete("clear")]
        public async Task<IActionResult> ClearCart()
        {
            var userId = 1;
            await _cartService.ClearCartAsync(userId);
            return Ok();
        }
    }
}