
namespace MediFlow.Modules.Practitioners.Features.Practitioners.GetDetailPractitioner;

public class GetDetailPractitionerValidator : AbstractValidator<GetDetailPractitionerQuery>
{
    public GetDetailPractitionerValidator()
    {
        RuleFor(op => op.Id).NotEmpty();
    }
}
public record GetDetailPractitionerQuery(Guid Id) : IRequest<Result<PractitionerDto>>;
public class GetDetailPractitionerHandler(PractitionersDbContext dbContext) : IRequestHandler<GetDetailPractitionerQuery, Result<PractitionerDto>>
{
    public async Task<Result<PractitionerDto>> Handle(GetDetailPractitionerQuery request, CancellationToken cancellationToken)
    {
        var existing = await dbContext.Practitioners.AnyAsync(op => op.Id == request.Id);
        if (!existing)
            Result<PractitionerDto>.Failure(PractitionerError.NotFound);
        var practitioners = dbContext.Practitioners.AsNoTracking();
        var practitionerDto = await (from p in dbContext.Practitioners.AsNoTracking()
                                     join s in dbContext.Specialties on p.SpecialtyId equals s.Id
                                     select new PractitionerDto(p.Id, p.FirstName, p.LastName, p.Email, p.PhoneNumber, s.Name, s.Id))
               .FirstOrDefaultAsync();
        return Result<PractitionerDto>.Success(practitionerDto!);
    }
}
