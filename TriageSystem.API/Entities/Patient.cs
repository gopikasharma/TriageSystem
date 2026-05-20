using System.ComponentModel.DataAnnotations.Schema;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Entities;

public class Patient : BaseEntity,IAuditable
{
    public Guid UserId { get; set; }
    [ForeignKey("UserId")]
    public AppUser User { get; set; } = null!;

    public DateTime DateOfBirth { get; set; }
    public string Gender { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    
    public string? Allergies { get; set; }
    public string? CurrentMedications { get; set; }
    public string? ChronicConditions { get; set; }
    public string? EmergencyContactInfo { get; set; }

    public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    public ICollection<SymptomReport> SymptomReports { get; set; } = new List<SymptomReport>();
}
