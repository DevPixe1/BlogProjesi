using Blog.Core.DTOs;
using Blog.Core.Enums;

namespace Blog.Core.Interfaces
{
    public interface IJwtService
    {
        // Uusername ve rol
        string GenerateToken(UserDto user);
    }
}