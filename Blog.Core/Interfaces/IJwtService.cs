using Blog.Core.DTOs;

namespace Blog.Core.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(UserInfoDto user);
    }
}
