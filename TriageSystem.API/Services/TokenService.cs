using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace TriageSystem.API.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _userManager = userManager;

            var key = _config["Jwt:Key"];
            if (string.IsNullOrEmpty(key))
                throw new Exception("JWT Key is missing");

            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
        }

        public async Task<string> CreateToken(AppUser user)
        
        {
            if (string.IsNullOrEmpty(user.Email))
                throw new Exception("User email is required");
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),

                new Claim(JwtRegisteredClaimNames.Email, user.Email ?? ""),

                new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName ?? ""),

                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
            var roles = await _userManager.GetRolesAsync(user);
            claims.AddRange(
                roles.Select(role => new Claim(ClaimTypes.Role, role))
            );
            var creds = new SigningCredentials(
                _key,
                SecurityAlgorithms.HmacSha512Signature
            );
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = creds,

                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}