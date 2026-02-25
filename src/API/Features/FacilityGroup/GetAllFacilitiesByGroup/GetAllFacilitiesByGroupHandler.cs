namespace FacilitiesCoordinator.API.Features.FacilityGroup.GetAllFacilitiesByGroup;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore; 

public sealed class GetAllFacilitiesByGroupHandler
{
    private readonly AppDbContext _db;

    public GetAllFacilitiesByGroupHandler(AppDbContext db) => _db = db;

    public async Task<GetAllFacilitiesByGroupResult> HandleAsync(int groupId, CancellationToken ct)
    {
        var groupExists = await _db.FacilityGroups
            .AsNoTracking()
            .AnyAsync(g => g.Id == groupId, ct);

        if (!groupExists)
            return new GetAllFacilitiesByGroupResult(GetAllFacilitiesByGroupStatus.NotFound);
        
        var facilities = await _db.Facilities
            .AsNoTracking()
            .Where(f => f.GroupId == groupId)
            .OrderBy(f => f.Code)
            .Select(f => new GetAllFacilitiesByGroupResponse(
                f.Code,
                f.Name,
                f.CurrentStatus
            ))
            .ToListAsync(ct);

        return new GetAllFacilitiesByGroupResult(GetAllFacilitiesByGroupStatus.Success, facilities);
    }
}