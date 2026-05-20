using TriageSystem.API.DTOs;

namespace TriageSystem.API.Interfaces;

public interface IAppointmentService
{
    Task<IEnumerable<AppointmentSummaryDto>> GetPatientAppointmentsAsync(Guid userId);
}