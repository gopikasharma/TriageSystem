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
        private readonly RoleManager<AppRole> _roleManager;
        private readonly ITokenService _tokenService;

        // FIX 1: ITokenService added to the constructor here
        public AccountController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, ITokenService tokenService)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
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
                UserName = registerDto.Email, 
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
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

        [HttpPost("register-doctor")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult>RegisterDoctor([FromBody]DoctorRegisterDto registerDto)
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
                UserName = registerDto.Email,
                Email = registerDto.Email,
                FirstName = registerDto.FirstName,
                LastName = registerDto.LastName,
                PhoneNumber = registerDto.PhoneNumber,
                Sex = registerDto.Sex,
                Specialization = registerDto.Specialization
            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description);
                return BadRequest(new { Errors = errors });
            }
            var roleExists = await _roleManager.RoleExistsAsync("Doctor");
            if (!roleExists)
            {
                await _roleManager.CreateAsync(new AppRole { Name = "Doctor" });
            }

            await _userManager.AddToRoleAsync(user, "Doctor");

            return Ok(new { Message = "Doctor registered successfully." });
            
        }

        [HttpPost("login")]
        public async Task <IActionResult>Login([FromBody] LoginDto loginDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var user = await _userManager.FindByEmailAsync(loginDto.Email);

            // FIX 2: Replaced _signInManager with _userManager.CheckPasswordAsync
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