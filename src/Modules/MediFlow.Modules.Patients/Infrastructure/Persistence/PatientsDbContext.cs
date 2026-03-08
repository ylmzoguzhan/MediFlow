using MediFlow.Modules.Patients.Domain;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Patients.Infrastructure.Persistence;

public class PatientsDbContext : DbContext
{
    public PatientsDbContext(DbContextOptions<PatientsDbContext> options) : base(options) { }
    public DbSet<Patient> Patients { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("patients");
        modelBuilder.Entity<Patient>()
        .HasIndex(p => p.Email)
        .IsUnique();
        base.OnModelCreating(modelBuilder);
    }
}
