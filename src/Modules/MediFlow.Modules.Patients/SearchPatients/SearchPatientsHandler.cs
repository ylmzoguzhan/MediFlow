namespace MediFlow.Modules.Patients.SearchPatients;

public class SearchPatientsValidator : AbstractValidator<SearchPatientsQuery>
{
    public SearchPatientsValidator()
    {
        RuleFor(op => op.Term).NotEmpty().MinimumLength(2);
        RuleFor(op => op.Page).GreaterThan(0);
        RuleFor(op => op.PageSize).GreaterThan(0);
    }
}

public record SearchPatientsQuery(string Term, int Page, int PageSize) : IRequest<Result<ListItem<PatientItem>>>;
public class SearchPatientsHandler(PatientsDbContext dbContext) : IRequestHandler<SearchPatientsQuery, Result<ListItem<PatientItem>>>
{
    public async Task<Result<ListItem<PatientItem>>> Handle(SearchPatientsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Patients.Where(op => op.FirstName.Contains(request.Term) || op.LastName.Contains(request.Term) || op.PhoneNumber.Contains(request.Term) || op.Email.Contains(request.Term)).AsNoTracking();
        var totalCount = await query.CountAsync();
        var patients = await query.Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize).Select(x => new PatientItem(x.FirstName, x.LastName, x.PhoneNumber, x.Email, x.DateOfBirth))
            .ToListAsync(cancellationToken);
        return Result<ListItem<PatientItem>>.Success(new(patients, request.Page, request.PageSize, totalCount));

    }
}
