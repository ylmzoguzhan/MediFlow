using MediatR;
using MediFlow.Modules.Patients.Domain;
using MediFlow.Modules.Patients.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Patients.RegisterPatient;

public class RegisterPatientHandler(PatientsDbContext dbContext) : IRequestHandler<RegisterPatientCommand, Result<RegisterPatientResponse>>
{
    public async Task<Result<RegisterPatientResponse>> Handle(RegisterPatientCommand command, CancellationToken cancellationToken)
    {
        var existing = await dbContext.Patients.AnyAsync(op => op.Email == command.Email);
        if (existing)
            return Result.Failure<RegisterPatientResponse>(PatientErrors.EmailAlreadyExists);
        var patient = new Patient(command.FirstName, command.LastName, command.Email, command.PhoneNumber, command.DateOfBirth);
        await dbContext.Patients.AddAsync(patient);
        await dbContext.SaveChangesAsync();
        return Result.Success<RegisterPatientResponse>(new RegisterPatientResponse(patient.Id));
    }
}
