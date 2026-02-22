namespace FacilitiesCoordinator.API.Features.Facilities.Read;

using FacilitiesCoordinator.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public sealed class ReadFacilitiesHandler
{
    private readonly AppDbContext _db;

    public ReadFacilitiesHandler(AppDbContext db) => _db = db;

    public async Task<IReadOnlyList<ReadFacilitiesResponse>> HandleAsync(CancellationToken ct)
    {
       return await _db.Facilities
            .AsNoTracking()
            .OrderBy(f => f.Code)
            .Select(f => new ReadFacilitiesResponse(
                f.Code,
                f.Name,
                f.CurrentStatus
            ))
            .ToListAsync(ct);
    }
}