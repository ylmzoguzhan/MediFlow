using MediFlow.Modules.Practitioners.Domain.Practitioner;
using MediFlow.Modules.Practitioners.Domain.Specialty;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Practitioners.Infrastructure.Persistence;

public class PractitionersDbContext : DbContext
{
    public PractitionersDbContext(DbContextOptions<PractitionersDbContext> options) : base(options) { }
    public DbSet<Specialty> Specialties { get; set; }
    public DbSet<Practitioner> Practitioners { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("practitioners");
        base.OnModelCreating(modelBuilder);
    }
}
