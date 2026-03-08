using FluentValidation;
using MediatR;
using MediFlow.Modules.Patients.Domain;
using MediFlow.Modules.Patients.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Patients.UpdatePatient;

public class UpdatePatientProfileValidator : AbstractValidator<UpdatePatientProfileCommand>
{
    public UpdatePatientProfileValidator()
    {
        RuleFor(op => op.FirstName).NotEmpty();
        RuleFor(op => op.LastName).NotEmpty();
        RuleFor(op => op.PhoneNumber).NotEmpty();
        RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Today);
    }
}

public record UpdatePatientProfileCommand(Guid Id, string FirstName, string LastName, string PhoneNumber, DateTime DateOfBirth) : IRequest<Result<UpdatePatientProfileResponse>>;
public record UpdatePatientProfileResponse(Guid Id);
public class UpdatePatientProfileHandler(PatientsDbContext dbContext) : IRequestHandler<UpdatePatientProfileCommand, Result<UpdatePatientProfileResponse>>
{
    public async Task<Result<UpdatePatientProfileResponse>> Handle(UpdatePatientProfileCommand request, CancellationToken cancellationToken)
    {
        var patient = await dbContext.Patients.FirstOrDefaultAsync(op => op.Id == request.Id, cancellationToken);
        if (patient == null)
            return Result<UpdatePatientProfileResponse>.Failure(PatientErrors.PatientNotFound);
        patient.UpdatePatient(request.FirstName, request.LastName, request.PhoneNumber, request.DateOfBirth);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result<UpdatePatientProfileResponse>.Success(new UpdatePatientProfileResponse(patient.Id));
    }
}
