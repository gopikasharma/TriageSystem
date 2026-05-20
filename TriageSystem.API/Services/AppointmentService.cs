using Microsoft.EntityFrameworkCore;
using TriageSystem.API.Data;
using TriageSystem.API.DTOs;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Services;

public class AppointmentService : IAppointmentService
{
    private readonly AppDbContext _context;

    public AppointmentService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AppointmentSummaryDto>> GetPatientAppointmentsAsync(Guid userId)
    {
        return await _context.Appointments
            .AsNoTracking()
            .Where(a => a.Patient.UserId == userId)
            .OrderByDescending(a => a.CreatedAt)
            .Select(a => new AppointmentSummaryDto
            {
                Id = a.Id,
                Department = a.Department,
                RequestedDate = a.RequestedDate,
                ConfirmedDate = a.ConfirmedDate,
                Status = a.Status,
                DoctorName = a.Doctor != null ? $"Dr. {a.Doctor.LastName}" : "Pending Assignment"
            })
            .ToListAsync();
    }
}