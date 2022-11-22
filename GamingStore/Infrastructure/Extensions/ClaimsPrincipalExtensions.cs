using System.Security.Claims;

namespace GamingStore.Infrastructure.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static string Id(this ClaimsPrincipal principal)
            => principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        public static bool IsAdmin(this ClaimsPrincipal principal)
            => principal.IsInRole("Administrator");
    }
}