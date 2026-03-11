namespace MediFlow.Modules.Practitioners.Features.Practitioners.CreatePractitioner;

public class CreatePractitionerValidator : AbstractValidator<CreatePractitionerCommand>
{
    public CreatePractitionerValidator()
    {
        RuleFor(op => op.FirstName).NotEmpty();
        RuleFor(op => op.LastName).NotEmpty();
        RuleFor(op => op.Email).NotEmpty().EmailAddress();
        RuleFor(op => op.SpecialtyId).NotEmpty();
    }
}
public record CreatePractitionerCommand(string FirstName, string LastName, string Email, string PhoneNumber, Guid SpecialtyId) : IRequest<Result<CreatePractitionerResponse>>;
public record CreatePractitionerResponse(Guid Id);
public class CreatePractitionerHandler(PractitionersDbContext dbContext) : IRequestHandler<CreatePractitionerCommand, Result<CreatePractitionerResponse>>
{
    public async Task<Result<CreatePractitionerResponse>> Handle(CreatePractitionerCommand request, CancellationToken ct)
    {
        var practitionerResult = Practitioner.Create(request.FirstName, request.LastName, request.Email, request.PhoneNumber, request.SpecialtyId);
        if (!practitionerResult.IsSuccess)
            return Result<CreatePractitionerResponse>.Failure(practitionerResult.Error);
        await dbContext.AddAsync(practitionerResult.Value!, ct);
        await dbContext.SaveChangesAsync(ct);
        return Result<CreatePractitionerResponse>.Success(new(practitionerResult.Value.Id));
    }
}
