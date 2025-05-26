using Microsoft.AspNetCore.Mvc;
using Blog.Core.Interfaces;
using Blog.Core.DTOs.Authorization;
using Blog.Core.Enums;

namespace Blog.API.Controllers.Authorization;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IJwtService _jwtService;

    // JwtService DI (dependency injection) ile alınır
    public AuthController(IJwtService jwtService)
    {
        _jwtService = jwtService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest loginRequest)
    {
        // Basit demo amaçlı kullanıcı kontrolü ve rol ataması
        UserRole assignedRole;

        // Kullanıcı adı/parola kontrolü ve role belirleme
        if (loginRequest.Username == "admin" && loginRequest.Password == "123")
        {
            assignedRole = UserRole.Author; // Tüm işlemlere yetkili
        }
        else if (loginRequest.Username == "user" && loginRequest.Password == "123")
        {
            assignedRole = UserRole.User; // Yorum ve Get işlemleri
        }
        else if (loginRequest.Username == "outsider" && loginRequest.Password == "123")
        {
            assignedRole = UserRole.Outsider; // Sadece Get işlemleri
        }
        else
        {
            return Unauthorized("Geçersiz kullanıcı adı veya şifre.");
        }

        // Token üretilirken kullanıcı adı ve rol bilgisi verilir
        var token = _jwtService.GenerateToken(loginRequest.Username, assignedRole);
        return Ok(new { token });
    }
}
