using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TriageSystem.API.Data;
using TriageSystem.API.DTOs;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly ITokenService _tokenService;
    private readonly AppDbContext _context; 

    public AccountService(UserManager<AppUser> userManager, ITokenService tokenService, AppDbContext context)
    {
        _userManager = userManager;
        _tokenService = tokenService;
        _context = context;
    }

    public async Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto)
    {
        if (await _userManager.Users.AnyAsync(x => x.Email == registerDto.Email.ToLower()))
            throw new Exception("Email is already taken");

        var assignedRole = (UserRole)registerDto.Role; 

        var user = new AppUser
        {
            UserName = registerDto.Email.ToLower(),
            Email = registerDto.Email.ToLower(),
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            Role = assignedRole 
        };

        var result = await _userManager.CreateAsync(user, registerDto.Password);
        if (!result.Succeeded) throw new Exception("Failed to create user");

        if (assignedRole == UserRole.Patient)
        {
            var patientProfile = new Patient
            {
                UserId = user.Id,
                DateOfBirth = DateTime.UtcNow.AddYears(-30),
                Gender = "Not Specified",
                Phone = "",
                Address = ""
            };

            _context.Patients.Add(patientProfile);
            await _context.SaveChangesAsync();
        }

        return new AuthResponseDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            Token = _tokenService.CreateToken(user),
            Roles = new[] { assignedRole.ToString() }
        };
    }

    public async Task<AuthResponseDto> LoginAsync(LoginDto loginDto)
    {
        var user = await _userManager.Users.SingleOrDefaultAsync(x => x.Email == loginDto.Email.ToLower());

        if (user == null || user.IsDeleted) 
            throw new UnauthorizedAccessException("Invalid Email");

        var result = await _userManager.CheckPasswordAsync(user, loginDto.Password);

        if (!result) 
            throw new UnauthorizedAccessException("Invalid Password");

        return new AuthResponseDto
        {
            Email = user.Email!,
            FirstName = user.FirstName,
            Token = _tokenService.CreateToken(user),
            Roles = new[] { user.Role.ToString() }
        };
    }
}