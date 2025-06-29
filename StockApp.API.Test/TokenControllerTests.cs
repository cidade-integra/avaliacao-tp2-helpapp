using Microsoft.AspNetCore.Mvc;
using Moq;
using StockApp.API.Controllers;
using StockApp.Application.Interfaces;
using StockApp.Application.DTOs;


namespace StockApp.API.Test
{
    public class TokenControllerTests
    {
        [Fact]
        public async Task Login_ValidCredentials_ReturnsToken()
        {
            // Arrange
            var authServiceMock = new Mock<IAuthService>();
            var tokenController = new AuthController(authServiceMock.Object);
            authServiceMock.Setup(service => service.AuthenticateAsync(
                It.IsAny<string>(),
                It.IsAny<string>())).Returns(Task.FromResult(new StockApp.Application.DTOs.TokenResponseDto
                {
                    Token = "token",
                    Expiration = DateTime.UtcNow.AddMinutes(60)
                }));

            var userLoginDto = new StockApp.API.Controllers.LoginRequest
            {
                Email = "testuser",
                Password = "password"
            };

            // Act
            var result = await tokenController.Login(userLoginDto) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.IsType<StockApp.Application.DTOs.TokenResponseDto>(result.Value);
        }
    }
}