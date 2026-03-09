using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MediFlow.Modules.Patients.GetPatients;

public static class ListPatientsEndpoint
{
    public static void MapListPatientsEndpoint(this IEndpointRouteBuilder app)
    {
        app.MapGet("/patients", async (int page, int pageSize, ISender sender, CancellationToken ct) =>
        {
            var result = await sender.Send(new ListPatientsQuery(page, pageSize), ct);
            return Results.Ok(result.Value);
        });
    }
}
