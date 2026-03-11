namespace MediFlow.Modules.Patients.SearchPatients;

public static class SearchPatientsEndpoint
{
    public static void MapSearchPatientsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/patients/search", async (int page, int pageSize, string term, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new SearchPatientsQuery(term, page, pageSize), ct);
            return Results.Ok(result.Value);
        });
    }
}
