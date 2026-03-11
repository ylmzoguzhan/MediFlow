namespace MediFlow.Modules.Patients.UpdatePatient;

public record UpdatePatientProfileRequest(string FirstName, string LastName, string PhoneNumber, DateTime DateOfBirth);
public static class UpdatePatientProfileEndpoint
{
    public static void MapUpdatePatientProfileEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPut("/patients/{id}/profile", async (Guid id, UpdatePatientProfileRequest request, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new UpdatePatientProfileCommand(id, request.FirstName, request.LastName, request.PhoneNumber, request.DateOfBirth), ct);
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        });
    }
}
