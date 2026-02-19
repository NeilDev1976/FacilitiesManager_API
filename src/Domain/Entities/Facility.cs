namespace FacilitiesCoordinator.Domain.Entities;
public class Facility
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string CurrentStatus { get; set; } = "Open";

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<FacilityStatusHistory> StatusHistory { get; set; } = new List<FacilityStatusHistory>();
}