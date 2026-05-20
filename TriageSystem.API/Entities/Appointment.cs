using TriageSystem.API.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;

namespace TriageSystem.API.Entities;

public class Appointment : BaseEntity, IAuditable
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public Guid? DoctorId { get; set; }
    public AppUser? Doctor { get; set; }

    public Guid? PriorityAssessmentId { get; set; }
    public PriorityAssessment? PriorityAssessment { get; set; }

    public DateTime RequestedDate { get; set; }
    public DateTime? ConfirmedDate { get; set; }
    
    public string Department { get; set; } = string.Empty;
    public string Status { get; set; } = "Pending Triage"; 

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}