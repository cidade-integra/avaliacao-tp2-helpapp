using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetCartAsync (int id);
        Task AddToCartAsync(int userId, CartItemDTO Item);
        Task UpdateCartItemAsync(int userId, CartItemDTO Item);
        Task RemoveFromCartAsync(int userId, int productId);
        Task ClearCartAsync(int useriId);
    }
}
