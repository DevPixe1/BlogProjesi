using Blog.Core.DTOs;
using Blog.Core.Enums;
using Blog.Core.Interfaces;
using Blog.Core.Services;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;
        private readonly IUserService _userService;

        public AuthController(IJwtService jwtService, IUserService userService)
        {
            _jwtService = jwtService;
            _userService = userService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            // Gerçek kullanıcı doğrulaması yapılır
            var user = await _userService.AuthenticateAsync(loginDto.Username, loginDto.Password);

            if (user == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }
    }
}
