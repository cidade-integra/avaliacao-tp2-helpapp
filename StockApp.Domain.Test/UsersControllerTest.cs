using Moq;
using Xunit;
using FluentAssertions;
using StockApp.API.Controllers;
using StockApp.Application.DTOs;
using StockApp.Application.Interfaces;
using System.Security.Principal;
using Microsoft.AspNetCore.Mvc;

namespace StockApp.Domain.Test
{
    public class UsersControllerTest
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly UserController _usersController;

        public UsersControllerTest()
        {
            _userServiceMock = new Mock<IUserService>();
            _usersController = new UserController(_userServiceMock.Object);
        }

        #region Testes Positivos
        [Fact(DisplayName = "Register User With Valid Data")]
        public async Task Register_UserWithValidData_returnsOK()
        {
            var validUser = new UserRegisterDTO
            {
                UserName = "userValid",
                Password = "Valid@123",
                Role = "User"
            };
            _userServiceMock.Setup(x => x.RegisterUserAsync(validUser))
                .ReturnsAsync(new RegisterResult
                {
                    Success = true,
                    UserId = 1,
                    Message = "User registred successfully"
                });

            var result = await _usersController.Register(validUser);

            result.Should().BeOfType<CreatedAtActionResult>().Which.StatusCode.Should().Be(201);
        }               

        [Fact(DisplayName = "Get User By Valid Id")]
        public async Task GetById_ExistingUser_returnsUser()
        {
            var userId = 1;
            var expectedUser = new UserDTO { Id = userId, UserName = "existingUser" };

            _userServiceMock.Setup(x => x.GetUserByIdAsync(userId)).ReturnsAsync(expectedUser);

            var result = await _usersController.GetUserById(userId);
            result.Should().BeOfType<ActionResult<UserDTO>>();

            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult.Value.Should().BeEquivalentTo(expectedUser);
        }
        #endregion

        #region Testes Negativos
        [Fact(DisplayName = "Register With Invalid Data")]
        public async Task Register_WithInvalidData_ReturnsBadRequest()
        {
            var invalidUser = new UserRegisterDTO
            {
                UserName = "",
                Password = "",
                Role = "InvalidRole"
            };
            _usersController.ModelState.AddModelError("Username", "Rquired");
            _usersController.ModelState.AddModelError("Password", "Rquired");

            var result = await _usersController.Register(invalidUser);

            result.Should().BeOfType<BadRequestObjectResult>();
        }
                
        [Fact(DisplayName = "Get User By Invalid Id")]
        public async Task GetById_NonExistingUser_ReturnsNotFound()
        {
            var invalidId = 999;
            _userServiceMock.Setup(x => x.GetUserByIdAsync(invalidId)).ReturnsAsync((UserDTO)null);

            var result =  await _usersController.GetUserById(invalidId);

            var actionResult = result.Should().BeAssignableTo<ActionResult<UserDTO>>().Subject;
            actionResult.Result.Should().BeOfType<NotFoundObjectResult>();
        }
        [Fact(DisplayName = "Register Duplicate Username")]
        public async Task Register_DuplicateUsername_ReturnsBadRequest()
        {
            var duplicateUser = new UserRegisterDTO
            {
                UserName = "userValid",
                Password = "Valid@123",
                Role = "User"
            };

            _userServiceMock.Setup(x => x.RegisterUserAsync(duplicateUser))
                .ThrowsAsync(new InvalidOperationException("Username already exists."));

            var result = await _usersController.Register(duplicateUser);

            var badRequestResult = result.Should().BeOfType<BadRequestObjectResult>().Subject;
            badRequestResult.Value.Should().Be("Username already exists."); // Verificação exata
        }
        [Theory(DisplayName = "Register With Short Password")]
        [InlineData("short")]
        [InlineData("123")]
        [InlineData("a")]
        public async Task Register_ShortPassword_ReturnsBadRequest(string shortPassword)
        {
            var invalidUser = new UserRegisterDTO
            {
                UserName = "userValid",
                Password = shortPassword,
                Role = "User"
            };

            _usersController.ModelState.AddModelError("Password", "Password must be at least 6 characters");

            var result = await _usersController.Register(invalidUser);

            result?.Should().BeOfType<BadRequestObjectResult>();
        }
        #endregion
    }
}
