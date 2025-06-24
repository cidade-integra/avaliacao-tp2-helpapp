using Application.DTOs;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        TokenResponseDto GenerateToken(User user);
    }
}
