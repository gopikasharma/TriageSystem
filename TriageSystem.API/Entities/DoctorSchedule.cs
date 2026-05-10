namespace TriageSystem.API.Entities;

public class DoctorSchedule : BaseEntity
{
    public Guid DoctorId { get; set; }
    public AppUser Doctor { get; set; } = null!;

    public DayOfWeek DayOfWeek { get; set; } 
    public TimeSpan StartTime { get; set; } 
    public TimeSpan EndTime { get; set; } 
    
    public int SlotDurationMinutes { get; set; } = 20; 
}