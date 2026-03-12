using BuildingBlocks.Domain;

namespace MediFlow.Modules.Practitioners.Contracts;

public record PractitionerCreatedDomainEvent(Guid PractitionerId, bool IsActive) : IDomainEvent;
