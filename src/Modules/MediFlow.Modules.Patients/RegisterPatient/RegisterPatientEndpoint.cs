using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Routing;

namespace MediFlow.Modules.Patients.RegisterPatient;

public static class RegisterPatientEndpoint
{
    public static void MapRegisterPatientEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapPost("/patients", async (RegisterPatientCommand command, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(command, ct);
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Created($"/patients/{result.Value!.PatientId}", result.Value);
        });
    }
}
