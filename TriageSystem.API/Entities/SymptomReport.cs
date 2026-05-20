using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Entities;


public class SymptomReport : BaseEntity, IAuditable 
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public string ChiefComplaint { get; set; } = string.Empty;
    public string DetailedSymptoms { get; set; } = string.Empty;
    
    public int PainLevel { get; set; } // 0-10
    public int DurationInDays { get; set; }

    public PriorityAssessment? Assessment { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}