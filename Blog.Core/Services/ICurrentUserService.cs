using Blog.Core.Enums;

namespace Blog.Core.Services
{
    public interface ICurrentUserService
    {
        string Username { get; }
        UserRole Role { get; }
    }

}
