namespace MediFlow.Modules.Practitioners.Features.Specialty.ListSpecialties;

public record ListSpecialtiesQuery(string? Term = "", int Page = 1, int PageSize = 10) : IRequest<Result<ListItem<SpecialtyItem>>>;
public record SpecialtyItem(Guid Id, string Name, string Code);
public class ListSpecialtiesHandler(PractitionersDbContext dbContext) : IRequestHandler<ListSpecialtiesQuery, Result<ListItem<SpecialtyItem>>>
{
    public async Task<Result<ListItem<SpecialtyItem>>> Handle(ListSpecialtiesQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Specialties.AsNoTracking();
        if (!string.IsNullOrWhiteSpace(request.Term))
            query = query.Where(op => op.Name.Contains(request.Term) || op.Code.Contains(request.Term));
        var totalCount = await query.CountAsync(cancellationToken);

        var specialties = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new SpecialtyItem(x.Id, x.Name, x.Code))
            .ToListAsync(cancellationToken);

        return Result<ListItem<SpecialtyItem>>.Success(new(specialties, request.Page, request.PageSize, totalCount));
    }
}
