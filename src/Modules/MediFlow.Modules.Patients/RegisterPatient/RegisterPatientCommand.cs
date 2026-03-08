using MediatR;

namespace MediFlow.Modules.Patients.RegisterPatient;

public record RegisterPatientCommand(
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth
) : IRequest<Result<RegisterPatientResponse>>;
