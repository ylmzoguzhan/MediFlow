
namespace MediFlow.Modules.Practitioners.Features.Practitioners.ListPractitioners;

public record ListPractitionersQuery(string Term, int Page, int PageSize) : IRequest<Result<ListItem<PractitionerDto>>>;

public class ListPractitionersHandler(PractitionersDbContext dbContext) : IRequestHandler<ListPractitionersQuery, Result<ListItem<PractitionerDto>>>
{
    public async Task<Result<ListItem<PractitionerDto>>> Handle(ListPractitionersQuery request, CancellationToken cancellationToken)
    {
        var practitioners = dbContext.Practitioners.AsNoTracking();
        if (!string.IsNullOrEmpty(request.Term))
            practitioners = practitioners.Where(op => op.FirstName.Contains(request.Term) || op.LastName.Contains(request.Term) || op.PhoneNumber.Contains(request.Term) || op.Email.Contains(request.Term));
        var query = from p in practitioners
                    join s in dbContext.Specialties on p.SpecialtyId equals s.Id
                    select new PractitionerDto(
                        p.Id,
                        p.FirstName,
                        p.LastName,
                        p.Email,
                        p.PhoneNumber,
                        s.Name,
                        s.Id
                    );
        var totalCount = await query.CountAsync();
        var practitionerDtoList = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        return Result<ListItem<PractitionerDto>>.Success(new(practitionerDtoList, request.Page, request.PageSize, totalCount));
    }
}
