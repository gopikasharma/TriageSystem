using Microsoft.EntityFrameworkCore;
using TriageSystem.API.Data;
using TriageSystem.API.DTOs;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Services;

public class TriageEngineService : ITriageEngineService
{
    private readonly AppDbContext _context;

        public TriageEngineService(AppDbContext context)
    {
        _context = context;
    }
    
    public async Task<AssessmentResultDto> EvaluateSymptomsAsync(Guid userId, SymptomSubmissionDto input)
    {
       var patientId = await _context.Patients
            .Where(p => p.UserId == userId)
            .Select(p => p.Id)
            .FirstOrDefaultAsync();

        if (patientId == Guid.Empty)
        {
            var newPatientProfile = new Patient
            {
                UserId = userId,
                DateOfBirth = DateTime.UtcNow.AddYears(-30), // System default
                Gender = "Not Specified",
                Phone = "",
                Address = ""
            };
            
            _context.Patients.Add(newPatientProfile);
            await _context.SaveChangesAsync(); // Commit to DB
            
            patientId = newPatientProfile.Id;  // Grab the newly generated ID
        }

        
        int score = CalculateMockAiScore(input);
        PriorityLevel priority = DeterminePriorityLevel(score);
        string reasoning = $"Based on a pain level of {input.PainLevel}/10 and the chief complaint '{input.ChiefComplaint}', the engine has calculated a risk score of {score}. Recommended priority queue: {priority}.";

        var report = new SymptomReport
        {
            PatientId = patientId, 
            ChiefComplaint = input.ChiefComplaint,
            DetailedSymptoms = input.DetailedSymptoms,
            PainLevel = input.PainLevel,
            DurationInDays = input.DurationInDays
        };

        var assessment = new PriorityAssessment
        {
            SymptomReport = report,
            AiScore = score,
            SuggestedPriority = priority,
            FinalPriority = priority, 
            AiReasoning = reasoning,
            IsNurseValidated = false
        };

        _context.SymptomReports.Add(report);
        _context.PriorityAssessments.Add(assessment);
        
        
        await _context.SaveChangesAsync(); 

        
        return new AssessmentResultDto
        {
            AiScore = score,
            SuggestedPriority = priority.ToString(),
            AiReasoning = reasoning
        };
    }

    private int CalculateMockAiScore(SymptomSubmissionDto input)
    {
        int baseScore = input.PainLevel * 5; 
        string complaint = input.ChiefComplaint.ToLower();
        
        if (complaint.Contains("chest") || complaint.Contains("heart") || complaint.Contains("breath"))
            baseScore += 50;
            
        if (complaint.Contains("bleed") || complaint.Contains("headache") || complaint.Contains("vision"))
            baseScore += 30;

        return Math.Clamp(baseScore, 1, 100); 
    }

    
    private PriorityLevel DeterminePriorityLevel(int score)
    {
        if (score >= 80) return PriorityLevel.Immediate; 
        if (score >= 60) return PriorityLevel.Emergent;    
        if (score >= 40) return PriorityLevel.LessUrgent;  
        return PriorityLevel.Routine;                 
    }
}