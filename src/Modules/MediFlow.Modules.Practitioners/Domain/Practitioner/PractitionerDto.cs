namespace MediFlow.Modules.Practitioners.Domain.Practitioner;

public record PractitionerDto(Guid Id, string FirstName, string LastName, string Email, string PhoneNumber, string SpecialtyName, Guid SpecialtyId);
