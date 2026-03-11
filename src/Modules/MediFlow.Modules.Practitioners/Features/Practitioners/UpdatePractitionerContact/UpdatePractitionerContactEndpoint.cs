namespace MediFlow.Modules.Practitioners.Features.Practitioners.UpdatePractitionerContact;

public static class UpdatePractitionerContactEndpoint
{
    public static void MapUpdatePractitionerContact(this IEndpointRouteBuilder app)
    {
        app.MapPut("practitioner/{id}", async (Guid id, UpdatePractitionerContactRequest command, ISender sender) =>
        {
            var result = await sender.Send(new UpdatePractitionerContactCommand(id, command.PhoneNumber, command.Email));
            if (!result.IsSuccess)
                return Results.BadRequest(result.Error);
            return Results.Ok(result.Value);
        });
    }
    public record UpdatePractitionerContactRequest(string Email, string PhoneNumber);
}
