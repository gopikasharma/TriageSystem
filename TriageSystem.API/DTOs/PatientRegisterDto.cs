using TriageSystem.API.Shared;

namespace TriageSystem.API.DTOs;

public class PatientRegisterDto
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public BiologicalSex Sex{ get; set; }
    public int Age { get; set; }
    public string? Symptoms { get; set; }
}
