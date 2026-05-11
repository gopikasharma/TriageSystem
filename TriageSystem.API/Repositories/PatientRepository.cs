using System;
using Microsoft.EntityFrameworkCore;
using TriageSystem.API.Data;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Repositories;

public class PatientRepository : GenericRepository<Patient>, IPatientRepository
{
    public PatientRepository(AppDbContext context) : base(context)
    {
    }
    public async Task<Patient?> GetPatientByUserIdAsync(Guid userId)
    {
        return await _context.Patients
            .Include(p => p.User) 
            .FirstOrDefaultAsync(p => p.UserId == userId && !p.IsDeleted);
    }
}
