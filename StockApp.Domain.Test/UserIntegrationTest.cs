using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using StockApp.Application.DTOs;
using System.Net.Http.Json;

namespace StockApp.Domain.Test
{
    public class UserIntegrationTest : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _httpClient;

        public UserIntegrationTest(WebApplicationFactory<Program> factory)
        {
            _httpClient = factory.CreateClient();
        }

        [Fact(DisplayName = "Register and Login Valid Credentials")]
        public async Task RegisterAndLogin_ValidCredentials_ReturnsToken()
        {
            var login = new UserLoginDTO
            {
                Email = "valid@test.com",
                Password = "valid@123"
            };

            var register = new UserRegisterDTO
            {
                UserName = "UserValid",
                Password = "Valid@123",
                Role = "User"
            };

            var loginResponse = await _httpClient.PostAsJsonAsync("/api/token/login", login);
            loginResponse.EnsureSuccessStatusCode();

            var resiterResponse = await _httpClient.PostAsJsonAsync("/api/token/register", register);
            resiterResponse.EnsureSuccessStatusCode();

            var tokenResponse = await loginResponse.Content.ReadFromJsonAsync<TokenResponseDto>();

            Assert.NotNull(tokenResponse);
            Assert.NotNull(tokenResponse.Token);
            Assert.True(tokenResponse.Expiration > DateTime.UtcNow);
        }
    }
}
