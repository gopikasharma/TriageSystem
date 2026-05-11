using System;
using TriageSystem.API.Entities;

namespace TriageSystem.API.Interfaces;

public interface IPatientRepository : IGenericRepository<Patient>
{
    Task<Patient?> GetPatientByUserIdAsync(Guid userId);
}
