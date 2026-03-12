using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace MediFlow.Modules.Scheduling.Features.PractitionerAvailability.DefineWeeklyAvailability;

public static class DefineWeeklyAvailabilityEndpoint
{
    public static IEndpointRouteBuilder MapDefineWeeklyAvailability(this IEndpointRouteBuilder app)
    {
        app.MapPut("/practitioners/{practitionerId:guid}/availability/weekly",
            async (
                Guid practitionerId,
                DefineWeeklyAvailabilityRequest request,
                ISender sender,
                CancellationToken cancellationToken) =>
            {
                var command = new DefineWeeklyAvailabilityCommand(
                    practitionerId,
                    request.Rules);

                var result = await sender.Send(command, cancellationToken);

                if (result.IsFailure())
                    return Results.BadRequest(result.Error);

                return Results.Ok(result.Value);
            });

        return app;
    }
    public sealed record DefineWeeklyAvailabilityRequest(
    List<DefineWeeklyAvailabilityRuleRequest> Rules);

}
