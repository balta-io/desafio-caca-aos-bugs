using System.Security.Claims;
using Dima.Api.Common.Api;
using Dima.Core.Models.Account;
using Microsoft.AspNetCore.Identity;

namespace Dima.Api.Endpoints.Identity;

public class GetRolesEndpoint : IEndpoint
{
    public static void Map(IEndpointRouteBuilder app)
        => app
            .MapGet("/roles", Handle)
            .RequireAuthorization();

    private static Task<IResult> Handle(
        ClaimsPrincipal user,
        UserManager<Dima.Api.Models.User> userManager)
    {
        if (user.Identity is null || !user.Identity.IsAuthenticated)
            return Task.FromResult(Results.Unauthorized());

        var identity = (ClaimsIdentity)user.Identity;
        var roles = identity
            .FindAll(identity.RoleClaimType)
            .Select(c => new RoleClaim
            {
                Issuer = c.Issuer,
                OriginalIssuer = c.OriginalIssuer,
                Type = c.Type,
                Value = c.Value,
                ValueType = c.ValueType
            });

        var userId = userManager.GetUserId(user);

        long.TryParse(userId, out var userIdLong);

        return Task.FromResult<IResult>(
            TypedResults.Json(
                new UserInfo
                {
                    Roles = roles.ToArray(),
                    UserId = userIdLong
                }
            ));
    }
}