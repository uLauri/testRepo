using DeliveryApp.Models.Authentication;
using DeliveryApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DeliveryApp.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly JwtService _jwtService;

        public AuthenticationController(JwtService jwtService)
        {
            _jwtService = jwtService;
        }
        /// <summary>
        /// Endpoint to login by username and password for JWT retrieval. Currently only returning JWT to default user with username: user and pw: password
        /// Key for token generation is kept in appsettings.json file.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [AllowAnonymous]
        public IActionResult Login([FromBody] LoginRequest? request)
        {
            if (request == null)
                return BadRequest();

            if (request.Username == "user" && request.Password == "password")
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new TokenResponse { Token = token });
            } 
            return Unauthorized(new ErrorResponse { Messsage = "Invalid username or password!" });
        }
    }
}
