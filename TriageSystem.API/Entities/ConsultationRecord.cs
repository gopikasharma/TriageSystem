namespace TriageSystem.API.Entities;

  

public class ConsultationRecord : BaseEntity

{

    public Guid AppointmentId { get; set; }

    public Appointment Appointment { get; set; } = null!;

    

    public Guid DoctorId { get; set; }

    public AppUser Doctor { get; set; } = null!;

    

    public Guid PatientId { get; set; }

    public Patient Patient { get; set; } = null!;

    

    public string DoctorsFindings { get; set; } = string.Empty;

    public string TreatmentPlan { get; set; } = string.Empty;

    public bool NeedsFollowUp { get; set; } = false;

    public DateTimeOffset? SuggestedFollowUpDate { get; set; }

}