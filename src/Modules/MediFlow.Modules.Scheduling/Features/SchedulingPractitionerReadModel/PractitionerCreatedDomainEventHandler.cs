using MediatR;
using MediFlow.Modules.Practitioners.Contracts;
using MediFlow.Modules.Scheduling.Infastructure.Persistence;

namespace MediFlow.Modules.Scheduling.Features.SchedulingPractitionerReadModel;

public class PractitionerCreatedDomainEventHandler(SchedulingDbContext dbContext) : INotificationHandler<PractitionerCreatedDomainEvent>
{
    public async Task Handle(PractitionerCreatedDomainEvent notification, CancellationToken cancellationToken)
    {
        var readModel = new MediFlow.Modules.Scheduling.Domain.SchedulingPractitionerReadModel(notification.PractitionerId, notification.IsActive);
        await dbContext.AddAsync(readModel);
        await dbContext.SaveChangesAsync();
    }
}
