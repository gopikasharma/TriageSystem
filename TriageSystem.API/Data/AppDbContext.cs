using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TriageSystem.API.Entities;
using TriageSystem.API.Interfaces;

namespace TriageSystem.API.Data;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // 1. Clinical Domain DbSets (Initialized to satisfy C# nullable rules)
    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<SymptomReport> SymptomReports => Set<SymptomReport>();
    public DbSet<PriorityAssessment> PriorityAssessments => Set<PriorityAssessment>();
    public DbSet<DoctorSchedule> DoctorSchedules => Set<DoctorSchedule>();
    public DbSet<Appointment> Appointments => Set<Appointment>();
    
    public DbSet<ConsultationRecord> Consultations => Set<ConsultationRecord>(); 

    // 2. The Fluent API
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); 

        // RULE 1: Enforce the strict 1-to-1 relationship for the AI Engine
        builder.Entity<SymptomReport>()
            .HasOne(s => s.Assessment)
            .WithOne(a => a.SymptomReport)
            .HasForeignKey<PriorityAssessment>(a => a.SymptomReportId)
            .OnDelete(DeleteBehavior.Restrict); 

        // RULE 2: Prevent "Multiple Cascade Paths" errors
        builder.Entity<Appointment>()
            .HasOne(a => a.Patient)
            .WithMany(p => p.Appointments)
            .HasForeignKey(a => a.PatientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Appointment>()
            .HasOne(a => a.Doctor)
            .WithMany()
            .HasForeignKey(a => a.DoctorId)
            .OnDelete(DeleteBehavior.Restrict);
            
    }

    public override int SaveChanges()
    {
        ApplyAuditTimestamps();
        return base.SaveChanges();
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        ApplyAuditTimestamps();
        return base.SaveChangesAsync(cancellationToken);
    }
    private void ApplyAuditTimestamps()
    {
        foreach (var entry in ChangeTracker.Entries<IAuditable>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Entity.CreatedAt = DateTimeOffset.UtcNow;
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow; 
                    break;
                case EntityState.Modified:
                    entry.Entity.UpdatedAt = DateTimeOffset.UtcNow;
                    break;
            }
        }
    }
}