using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace RuoYi.Common.Security
{
    public class PermissionHandler
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PermissionHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public bool HasPermission(string permission)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity?.IsAuthenticated == true)
                return false;

            var permissionsClaim = user.FindFirst("permissions")?.Value;
            if (string.IsNullOrEmpty(permissionsClaim))
                return false;

            var permissions = permissionsClaim.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return permissions.Contains(permission);
        }

        public bool HasRole(string role)
        {
            var user = _httpContextAccessor.HttpContext?.User;
            if (user == null || !user.Identity?.IsAuthenticated == true)
                return false;

            var rolesClaim = user.FindFirst(ClaimTypes.Role)?.Value;
            if (string.IsNullOrEmpty(rolesClaim))
                return false;

            var roles = rolesClaim.Split(',', StringSplitOptions.RemoveEmptyEntries);
            return roles.Contains(role);
        }

        public bool IsAdmin()
        {
            return HasRole("admin");
        }
    }
}