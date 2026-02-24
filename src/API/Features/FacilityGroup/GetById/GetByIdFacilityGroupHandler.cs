namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetById;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class GetByIdFacilityGroupHandler
{
    private readonly AppDbContext _db;

    public GetByIdFacilityGroupHandler(AppDbContext db) => _db = db;

    public async Task<GetByIdFacilityGroupResponse?> HandleAsync(int id, CancellationToken ct)
    {
       return await _db.FacilityGroups
            .AsNoTracking()
            .Where(g => g.Id == id)
            .Select(g => new GetByIdFacilityGroupResponse(
                g.Id,
                g.Name,
                g.CreatedAt
            ))
            .SingleOrDefaultAsync(ct);
    }
}