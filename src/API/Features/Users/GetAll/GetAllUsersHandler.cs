namespace FacilitiesCoordinator.API.Features.Users.GetAll;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class GetAllUsersHandler
{
    private readonly AppDbContext _db;

    public GetAllUsersHandler(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<GetAllUsersResponse>> HandleAsync(CancellationToken ct)
    {
       return await _db.Users
            .AsNoTracking()
            .OrderBy(u => u.Username)
            .Select(u => new GetAllUsersResponse(
                u.Id,
                u.Username,
                u.Email,
                u.FirstName,
                u.LastName,
                u.IsActive,
                u.UserRoles.Select(ur => ur.Role.Name).ToList()
            ))
            .ToListAsync(ct);
    }
}