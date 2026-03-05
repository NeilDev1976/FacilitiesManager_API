namespace FacilitiesCoordinator.Domain.Entities;
public class FacilityGroup
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public ICollection<Facility> OwnedFacilityList { get; set; } = new List<Facility>();
}