namespace TriageSystem.API.DTOs;

public class AuthResponseDto
{
    public string Email { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string Token { get; set; } = string.Empty;
    public string[] Roles { get; set; } = Array.Empty<string>();
}