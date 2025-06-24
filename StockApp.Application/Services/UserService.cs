using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using BCrypt.Net;
using System.Data;

namespace StockApp.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<RegisterResult> RegisterUserAsync(UserRegisterDTO userDTO)
        {
            if (userDTO == null) 
                return new RegisterResult { Success = false, Message = "Dados Inválidos." };
            if (await _userRepository.UserExists(userDTO.Email))
                return new RegisterResult { Success = false, Message = "E-mail já cadastrado." };

            var user = new User
            (
                userName: userDTO.UserName,
                email: userDTO.Email,
                passwordHash: BCrypt.Net.BCrypt.HashPassword(userDTO.Password)
            );

            await _userRepository.AddAsync(user);

            return new RegisterResult
            {
                Success = true,
                UserId = user.Id,
                Message = "Usuário registrado com sucesso!"
            };
        }

        public async Task<UserDTO> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return new UserDTO
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Role = user.Role,
            };
        }        
    }
}
