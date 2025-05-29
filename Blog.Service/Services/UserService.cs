using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Enums;
using Blog.Core.Interfaces;
using Blog.Core.Repositories;
using Blog.Core.Services;
using Blog.Service.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Blog.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // Yeni kullanıcıyı veritabanına kaydeder
        public async Task RegisterAsync(RegisterUserDto dto)
        {
            var passwordHasher = new PasswordHasher<User>();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Username = dto.Username,
                Email = dto.Email,
                Role = dto.Role
            };

            user.PasswordHash = passwordHasher.HashPassword(user, dto.Password);

            await _userRepository.AddAsync(user);
        }

        // Kullanıcı adı veya e-posta zaten var mı kontrol eder
        public async Task<bool> UserExistsAsync(string username, string email)
        {
            return await _userRepository.AnyAsync(u => u.Username == username || u.Email == email);
        }

        // Kullanıcı adı ve şifre ile giriş kontrolü
        public async Task<UserDto?> AuthenticateAsync(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAndPasswordAsync(username, password);

            if (user == null) return null;

            return new UserDto
            {
                Username = user.Username,
                Email = user.Email,
                Role = user.Role
            };
        }
    }
}
