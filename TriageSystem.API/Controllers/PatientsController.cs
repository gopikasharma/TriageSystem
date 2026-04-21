using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriageSystem.API.Data;
using TriageSystem.API.DTOs;
using TriageSystem.API.Entities;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Controllers;

[AllowAnonymous]
public class PatientsController :BaseApiController
{
    private readonly AppDbContext _context;
    public PatientsController(AppDbContext context) => _context = context;

[HttpPost("register")]
    public async Task<IActionResult> RegisterPatient([FromBody] PatientRegisterDto dto)
    {
        var patient = new Patient
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Email = dto.Email,
            Sex = dto.Sex,
            Age = dto.Age,
            Symptoms = dto.Symptoms,
            Priority = Priority.Routine
        };
        _context.Patients.Add(patient);
        await _context.SaveChangesAsync();
        return Ok(new { Message = "Registration successful", Id = patient.Id });
    }
}