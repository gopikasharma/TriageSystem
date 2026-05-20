using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TriageSystem.API.DTOs;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Controllers;

[Authorize] 
public class AppointmentController : BaseApiController
{
    private readonly IAppointmentService _appointmentService;

    public AppointmentController(IAppointmentService appointmentService)
    {
        _appointmentService = appointmentService;
    }

    [Authorize(Roles = "Patient")]
    [HttpGet("my-appointments")]
    public async Task<ActionResult<IEnumerable<AppointmentSummaryDto>>> GetMyAppointments()
    {
        var userId = GetCurrentUserId(); 
        var appointments = await _appointmentService.GetPatientAppointmentsAsync(userId);
        return Ok(appointments);
    }
}