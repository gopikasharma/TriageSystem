using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriageSystem.API.DTOs;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Controllers;

[Authorize(Roles = "Patient")]
public class TriageController : BaseApiController
{
    private readonly ITriageEngineService _triageService;

    public TriageController(ITriageEngineService triageService)
    {
        _triageService = triageService;
    }

    [HttpPost("submit-symptoms")]
    public async Task<ActionResult<AssessmentResultDto>> SubmitSymptoms(
        [FromBody] SymptomSubmissionDto submission)
    {
        try
        {
            var userId = GetCurrentUserId();
            var result = await _triageService.EvaluateSymptomsAsync(userId, submission);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message); 
        }
    }
}