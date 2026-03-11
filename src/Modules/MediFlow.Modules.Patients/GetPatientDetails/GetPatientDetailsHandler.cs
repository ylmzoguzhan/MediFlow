namespace MediFlow.Modules.Patients.GetPatientDetails;

public class GetPatientDetailsValidator : AbstractValidator<GetPatientDetailsQuery>
{
    public GetPatientDetailsValidator()
    {
        RuleFor(op => op.PatientId).NotEmpty().WithMessage("Geçerli bir hasta ID'si gereklidir.");
    }
}

public record GetPatientDetailsQuery(Guid PatientId) : IRequest<Result<GetPatientDetailsResponse>>;

public record GetPatientDetailsResponse(
    Guid PatientId,
    string FirstName,
    string LastName,
    string Email,
    DateTime DateOfBirth);
public class GetPatientDetailsHandler(PatientsDbContext dbContext) : IRequestHandler<GetPatientDetailsQuery, Result<GetPatientDetailsResponse>>
{
    public async Task<Result<GetPatientDetailsResponse>> Handle(GetPatientDetailsQuery request, CancellationToken cancellationToken)
    {
        var patient = await dbContext.Patients.FindAsync(request.PatientId);
        if (patient == null)
            return Result<GetPatientDetailsResponse>.Failure(PatientErrors.PatientNotFound);
        var response = new GetPatientDetailsResponse(patient.Id, patient.FirstName, patient.LastName, patient.Email, patient.DateOfBirth);
        return Result<GetPatientDetailsResponse>.Success(response);
    }
}
