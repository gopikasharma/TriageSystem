using TriageSystem.API.Entities;

namespace TriageSystem.API.Interfaces;
public interface ITokenService
{
    Task <string> CreateToken(AppUser user);
}