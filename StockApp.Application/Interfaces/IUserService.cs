using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResult>RegisterUserAsync(UserRegisterDTO userDTO);
        Task<UserDTO> GetUserByIdAsync(int id);
    }

    public class RegisterResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }
}
