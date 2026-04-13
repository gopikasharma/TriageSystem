using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Entities
{
    public class AppUser :IdentityUser
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public DateOnly DateOfBirth { get; set; }
        public BiologicalSex Sex { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [MaxLength(100)]
        public string? Specialization { get; set; }
    }
}