using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using StockApp.Application.Settings;
using StockApp.Domain.Interfaces;
using StockApp.Domain.Validation;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StockApp.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _userRepository;
        private readonly JwtSettings _jwtSettings;

        public AuthService(IUserRepository userRepository, IOptions<JwtSettings> jwtSettings)
        {
            _userRepository = userRepository;
            _jwtSettings = jwtSettings.Value;
        }

        public async Task<TokenResponseDto> AuthenticateAsync(string email, string password)
        {
            DomainExceptionValidation.When(string.IsNullOrEmpty(email), "Email é Obrigatório");
            DomainExceptionValidation.When(string.IsNullOrEmpty(password), "Senha é Obrigatória");

            var user = await _userRepository.GetByEmailAndPasswordAsync(email, password);

            AuthenticationException.ThrowIf(user == null, "Usuário não Encontrado");

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim("id", user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddMinutes(_jwtSettings.ExpirationMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: expiration,
                signingCredentials: creds
            );

            return new TokenResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }
    }

}
