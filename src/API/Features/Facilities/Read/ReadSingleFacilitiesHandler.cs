namespace FacilitiesCoordinator.API.Features.Facilities.Read;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class ReadSingleFacilityHandler
{
    private readonly AppDbContext _db;

    public ReadSingleFacilityHandler(AppDbContext db) => _db = db;

    public async Task<ReadFacilitiesResponse?> HandleAsync(string code, CancellationToken ct)
    {
       return await _db.Facilities
            .AsNoTracking()
            .Where(f => f.Code == code)
            .Select(f => new ReadFacilitiesResponse(
                f.Code,
                f.Name,
                f.CurrentStatus
            ))
            .SingleOrDefaultAsync(ct);
    }
}