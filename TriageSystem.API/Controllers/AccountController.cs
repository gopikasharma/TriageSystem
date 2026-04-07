using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TriageSystem.API.Controllers;
using TriageSystem.API.DTOs;
using TriageSystem.API.Entities;

namespace TriageSystem.API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpPost("register-patient")]
        public async Task<IActionResult> RegisterPatient([FromBody] UserRegisterDto registerDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userExists = await _userManager.FindByEmailAsync(registerDto.Email);
            if (userExists != null)
            {
                return BadRequest(new { Message = "Email is already registered." });
            }

            var user = new AppUser
            {
                UserName = registerDto.Email, // Email as username is standard
                Email = registerDto.Email,
                Fname = registerDto.Fname,
                Lname = registerDto.Lname,
                PhoneNumber = registerDto.PhoneNumber,
                Sex = registerDto.Sex
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            var roleExists = await _roleManager.RoleExistsAsync("Patient");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new AppRole { Name = "Patient" });
            }

            await _userManager.AddToRoleAsync(user, "Patient");

            return Ok(new { Message = "Patient registered successfully." });
        }
    }
}