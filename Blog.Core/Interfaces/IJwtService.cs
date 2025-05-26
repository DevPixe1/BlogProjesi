using Blog.Core.Enums;

namespace Blog.Core.Interfaces
{
    public interface IJwtService
    {
        // Uusername + rol
        string GenerateToken(string username, UserRole role);
    }
}