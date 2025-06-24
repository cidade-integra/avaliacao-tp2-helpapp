using StockApp.Application.DTOs;
using StockApp.Domain.Entities;

namespace Stockapp.Application.Interfaces
{
    public interface ITokenService
    {
        TokenResponseDto GenerateToken(User user);
    }
}
