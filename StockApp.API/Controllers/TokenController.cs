using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stockapp.Application.Interfaces;
using StockApp.Application.DTOs;
using StockApp.Infra.Data.Context;

namespace StockApp.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TokenController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ITokenService _tokenService;

        public TokenController(ApplicationDbContext context, ITokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequestDto loginDto)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Email == loginDto.Email && u.PasswordHash == loginDto.Password);

            if (user == null)
                return Unauthorized("Credenciais inválidas.");

            var tokenResponse = _tokenService.GenerateToken(user);

            return Ok(tokenResponse);
        }
    }
}
