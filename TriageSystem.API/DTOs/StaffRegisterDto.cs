

using System.ComponentModel.DataAnnotations;
using TriageSystem.API.Shared;

namespace TriageSystem.API.DTOs;

public class StaffRegisterDto
{
    [Required]
    [EmailAddress]
    public required string Email { get; set; }
    [Required]
    [StringLength(100,MinimumLength =8)]
    public required string Password { get; set; }
    [Required]
    [MaxLength(50)]
    public required string FirstName { get; set; }
    [Required]
    [MaxLength(50)]
    public required string LastName { get; set; }
    [Required]
    [Phone]
    public required string PhoneNumber { get; set; }
    public BiologicalSex Sex {get; set;}
    [Required]
    [MaxLength(100)]
    public string? Specialization { get; set; }
    [Required] 
    public required string Role { get; set; }
}
