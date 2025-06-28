using StockApp.Domain.Entities;

namespace StockApp.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> UserExists(string email);

        Task<User> AddAsync(User user);

        Task<User> GetByIdAsync(int id);
        Task<User> GetByEmailAndPasswordAsync(string email, string password);
        Task UpdateAsync(User user);
    }
}
