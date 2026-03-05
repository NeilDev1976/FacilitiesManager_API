namespace FacilitiesCoordinator.API.Features.Facilities.Create;

using FacilitiesCoordinator.Domain.Entities;
using FacilitiesCoordinator.Infrastructure.Data;

public sealed class CreateFacilityHandler
{
    private readonly AppDbContext _db;

    public CreateFacilityHandler(AppDbContext db) => _db = db;

    public async Task<CreateFacilityResponse> HandleAsync(CreateFacilityRequest request, CancellationToken ct)
    {
        var facility = new Facility
        {
            Code = request.Code.Trim(),
            Name = request.Name.Trim(),
            GroupId = request.GroupId
            // CurrentStatus + CreatedAt set by defaults on entity
        };

        _db.Facilities.Add(facility);
        await _db.SaveChangesAsync(ct);

        return new CreateFacilityResponse(
            facility.Id,
            facility.Code,
            facility.Name,
            facility.CurrentStatus,
            facility.CreatedAt,
            request.GroupId
        );
    }
}