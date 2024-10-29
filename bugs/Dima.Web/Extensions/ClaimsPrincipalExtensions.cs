using System.Security.Claims;

namespace Dima.Web.Extensions;

internal static class ClaimsPrincipalExtensions
{
    public static long? GetUserId(this ClaimsPrincipal user)
    {
        var claim = user.Claims.FirstOrDefault(e => e.Type == ClaimTypes.NameIdentifier);

        if (claim is not null &&
            long.TryParse(claim.Value, out var userId))
        {
            return userId;
        }

        return null;
    }
}
