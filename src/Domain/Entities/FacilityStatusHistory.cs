namespace FacilitiesCoordinator.Domain.Entities;

public class FacilityStatusHistory
{
    public int Id { get; set; }

    public int FacilityId { get; set; }

    public string PreviousStatus { get; set; } = null!;

    public string NewStatus { get; set; } = null!;

    public int ChangedByUserId { get; set; }

    public DateTime ChangedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public Facility Facility { get; set; } = null!;
    public User ChangedByUser { get; set; } = null!;
    public ICollection<FacilityStatusNote> Notes { get; set; } = new List<FacilityStatusNote>();
}
