namespace TriageSystem.API.DTOs;

public class AppointmentSummaryDto
{
    public Guid Id { get; set; }
    public string Department { get; set; } = string.Empty;
    public DateTime RequestedDate { get; set; }
    public DateTime? ConfirmedDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string DoctorName { get; set; } = "Pending Assignment";
}