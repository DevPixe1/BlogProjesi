using Blog.Core.Enums;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;

namespace Blog.Core.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CurrentUserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        // GUID mantığı tamamen kaldırıldı; artık UserId dönmüyor.
        // Eğer interface’de UserId zorunluysa, bu kısmı ICurrentUserService’ten de silmeniz gerekir.

        public string Username =>
            _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Name)?.Value
            ?? string.Empty;

        public UserRole Role
        {
            get
            {
                var roleClaim = _httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.Role)?.Value;
                return Enum.TryParse<UserRole>(roleClaim, out var role) ? role : UserRole.Outsider;
            }
        }
    }
}
