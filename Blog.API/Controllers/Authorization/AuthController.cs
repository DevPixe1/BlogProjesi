using Microsoft.AspNetCore.Mvc;
using Blog.Core.Interfaces;
using Blog.Core.DTOs.Authorization;


namespace Blog.API.Controllers.Authorization;

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
    public IActionResult Login([FromBody] Core.DTOs.Authorization.LoginRequest loginRequest)
    {
        // Basit demo amaçlı kontrol, kullanıcı yok
        if (loginRequest.Username == "admin" && loginRequest.Password == "123")
        {
            var token = _jwtService.GenerateToken(loginRequest.Username);
            return Ok(new { token });
        }

        return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
    }
}
