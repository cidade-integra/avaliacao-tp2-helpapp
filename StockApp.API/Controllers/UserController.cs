using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using StockApp.Application.DTOs;
using System;
using System.Threading.Tasks;

namespace StockApp.API.Controllers
{
    /// <summary>
    /// Controlador para gerenciamento de usuários, incluindo registro e consulta.
    /// </summary>
    [Route("/api/[controller]")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        /// <summary>
        /// Registra um novo usuário.
        /// </summary>
        /// <param name="userRegisterDTO">Dados para cadastro do usuário.</param>
        /// <returns>Resultado da operação com detalhes do usuário criado.</returns>
        /// <response code="201">Usuário criado com sucesso.</response>
        /// <response code="400">Dados inválidos ou falha no registro.</response>
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var result = await _userService.RegisterUserAsync(userRegisterDTO);

                if (result.Success)
                    return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, result);

                return BadRequest(result.Message);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Obtém um usuário pelo ID.
        /// </summary>
        /// <param name="id">ID do usuário.</param>
        /// <returns>Dados do usuário solicitado.</returns>
        /// <response code="200">Usuário encontrado.</response>
        /// <response code="404">Usuário não encontrado.</response>
        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound("Usuário Não Encontrado.");

            return Ok(user);
        }
    }
}