namespace MediFlow.Modules.Practitioners.Features.Practitioners.CreatePractitioner;

public static class CreatePractitionerEndpoint
{
    public static void MapCreatePractitionerEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/practitioners", async (CreatePractitionerCommand command, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(command, ct);
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Created($"/practitioners/{result.Value!.Id}", result.Value);
        });
    }
}
