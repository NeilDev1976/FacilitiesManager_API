namespace FacilitiesCoordinator.Domain.Entities;

public class ExternalLogin
{
    public int Id { get; set; }

    // FK to your existing User
    public int UserId { get; set; }
    public User User { get; set; } = null!;

    // e.g. "entra", "google", "github"
    public string Provider { get; set; } = null!;

    // OIDC issuer (iss claim) – distinguishes tenants
    public string Issuer { get; set; } = null!;

    // Stable subject identifier
    // For Entra: use oid if present, otherwise sub
    public string Subject { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime? LastLoginAt { get; set; }
}