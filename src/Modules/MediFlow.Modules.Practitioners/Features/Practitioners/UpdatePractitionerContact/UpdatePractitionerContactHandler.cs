
namespace MediFlow.Modules.Practitioners.Features.Practitioners.UpdatePractitionerContact;

public class UpdatePractitionerContactValidator : AbstractValidator<UpdatePractitionerContactCommand>
{
    public UpdatePractitionerContactValidator()
    {
        RuleFor(op => op.Id).NotEmpty();
        RuleFor(op => op.PhoneNumber).NotEmpty();
        RuleFor(op => op.Email).NotEmpty().EmailAddress();
    }
}
public record UpdatePractitionerContactCommand(Guid Id, string PhoneNumber, string Email) : IRequest<Result<UpdatePractitionerContactResult>>;
public record UpdatePractitionerContactResult(Guid Id);
public class UpdatePractitionerContactHandler(PractitionersDbContext dbContext) : IRequestHandler<UpdatePractitionerContactCommand, Result<UpdatePractitionerContactResult>>
{
    public async Task<Result<UpdatePractitionerContactResult>> Handle(UpdatePractitionerContactCommand request, CancellationToken cancellationToken)
    {
        var practitioner = await dbContext.Practitioners.FindAsync(request.Id);
        if (practitioner == null)
            return Result<UpdatePractitionerContactResult>.Failure(PractitionerError.NotFound);
        practitioner!.UpdateContact(request.Email, request.PhoneNumber);
        await dbContext.SaveChangesAsync(cancellationToken);
        return Result<UpdatePractitionerContactResult>.Success(new(practitioner.Id));
    }
}
