using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TriageSystem.API.DTOs;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Controllers;
public class TriageController : BaseApiController
{
    private readonly ITriageEngineService _triageService;
    public TriageController(ITriageEngineService triageService)
    {
        _triageService = triageService;
    }
    [HttpPost("evaluate")]
    public async Task<ActionResult<AssessmentResultDto>> EvaluateSymptoms([FromBody] SymptomSubmissionDto request)
    {
        if (request == null)
            return BadRequest("Symptom data is required.");

        var assessmentResult = await _triageService.EvaluateSymptomsAsync(request);

        return Ok(assessmentResult);
    }
}
