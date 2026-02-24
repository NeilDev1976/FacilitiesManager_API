namespace FacilitiesCoordinator.API.Features.FacilityGroup.Create;

using FacilitiesCoordinator.Domain.Entities;
using FacilitiesCoordinator.Infrastructure.Data;

public sealed class CreateFacilityGroupHandler
{
    private readonly AppDbContext _db;
    private readonly ILogger<CreateFacilityGroupHandler> _logger;

    public CreateFacilityGroupHandler(AppDbContext db, ILogger<CreateFacilityGroupHandler> logger) => (_db, _logger) = (db, logger);

    public async Task<CreateFacilityGroupResponse> HandleAsync(CreateFacilityGroupRequest request, CancellationToken ct)
    {
        var group = new FacilityGroup
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
        );
    }
}