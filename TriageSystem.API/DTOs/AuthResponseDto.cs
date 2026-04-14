
namespace TriageSystem.API.DTOs;

public class AuthResponseDto
{
    public required string Email { get; set; }
    public required string Token { get; set; }
    public IList<string> Roles { get; set; } = new List<string>();
}
