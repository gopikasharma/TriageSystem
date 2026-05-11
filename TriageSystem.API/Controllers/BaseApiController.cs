using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;

namespace TriageSystem.API.Controllers;
[ApiController]
[Route("api/[controller]")]
public class BaseApiController : ControllerBase
{
    protected Guid GetCurrentUserId()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        
        if (Guid.TryParse(userIdString, out Guid userId))
        {
            return userId;
        }
        throw new UnauthorizedAccessException("User is not authenticated or token is invalid.");
    }
}