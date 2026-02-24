namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetAll;
using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class GetAllFacilityGroupsHandler
{
    private readonly AppDbContext _db;

    public GetAllFacilityGroupsHandler(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<GetAllFacilityGroupResponse>> HandleAsync(CancellationToken ct)
    {
       return await _db.FacilityGroups
            .AsNoTracking()
            .OrderBy(g => g.Name)
            .Select(g => new GetAllFacilityGroupResponse(
                g.Id,
                g.Name,
                g.CreatedAt
            ))
            .ToListAsync(ct);
    }
}