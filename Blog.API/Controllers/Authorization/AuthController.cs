using Blog.Core.DTOs;
using Blog.Core.Enums;
using Blog.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Blog.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IJwtService _jwtService;

        public AuthController(IJwtService jwtService)
        {
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginDto loginDto)
        {
            // Gerçek uygulamada burada kullanıcı veritabanından doğrulanmalı
            if (loginDto.Username == "testuser" && loginDto.Password == "1234")
            {
                // Diyelim ki bu bilgilerle bir kullanıcı bulundu:
                var user = new UserDto
                {
                    Id = Guid.NewGuid(),
                    Username = "testuser",
                    Email = "test@example.com",
                    Role = UserRole.Author // Örnek olarak Author
                };

                var token = _jwtService.GenerateToken(user);
                return Ok(new { token });
            }

            return Unauthorized("Kullanıcı adı veya şifre hatalı.");
        }
    }
}
