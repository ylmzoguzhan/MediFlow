namespace MediFlow.Modules.Practitioners.Features.Specialty.AddSpecialty;

public static class AddSpecialtyEndpoint
{
    public static void MapAddSpecialtyEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/practitioners/specialties", async (AddSpecialtyCommand command, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(command, ct);
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Created($"/specialties/{result.Value!.Id}", result.Value);
        });
    }
}
