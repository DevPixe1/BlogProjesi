﻿using Blog.Core.DTOs;
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

        // Kullanıcı giriş işlemi
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userService.AuthenticateAsync(loginDto.Username, loginDto.Password);

            if (user == null)
                return Unauthorized("Kullanıcı adı veya şifre hatalı.");

            var token = _jwtService.GenerateToken(user);
            return Ok(new { token });
        }

        // Kullanıcı kayıt işlemi
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
        {
            // Kullanıcı adı veya e-posta zaten var mı kontrol edilir
            var exists = await _userService.UserExistsAsync(dto.Username, dto.Email);
            if (exists)
                return BadRequest("Bu kullanıcı adı veya e-posta zaten kayıtlı.");

            // Yeni kullanıcı kaydedilir
            await _userService.RegisterAsync(dto);
            return Ok("Kayıt başarılı.");
        }
    }
}
