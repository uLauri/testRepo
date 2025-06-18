using DeliveryApp.Controllers;
using DeliveryApp.Models.Authentication;
using DeliveryApp.Services;
using DeliveryApp.Tests.Helpers;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Tests.Controllers
{
    public class AuthentiactionControllerTests
    {
        [Fact]
        public void Login_WithValidCredentials_ReturnsOkWithToken()
        {
            var config = TestHelpers.CreateTestConfig();

            var jwtService = new JwtService(config);
            var controller = new AuthenticationController(jwtService);

            var loginRequest = new LoginRequest
            {
                Username = "user",
                Password = "password"
            };

            var result = controller.Login(loginRequest);

            var okResult = Assert.IsType<OkObjectResult>(result);
            var payload = Assert.IsType<TokenResponse>(okResult.Value);

            Assert.False(string.IsNullOrWhiteSpace(payload.Token));
        }

        [Fact]
        public void Login_WithInvalidCredentials_ReturnsUnauthorized()
        {
            var config = TestHelpers.CreateTestConfig();

            var jwtService = new JwtService(config);
            var controller = new AuthenticationController(jwtService);

            var invalidLogin = new LoginRequest
            {
                Username = "randomusername",
                Password = "randompassword"
            };

            var result = controller.Login(invalidLogin);

            var unauthorizedResult = Assert.IsType<UnauthorizedObjectResult>(result);
            var error = Assert.IsType<ErrorResponse>(unauthorizedResult.Value);

            Assert.Equal("Invalid username or password!", error.Messsage);
        }

        [Fact]
        public void Login_WithNullRequest_ReturnsBadRequest()
        {
            var config = TestHelpers.CreateTestConfig();

            var controller = new AuthenticationController(new JwtService(config));
            var result = controller.Login(null);

            Assert.IsType<BadRequestResult>(result);
        }
    }
}
