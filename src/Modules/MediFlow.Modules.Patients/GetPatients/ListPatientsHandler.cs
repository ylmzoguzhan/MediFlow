using BuildingBlocks;
using MediatR;
using MediFlow.Modules.Patients.Domain;
using MediFlow.Modules.Patients.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Patients.GetPatients;

public record ListPatientsQuery(int Page = 1, int PageSize = 10) : IRequest<Result<ListItem<PatientItem>>>;
public record PatientItem(string FirstName, string LastName, string PhoneNumber, string Email, DateTime DateOfBirth);
public record ListItem<T>(IEnumerable<T> Data, int Page, int PageSize, int TotalCount);
public class ListPatientsHandler(PatientsDbContext dbContext) : IRequestHandler<ListPatientsQuery, Result<ListItem<PatientItem>>>
{
    public async Task<Result<ListItem<PatientItem>>> Handle(ListPatientsQuery request, CancellationToken cancellationToken)
    {
        var query = dbContext.Patients.AsNoTracking().OrderBy(x => x.FirstName).ThenBy(x => x.LastName);

        var totalCount = await query.CountAsync(cancellationToken);

        var patients = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .Select(x => new PatientItem(x.FirstName, x.LastName, x.PhoneNumber, x.Email, x.DateOfBirth))
            .ToListAsync(cancellationToken);

        return Result<ListItem<PatientItem>>.Success(new(patients, request.Page, request.PageSize, totalCount));
    }
}
