using System.ComponentModel.DataAnnotations;
using TriageSystem.API.Shared;

namespace TriageSystem.API.DTOs
{
    public class UserRegisterDto
    {
        [EmailAddress]
        public required string Email { get; set; }
        [StringLength(100, MinimumLength =8)]
        public required string Password { get; set; }
        public required string Fname { get; set; }
        public required string Lname { get; set; }
        public required string PhoneNumber { get; set;}
        public DateOnly DateOfBirth { get; set; }
        public BiologicalSex Sex { get; set; }



    }
}