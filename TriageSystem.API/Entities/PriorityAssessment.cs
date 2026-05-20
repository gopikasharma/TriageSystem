using TriageSystem.API.Interfaces;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Entities;

public class PriorityAssessment : BaseEntity,IAuditable
{
    public Guid SymptomReportId { get; set; }
    public SymptomReport SymptomReport { get; set; } = null!;

    // AI Generation
    public int AiScore { get; set; } 
    public PriorityLevel SuggestedPriority { get; set; }
    public string? AiReasoning { get; set; }

    public bool IsNurseValidated { get; set; } = false;
    public Guid? ValidatedByNurseId { get; set; }
    public AppUser? ValidatedByNurse { get; set; }
    
    public PriorityLevel FinalPriority { get; set; } 
    public string? ValidationNotes { get; set; }
    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}