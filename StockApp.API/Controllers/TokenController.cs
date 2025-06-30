using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Stockapp.Application.Interfaces;
using StockApp.Application.DTOs;
using StockApp.Infra.Data.Context;
using System.Threading.Tasks;

namespace StockApp.Api.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação e geração de tokens JWT.
    /// </summary>
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

        /// <summary>
        /// Autentica um usuário e gera um token JWT válido.
        /// </summary>
        /// <param name="loginDto">Dados de login contendo email e senha.</param>
        /// <returns>Token JWT em caso de sucesso ou erro 401 para credenciais inválidas.</returns>
        /// <response code="200">Token gerado com sucesso.</response>
        /// <response code="401">Credenciais inválidas.</response>
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