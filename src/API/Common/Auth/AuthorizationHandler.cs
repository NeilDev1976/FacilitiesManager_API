namespace FacilitiesCoordinator.API.Common.Auth;

using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using FacilitiesCoordinator.Infrastructure.Data;

public sealed class DatabaseRoleRequirement : IAuthorizationRequirement
{
    public string RequiredRole { get; }
    public DatabaseRoleRequirement(string requiredRole) => RequiredRole = requiredRole;
}

public sealed class DatabaseRoleHandler : AuthorizationHandler<DatabaseRoleRequirement>
{
    private readonly AppDbContext _dbContext;

    public DatabaseRoleHandler(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        DatabaseRoleRequirement requirement)
    {
        // 1) Pull external identity from claims
        // Provider: for now you can hardcode "entra" in dev.
        // Later, if you add multiple schemes/providers, you can infer it from auth scheme or add a claim.
        var provider = context.User.FindFirst("idp")?.Value ?? "entra";

        var issuer = context.User.FindFirst("iss")?.Value;
        var subject = context.User.FindFirst("oid")?.Value
                   ?? context.User.FindFirst("sub")?.Value;

        if (string.IsNullOrWhiteSpace(issuer) || string.IsNullOrWhiteSpace(subject))
        {
            context.Fail();
            return;
        }

        // 2) Resolve to local user id via ExternalLogins
        var userId = await _dbContext.ExternalLogins
            .Where(x => x.Provider == provider && x.Issuer == issuer && x.Subject == subject)
            .Select(x => (int?)x.UserId)
            .FirstOrDefaultAsync();

        if (userId is null)
        {
            context.Fail();
            return;
        }

        // 3) Check roles for that user
        var hasRole = await _dbContext.UserRoles
            .Include(ur => ur.Role)
            .AnyAsync(ur => ur.UserId == userId.Value && ur.Role.Name == requirement.RequiredRole);

        if (hasRole) context.Succeed(requirement);
        else context.Fail();
    }
}