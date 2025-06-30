using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using StockApp.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace StockApp.Infra.Data.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task <Cart> GetCartByUserIdAsync(int userId)
        {
            return await _context.Carts
                .Include(c => c.Items)
                .ThenInclude(i => i.Product)
                .FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task AddItemToCartAsync(int userId, CartItem item)
        {
            var cart = await GetCartByUserIdAsync(userId);

            if (cart == null)
            {
                cart = new Cart
                {
                    UserId = userId,
                    Items = new List<CartItem> { item }
                };
                _context.Carts.Add(cart);
            }
            else
            {
                var existingItem = cart.Items.FirstOrDefault(i => i.ProductId == item.ProductId);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                else
                {
                    cart.Items.Add(item);
                }
            }

            await _context.SaveChangesAsync();
        }

        public async Task UpdateItemQuantityAsync(int userId, int productId, int quantity)
        {
            var cart = await GetCartByUserIdAsync (userId);
            if (cart == null) throw new Exception("Cart not Found");

            var item = cart.Items.FirstOrDefault(i =>i.ProductId == productId);
            if (item == null) throw new Exception("Item not found in cart");

            item.Quantity = quantity;
            await _context.SaveChangesAsync();
        }

        public async Task RemoveItemFromCartAsync(int userId, int productId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart == null) throw new Exception("Cart not found");

            var item = cart.Items.FirstOrDefault(i => i.ProductId == productId);
            if (item != null)
            {
                cart.Items.Remove(item);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ClearCartAsync(int userId)
        {
            var cart = await GetCartByUserIdAsync(userId);
            if (cart != null)
            {
                cart.Items.Clear();
                await _context.SaveChangesAsync();
            }
        }
    }
}
