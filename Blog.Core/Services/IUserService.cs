using Blog.Core.DTOs;
using Blog.Core.Entities;
using System;
using System.Threading.Tasks;

namespace Blog.Core.Services
{
    public interface IUserService
    {
        Task RegisterAsync(RegisterUserDto dto); // Kullanıcı kaydı yapar
        Task<UserDto?> AuthenticateAsync(string username, string password); // Kullanıcı adı ve şifre ile doğrulama yapar
        Task<bool> UserExistsAsync(string username, string email); // Kullanıcının mevcut olup olmadığını kontrol eder
        Task<User> GetByIdAsync(Guid userId); // Kullanıcıyı ID ile getirir
        Task<User?> GetByUsernameAsync(string username); // Kullanıcıyı username ile getirir
    }
}
