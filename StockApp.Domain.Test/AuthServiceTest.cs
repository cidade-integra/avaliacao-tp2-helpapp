using Moq;
using StockApp.Application.DTOs;
using StockApp.Application.Services;
using StockApp.Application.Settings;
using StockApp.Domain.Entities;
using StockApp.Domain.Interfaces;
using Microsoft.Extensions.Options;
using Xunit;
using BCrypt.Net;
using System;
namespace StockApp.Domain.Test
{
    public class AuthServiceUnitTest
    {
        [Fact]
        public async Task AuthenticateAsync_ValidCredentials_ReturnsToken()
        {

            var userRepositoryMock = new Mock<IUserRepository>();
            var jwtSettings = new JwtSettings
            {
                SecretKey = "uK3r9@Lf92!qWxZ7#TpMn3$gVyBcL6^DfJqL!r29",
                Issuer = "StockApp",
                Audience = "StockAppUsers",
                ExpirationMinutes = 60
            };

            var optionsMock = new Mock<IOptions<JwtSettings>>();

            optionsMock.Setup(o => o.Value).Returns(jwtSettings);

            var authService = new AuthService(userRepositoryMock.Object, optionsMock.Object);

            userRepositoryMock.Setup(repo => repo.GetByEmailAndPasswordAsync(
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync(
                new User("testuser", "testuser@user.com", BCrypt.Net.BCrypt.HashPassword("password"))
                {
                    Id = 1,
                    Role = "User",
                    Email = "testuser@user.com"
                });

            var result = await authService.AuthenticateAsync("testuser@user.com",
            "password");

            Assert.NotNull(result);
            Assert.IsType<TokenResponseDto>(result);
            Assert.False(string.IsNullOrEmpty(result.Token));
        }
        [Fact]
        public async Task AuthenticateAsync_InvalidCredentials_ReturnsNull()
        {

            var userRepositoryMock = new Mock<IUserRepository>();

            var jwtSettings = new JwtSettings
            {
                SecretKey = "uK3r9@Lf92!qWxZ7#TpMn3$gVyBcL6^DfJqL!r29",
                Issuer = "StockApp",
                Audience = "StockAppUsers",
                ExpirationMinutes = 60
            };

            var optionsMock = new Mock<IOptions<JwtSettings>>();

            optionsMock.Setup(o => o.Value).Returns(jwtSettings);

            var authService = new AuthService(userRepositoryMock.Object,optionsMock.Object);
            userRepositoryMock.Setup(repo => repo.GetByEmailAndPasswordAsync(
                It.IsAny<string>(),
                It.IsAny<string>())).ReturnsAsync((User)null);

            await Assert.ThrowsAsync<StockApp.Domain.Validation.AuthenticationException>(() =>
            authService.AuthenticateAsync("teste@user.com", "testedesenha"));
        }

        [Theory]
        [InlineData(null, "password", "Email é Obrigatório")]
        [InlineData("testuser@user.com", null, "Senha é Obrigatória")]
        public async Task AuthenticateAsync_InvalidInput_ThrowsDomainExceptionValidation(string email, string password, string expectedMessage)
        {
            
            var userRepositoryMock = new Mock<IUserRepository>();

            var jwtSettings = new JwtSettings
            {
                SecretKey = "uK3r9@Lf92!qWxZ7#TpMn3$gVyBcL6^DfJqL!r29",
                Issuer = "StockApp",
                Audience = "StockAppUsers",
                ExpirationMinutes = 60
            };

            var optionsMock = new Mock<IOptions<JwtSettings>>();

            optionsMock.Setup(o => o.Value).Returns(jwtSettings);

            var authService = new AuthService(userRepositoryMock.Object, optionsMock.Object);

            var exception = await Assert.ThrowsAsync<StockApp.Domain.Validation.DomainExceptionValidation>(() =>
            authService.AuthenticateAsync(email, password));

            Assert.Equal(expectedMessage, exception.Message);
        }
    }
}