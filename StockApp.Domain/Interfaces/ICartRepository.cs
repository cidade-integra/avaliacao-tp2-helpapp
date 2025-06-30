using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface ICartRepository
    {
        Task<Cart> GetCartByUserIdAsync (int userId);
        Task AddItemToCartAsync (int userId, CartItem item);
        Task UpdateItemQuantityAsync(int userId, int productId, int quantity);
        Task RemoveItemFromCartAsync(int userId, int productId);
        Task ClearCartAsync(int userId);
    }
}
