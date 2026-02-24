namespace FacilitiesCoordinator.API.Features.FacilityGroup.Update;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class UpdateFacilityGroupHandler
{
    private readonly AppDbContext _db;
    private readonly ILogger<UpdateFacilityGroupHandler> _logger;

    public UpdateFacilityGroupHandler(AppDbContext db, ILogger<UpdateFacilityGroupHandler> logger) => (_db, _logger) = (db, logger);

    public async Task<UpdateFacilityGroupResult> HandleAsync(int id, UpdateFacilityGroupRequest request, CancellationToken ct)
    {
        var group = await _db.FacilityGroups.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (group is null) return new(UpdateFacilityGroupStatus.NotFound);

        var changed = false;

        if (request.Name is not null)
        {
            var trimmed = request.Name.Trim();
            if (!string.Equals(group.Name, trimmed, StringComparison.Ordinal))
            {
                group.Name = trimmed;
                changed = true;
            }
        }

        if (!changed) return new(UpdateFacilityGroupStatus.NoChanges);

        await _db.SaveChangesAsync(ct);

        return new(
            UpdateFacilityGroupStatus.Updated,
            new UpdateFacilityGroupResponse(group.Id, group.Name)
        );
        
        /*var group = new FacilityGroup
        {
            Name = request.Name.Trim()
            // ID + CreatedAt set by defaults on entity
        };

        _db.FacilityGroups.Add(group);
        await _db.SaveChangesAsync(ct);

        _logger.LogInformation("Created facility group with name {Name} and id: {Id}", group.Name, group.Id);

        return new CreateFacilityGroupResponse(
            group.Id,
            group.Name,
            group.CreatedAt
        );*/
    }
}