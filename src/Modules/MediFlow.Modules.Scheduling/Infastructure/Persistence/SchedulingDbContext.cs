using MediFlow.Modules.Scheduling.Domain;
using MediFlow.Modules.Scheduling.Domain.PractitionerAvailability;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Scheduling.Infastructure.Persistence;

public class SchedulingDbContext : DbContext
{
    public SchedulingDbContext(DbContextOptions<SchedulingDbContext> options) : base(options) { }
    public DbSet<PractitionerAvailability> PractitionerAvailabilities { get; set; }
    public DbSet<SchedulingPractitionerReadModel> SchedulingPractitionerReadModels { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("scheduling");
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(SchedulingModule).Assembly);
    }

}
