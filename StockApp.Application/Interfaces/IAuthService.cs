using Application.DTOs;

namespace Application.Interfaces
{
    public interface IAuthService
    {
        Task<TokenResponseDto> AuthenticateAsync(string email, string password);
    }
}
