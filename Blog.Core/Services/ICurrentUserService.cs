using Blog.Core.Enums;

namespace Blog.Core.Services
{
    public interface ICurrentUserService
    {
        Guid UserId { get; }
        string Username { get; }
        UserRole Role { get; }

    }

}
