using TriageSystem.API.DTOs;

namespace TriageSystem.API.Interfaces;

public interface IAccountService
{
    Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
    Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
}