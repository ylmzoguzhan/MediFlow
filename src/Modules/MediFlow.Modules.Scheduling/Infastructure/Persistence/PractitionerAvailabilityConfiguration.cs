using MediFlow.Modules.Scheduling.Domain.PractitionerAvailability;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediFlow.Modules.Scheduling.Infastructure.Persistence;

public class PractitionerAvailabilityConfiguration : IEntityTypeConfiguration<PractitionerAvailability>
{
    public void Configure(EntityTypeBuilder<PractitionerAvailability> builder)
    {
        builder.ToTable("PractitionerAvailabilities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.PractitionerId)
            .IsRequired();

        builder.OwnsMany(x => x.Rules, rules =>
        {
            rules.ToTable("WeeklyAvailabilityRules");

            rules.WithOwner().HasForeignKey("PractitionerAvailabilityId");

            rules.Property<Guid>("Id");
            rules.HasKey("Id");

            rules.Property(x => x.DayOfWeek)
                .IsRequired();

            rules.Property(x => x.StartTime)
                .IsRequired();

            rules.Property(x => x.EndTime)
                .IsRequired();
        });
    }
}