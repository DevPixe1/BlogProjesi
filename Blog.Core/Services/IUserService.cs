using Blog.Core.DTOs;
using Blog.Core.Entities;

namespace Blog.Core.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto dto); // Yeni kullanıcı kaydı
        Task<UserDto?> AuthenticateAsync(string username, string password); // Login işlemi
    }
}
