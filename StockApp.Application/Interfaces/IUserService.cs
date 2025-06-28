using StockApp.Application.DTOs;

namespace StockApp.Application.Interfaces
{
    public interface IUserService
    {
        Task<RegisterResult>RegisterUserAsync(UserRegisterDTO userDTO);
        Task<UserDTO> GetUserByIdAsync(int id);
        //Task<LoginResult> LoginAsync(UserLoginDTO userLogin);
    }

    public class RegisterResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int UserId { get; set; }
    }

    public class LoginResult
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Message { get; set; }
    }
}
