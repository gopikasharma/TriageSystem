using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Identity;
using TriageSystem.API.Interfaces;
using TriageSystem.API.Shared;

namespace TriageSystem.API.Entities
{
    public class AppUser : IdentityUser<Guid>,IAuditable
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        public UserRole Role { get; set; }
        public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
        public DateTimeOffset? UpdatedAt { get; set; }
        public bool IsDeleted { get; set; } = false;
        public DateTimeOffset? DeletedAt { get; set; }
    }
}