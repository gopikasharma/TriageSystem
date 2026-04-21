using TriageSystem.API.Shared;

namespace TriageSystem.API.Entities;

public class Patient
{
    public int Id { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public BiologicalSex Sex { get; set; }
    public int Age { get; set; }
    public string? Symptoms { get; set; }
    public Priority Priority{ get; set; } = Priority.Routine;
    public DateTime ArrivalTime { get; set; } = DateTime.UtcNow;
    public bool IsCheckedIn { get; set; } = false;
}
