namespace FacilitiesCoordinator.API.Features.FacilityGroup.Delete;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class DeleteFacilityGroupHandler
{
    private readonly AppDbContext _db;

    public DeleteFacilityGroupHandler(AppDbContext db) => _db = db;

    public async Task<bool> HandleAsync(int id, CancellationToken ct)
    {
       var affected = await _db.FacilityGroups
            .Where(g => g.Id == id)
            .ExecuteDeleteAsync(ct);
       return affected > 0;
    }
}