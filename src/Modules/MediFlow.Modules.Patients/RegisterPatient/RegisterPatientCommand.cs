namespace MediFlow.Modules.Patients.RegisterPatient;

public record RegisterPatientCommand(
    string FirstName,
    string LastName,
    string Email,
    string PhoneNumber,
    DateTime DateOfBirth
) : IRequest<Result<RegisterPatientResponse>>;
