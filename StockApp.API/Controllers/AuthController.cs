using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador responsável pela autenticação de usuários.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        /// <summary>
        /// Realiza o login do usuário com e-mail e senha.
        /// </summary>
        /// <param name="request">Objeto contendo e-mail e senha do usuário.</param>
        /// <returns>Retorna um token de autenticação se o login for bem-sucedido, ou Unauthorized se falhar.</returns>
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _authService.AuthenticateAsync(request.Email, request.Password);

            if (result == null)
                return Unauthorized("E-mail ou senha inválidos");

            return Ok(result);
        }
    }

    /// <summary>
    /// Modelo de requisição para login.
    /// </summary>
    public class LoginRequest
    {
        /// <summary>
        /// E-mail do usuário.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Senha do usuário.
        /// </summary>
        public string Password { get; set; }
    }
}
