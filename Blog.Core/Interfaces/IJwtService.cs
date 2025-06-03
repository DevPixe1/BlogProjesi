using Blog.Core.DTOs;
using Blog.Core.Enums;

namespace Blog.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(Guid userId, string username, UserRole role);
    }
}
