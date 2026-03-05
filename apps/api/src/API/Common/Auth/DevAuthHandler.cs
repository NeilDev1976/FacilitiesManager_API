namespace FacilitiesCoordinator.API.Common.Auth;

using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;

//This is to fake the authentication in development without having to send JWT tokens.
//Once I have built the client I will then swap this out for real JWT Bearer authentication and remove the DevAuthHandler and related code.
public sealed class DevAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
{
    public const string SchemeName = "Dev";

    public DevAuthHandler(
        IOptionsMonitor<AuthenticationSchemeOptions> options,
        ILoggerFactory logger,
        UrlEncoder encoder)
        : base(options, logger, encoder) { }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        Logger.LogInformation("DevAuthHandler: Authenticating request for {Path}", Request.Path);

        // Provider-style identifiers (mimic Entra / OIDC)
        var issuer = Request.Headers["X-Dev-Issuer"].FirstOrDefault()
                     ?? "https://login.microsoftonline.com/00000000-0000-0000-0000-000000000000/v2.0";

        var tenantId = Request.Headers["X-Dev-TenantId"].FirstOrDefault()
                       ?? "00000000-0000-0000-0000-000000000000"; // tid

        var objectId = Request.Headers["X-Dev-ObjectId"].FirstOrDefault()
                       ?? "11111111-1111-1111-1111-111111111111"; // oid

        // OIDC subject (sub). If you don’t supply it, use oid - for dev
        var subject = Request.Headers["X-Dev-Subject"].FirstOrDefault()
                      ?? objectId;

        // Friendly/profile-ish bits
        var username = Request.Headers["X-Dev-Username"].FirstOrDefault() ?? "alice";
        var email = Request.Headers["X-Dev-Email"].FirstOrDefault() ?? "alice@example.com";
        var displayName = Request.Headers["X-Dev-Name"].FirstOrDefault() ?? "Alice Example";

        // Roles (comma-separated)
        var rolesHeader = Request.Headers["X-Dev-Roles"].FirstOrDefault() ?? "User";
        var roles = rolesHeader.Split(',', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, objectId), 
            new(ClaimTypes.Name, displayName),
            new(ClaimTypes.Email, email),

            new("idp", "entra"),
            new("iss", issuer),  
            new("tid", tenantId),            
            new("oid", objectId),                        
            new("sub", subject),    
            new("preferred_username", email),  
            new("name", displayName),
        };

        foreach (var role in roles)
            claims.Add(new Claim(ClaimTypes.Role, role));

        var identity = new ClaimsIdentity(claims, authenticationType: SchemeName);
        var principal = new ClaimsPrincipal(identity);
        var ticket = new AuthenticationTicket(principal, SchemeName);

        return Task.FromResult(AuthenticateResult.Success(ticket));
    }
}