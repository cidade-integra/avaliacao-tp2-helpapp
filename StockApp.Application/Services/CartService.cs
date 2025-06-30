using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;

namespace StockApp.Application.Services
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        private readonly IProductRepository _productRepository;

        public CartService(ICartRepository cartRepository, IProductRepository productRepository)
        {
            _cartRepository = cartRepository;
            _productRepository = productRepository;
        }

        public async Task<CartDTO> GetCartAsync(int userId)
        {
            var cart = await _cartRepository.GetCartByUserIdAsync(userId);
            if (cart == null)
            {
                return new CartDTO { Items = new List<CartItemDTO>(), TotalPrice = 0 };
            }
            return new CartDTO
            {
                Items = cart.Items.Select(i => new CartItemDTO
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                }).ToList(),
                TotalPrice = cart.TotalPrice,
            };
        }

        public async Task AddToCartAsync(int userId, CartItemDTO itemDTO)
        {
            var product = await _productRepository.GetByIdAsync(itemDTO.ProductId);
            if (product == null) throw new Exception("Product not Found");

            var cartItem = new CartItem
            {
                ProductId = itemDTO.ProductId,
                Quantity = itemDTO.Quantity,
                Product = product,
            };

            await _cartRepository.AddItemToCartAsync(userId, cartItem);
        }

        public async Task UpdateCartItemAsync(int userId, CartItemDTO itemDTO)
        {
            if (itemDTO.Quantity <= 0)
            {
                await _cartRepository.RemoveItemFromCartAsync(userId, itemDTO.ProductId);
                return;
            }

            var product = await _productRepository.GetByIdAsync(itemDTO.ProductId);
            if (product == null) throw new Exception("Product not found");

            await _cartRepository.UpdateItemQuantityAsync(userId, itemDTO.ProductId, itemDTO.Quantity);
        }

        public async Task RemoveFromCartAsync(int userId, int productId)
        {
            await _cartRepository.RemoveItemFromCartAsync(userId, productId);
        }

        public async Task ClearCartAsync(int userId)
        {
            await _cartRepository.ClearCartAsync(userId);
        }
    }
}
