namespace MediFlow.Modules.Practitioners.Features.Specialty.ListSpecialties;

public static class ListSpecialtiesEndpoint
{
    public static void MapListSpecialtiesEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/practitioners/specialties", async (string? term, int page, int pageSize, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new ListSpecialtiesQuery(term, page, pageSize), ct);
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        });
    }
}
