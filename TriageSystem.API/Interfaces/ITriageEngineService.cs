using System;
using TriageSystem.API.DTOs;

namespace TriageSystem.API.Interfaces;

public interface ITriageEngineService
{
    Task<AssessmentResultDto> EvaluateSymptomsAsync(SymptomSubmissionDto input);
}

