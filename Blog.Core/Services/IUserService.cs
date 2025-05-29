using Blog.Core.DTOs;
using Blog.Core.Entities;
using Blog.Core.Repositories;

namespace Blog.Core.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto dto); // Yeni kullanıcı kaydı
        Task<UserDto?> AuthenticateAsync(string username, string password); // Giriş işlemi
        Task<bool> UserExistsAsync(string username, string email); // Aynı kullanıcı adı veya e-posta var mı kontrolü
    }
}
