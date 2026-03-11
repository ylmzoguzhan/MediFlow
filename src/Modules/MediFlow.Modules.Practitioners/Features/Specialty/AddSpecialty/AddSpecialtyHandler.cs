namespace MediFlow.Modules.Practitioners.Features.Specialty.AddSpecialty;

public class AddSpecialtyValidator : AbstractValidator<AddSpecialtyCommand>
{
    public AddSpecialtyValidator()
    {
        RuleFor(op => op.Name).NotEmpty();
        RuleFor(op => op.Code).NotEmpty();
    }
}

public record AddSpecialtyCommand(string Name, string Code) : IRequest<Result<AddSpecialtyResponse>>;
public record AddSpecialtyResponse(Guid Id);
public class AddSpecialtyHandler(PractitionersDbContext dbContext) : IRequestHandler<AddSpecialtyCommand, Result<AddSpecialtyResponse>>
{
    public async Task<Result<AddSpecialtyResponse>> Handle(AddSpecialtyCommand request, CancellationToken ct)
    {
        var nameExisting = await dbContext.Specialties.AnyAsync(op => op.Name == request.Name, ct);
        if (nameExisting)
            return Result<AddSpecialtyResponse>.Failure(SpecialtyError.NameAlreadyExists);
        var codeExisting = await dbContext.Specialties.AnyAsync(op => op.Code == request.Code, ct);
        if (codeExisting)
            return Result<AddSpecialtyResponse>.Failure(SpecialtyError.CodeAlreadyExists);
        var specialtyResult = MediFlow.Modules.Practitioners.Domain.Specialty.Specialty.Create(request.Name, request.Code);
        if (!specialtyResult.IsSuccess)
            return Result<AddSpecialtyResponse>.Failure(specialtyResult.Error);
        await dbContext.Specialties.AddAsync(specialtyResult.Value!, ct);
        await dbContext.SaveChangesAsync(ct);
        return Result<AddSpecialtyResponse>.Success(new(specialtyResult.Value!.Id));
    }
}
