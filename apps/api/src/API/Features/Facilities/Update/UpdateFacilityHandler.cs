namespace FacilitiesCoordinator.API.Features.Facilities.Update;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class UpdateFacilityHandler
{
    private readonly AppDbContext _db;
    private readonly ILogger<UpdateFacilityHandler> _logger;

    public UpdateFacilityHandler(AppDbContext db, ILogger<UpdateFacilityHandler> logger) => (_db, _logger) = (db, logger);

    public async Task<UpdateFacilityResult> HandleAsync(int id, UpdateFacilityRequest request, CancellationToken ct)
    {
        var facility = await _db.Facilities.FirstOrDefaultAsync(x => x.Id == id, ct);
        if (facility is null) return new(UpdateFacilityStatus.NotFound);

        var changed = false;

        if (request.Name is not null)
        {
            var trimmed = request.Name.Trim();
            if (!string.Equals(facility.Name, trimmed, StringComparison.Ordinal))
            {
                facility.Name = trimmed;
                changed = true;
            }
        }

            if (request.GroupId.HasValue && request.GroupId.Value != facility.GroupId)
            {
                var groupExists = await _db.FacilityGroups.AnyAsync(g => g.Id == request.GroupId.Value, ct);
                if (!groupExists)
                {
                    _logger.LogWarning("Attempted to update facility {FacilityId} with non-existent group ID {GroupId}", id, request.GroupId.Value);
                    return new(UpdateFacilityStatus.NotFound);
                }
    
                facility.GroupId = request.GroupId.Value;
                changed = true;
            }

        if (!changed) return new(UpdateFacilityStatus.NoChanges);

        facility.UpdatedAt = DateTime.UtcNow;
        await _db.SaveChangesAsync(ct);

        return new(
            UpdateFacilityStatus.Updated,
            new UpdateFacilityResponse(facility.Id, 
                                    facility.Code, 
                                    facility.Name, 
                                    facility.CurrentStatus, 
                                    facility.UpdatedAt, 
                                    facility.GroupId.GetValueOrDefault())
        );
    }
}