using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TriageSystem.API.DTOs;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenService _tokenService;
        public AccountController(UserManager<AppUser> userManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _tokenService = tokenService;
        }
        
        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] UserRegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return BadRequest(new { Message = "Email is already registered." });
            }

            var user = new AppUser
            {
                UserName = registerDto.Email, 
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Sex = registerDto.Sex
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

            await _userManager.AddToRoleAsync(user, "Patient");

            return Ok(new { Message = "Patient registered successfully." });
        }

        [HttpPost("register-staff")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterStaff([FromBody] StaffRegisterDto registerDto)
        {
            // Fixed: "Reception" changed to "Receptionist" to match DbInitializer
            var allowedRoles = new[] { "Doctor", "Nurse", "Receptionist", "Reviewer" };
            
            var targetRole = allowedRoles.FirstOrDefault(r => r.Equals(registerDto.Role, StringComparison.OrdinalIgnoreCase));
            if (targetRole == null)
            {
                return BadRequest(new { Message = $"Invalid role. Allowed roles are: {string.Join(", ", allowedRoles)}" });
            }

            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                return BadRequest(new { Message = "Email is already registered." });
            }

            var user = new AppUser
            {
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Sex = registerDto.Sex,
                Specialization = registerDto.Specialization 
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded) return BadRequest(new { Errors = result.Errors.Select(e => e.Description) });

            await _userManager.AddToRoleAsync(user, targetRole);

            return Ok(new { Message = $"{targetRole} registered successfully." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            if (user == null || !await _userManager.CheckPasswordAsync(user, loginDto.Password))
            {
                return Unauthorized(new { Message = "Invalid email or password." });
            }

            var token = await _tokenService.CreateToken(user);
            var roles = await _userManager.GetRolesAsync(user);
            
            return Ok(new AuthResponseDto
            {
                Email = user.Email,
                Token = token,
                Roles = roles
            });
        }
    }
}