namespace FacilitiesCoordinator.Domain.Entities;

public class Staff
{
    public int StaffId { get; set; }

    public int FacilityId { get; set; }
    public Facility Facility { get; set; } = null!;

    public string Name { get; set; } = null!;
    public string NameNormalized { get; set; } = null!;

    public int MinimumStaffRequired { get; set; }

    public int CurrentStaffCount { get; set; }

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}