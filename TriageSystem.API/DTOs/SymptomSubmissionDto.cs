using System;

namespace TriageSystem.API.DTOs;

public class SymptomSubmissionDto
{
    public string ChiefComplaint { get; set; } = string.Empty;
    public string DetailedSymptoms { get; set; } = string.Empty;
    public int PainLevel { get; set; } 
    public int DurationInDays { get; set; }
}
