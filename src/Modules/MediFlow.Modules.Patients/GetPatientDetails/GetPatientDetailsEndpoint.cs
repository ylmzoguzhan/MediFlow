namespace MediFlow.Modules.Patients.GetPatientDetails;

public static class GetPatientDetailsEndpoint
{
    public static void MapGetPatientDetailsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/patients/{id}", async (Guid id, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new GetPatientDetailsQuery(id), ct);
            if (!result.IsSuccess)
                return Results.NotFound(result.Error);
            return Results.Ok(result.Value);
        });
    }
}
