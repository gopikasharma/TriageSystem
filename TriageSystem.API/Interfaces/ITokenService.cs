using TriageSystem.API.Entities;

namespace TriageSystem.API.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}