using TriageSystem.API.Shared;

namespace TriageSystem.API.Entities;

public class Appointment : BaseEntity
{
    public Guid PatientId { get; set; }
    public Patient Patient { get; set; } = null!;

    public Guid DoctorId { get; set; }
    public AppUser Doctor { get; set; } = null!;

    public Guid SymptomReportId { get; set; }
    public SymptomReport SymptomReport { get; set; } = null!;

    public DateTimeOffset ScheduledDateTime { get; set; }
    public AppointmentStatus Status { get; set; } = AppointmentStatus.PendingValidation;
}