
namespace TriageSystem.API.DTOs;

public class AuthResponseDto
{
    public string Email { get; set; }
    public string Token { get; set; }
    public IList<string> Roles { get; set; }
}
