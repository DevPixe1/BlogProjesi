using Blog.Core.Enums;
using System;
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

        public Guid UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User.FindFirst("nameid")
                    ?? _httpContextAccessor.HttpContext?.User.FindFirst("sub"); // Alternatif kontrol

                return (userIdClaim != null && Guid.TryParse(userIdClaim.Value, out var id)) ? id : Guid.Empty;
            }
        }

        public string Username =>
            _httpContextAccessor.HttpContext?.User.FindFirst("unique_name")?.Value // Token'daki uygun claim
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
