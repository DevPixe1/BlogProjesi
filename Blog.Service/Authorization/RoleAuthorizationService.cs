using Blog.Core.Enums;

namespace Blog.Service.Authorization
{
    public class RoleAuthorizationService
    {
        public static void EnsureUserHasPermission(UserRole userRole, UserRole requiredRole)
        {
            if (userRole < requiredRole)
            {
                throw new UnauthorizedAccessException("Rolün yetkisi yok.");
            }
        }
    }
}
