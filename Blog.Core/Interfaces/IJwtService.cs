using Blog.Core.DTOs;
using Blog.Core.Enums;

namespace Blog.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(string username, UserRole role);
    }
}
