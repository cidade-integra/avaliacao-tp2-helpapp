using Microsoft.AspNetCore.Mvc;
using StockApp.Application.Interfaces;
using StockApp.Application.DTOs;

namespace StockApp.API.Controllers
{
    [Route("/api/controller")]
    [ApiController]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UserRegisterDTO userRegisterDTO)
        {
            if(!ModelState.IsValid) { 
                return BadRequest(ModelState);}

            try
            {
                var result = await _userService.RegisterUserAsync(userRegisterDTO);

                if (result.Success)
                    return CreatedAtAction(nameof(GetUserById), new { id = result.UserId }, result);

                return BadRequest(result.Message);
            } catch (InvalidOperationException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}", Name = "GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById (int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if(user == null) 
                return NotFound("Usuário Não Encontrado.");

            return Ok(user);
        }
    }
}
