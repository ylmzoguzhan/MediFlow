namespace MediFlow.Modules.Practitioners.Features.Practitioners.ListPractitioners;

public static class ListPractitionersEndpoint
{
    public static void MapListPractitionersEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/practitioners", async (string? term, int page, int pageSize, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new ListPractitionersQuery(term, page, pageSize), ct);
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        });
    }
}
