namespace FacilitiesCoordinator.Domain.Entities;

public class FacilityStatusNote
{
    public int Id { get; set; }

    public int StatusHistoryId { get; set; }

    public string NoteText { get; set; } = null!;

    public int CreatedByUserId { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Navigation
    public FacilityStatusHistory StatusHistory { get; set; } = null!;
    public User CreatedByUser { get; set; } = null!;
}
