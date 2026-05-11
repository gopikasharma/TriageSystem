using System;
using TriageSystem.API.DTOs;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Services;

public class TriageEngineService : ITriageEngineService
{
    public async Task<AssessmentResultDto> EvaluateSymptomsAsync(SymptomSubmissionDto input)
    {
        await Task.Delay(1000);
        int score = CalculateMockAiScore(input);
        string priority = DeterminePriorityLevel(score);
        string reasoning = $"Based on a pain level of {input.PainLevel}/10 and the chief complaint '{input.ChiefComplaint}', the engine has calculated a risk score of {score}. Recommended priority queue: {priority}.";
        return new AssessmentResultDto
        {
            AiScore = score,
            SuggestedPriority = priority,
            AiReasoning = reasoning
        };
        
    }
    private int CalculateMockAiScore(SymptomSubmissionDto input)
    {
        // Base score driven by pain
        int baseScore = input.PainLevel * 5; 

        string complaint = input.ChiefComplaint.ToLower();
        
        if (complaint.Contains("chest") || complaint.Contains("heart") || complaint.Contains("breath"))
            baseScore += 50;
            
        if (complaint.Contains("bleed") || complaint.Contains("headache") || complaint.Contains("vision"))
            baseScore += 30;

        // Ensure score stays between 1 and 100
        return Math.Clamp(baseScore, 1, 100); 
    }
    private string DeterminePriorityLevel(int score)
    {
        if (score >= 80) return "Emergency"; 
        if (score >= 60) return "Urgent";    
        if (score >= 40) return "Standard";  
        return "Non-Urgent";                 
    }
}
