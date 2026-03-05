namespace FacilitiesCoordinator.API.Features.Users.GetById;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class GetUserByIdHandler
{
    private readonly AppDbContext _db;

    public GetUserByIdHandler(AppDbContext db) => _db = db;

    public async Task<GetUserByIdResponse?> HandleAsync(int id, CancellationToken ct)
    {
       return await _db.Users
            .AsNoTracking()
            .Where(u => u.Id == id)
            .Select(u => new GetUserByIdResponse(
                u.Id,
                u.Username,
                u.Email,
                u.FirstName,
                u.LastName,
                u.IsActive,
                u.UserRoles.Select(ur => ur.Role.Name).ToList()
            ))
            .SingleOrDefaultAsync(ct);
    }
}