namespace MediFlow.Modules.Practitioners.Features.Practitioners.GetDetailPractitioner;

public static class GetDetailPractitionerEndpoint
{
    public static void MapGetDetailPractitioner(this IEndpointRouteBuilder app)
    {
        app.MapGet("/practitioners/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new GetDetailPractitionerQuery(id));
            if (!result.IsSuccess)
                return Results.NotFound(result.Error);
            return Results.Ok(result.Value);
        });
    }
}
