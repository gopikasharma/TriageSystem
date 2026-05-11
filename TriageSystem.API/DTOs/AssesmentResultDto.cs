using System;

namespace TriageSystem.API.DTOs;

public class AssessmentResultDto
{
    public int AiScore { get; set; }
    public string SuggestedPriority { get; set; } = string.Empty;
    public string AiReasoning { get; set; } = string.Empty;
}
