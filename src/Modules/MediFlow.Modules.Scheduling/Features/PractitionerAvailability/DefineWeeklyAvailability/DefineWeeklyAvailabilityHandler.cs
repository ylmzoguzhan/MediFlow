using BuildingBlocks;
using MediatR;
using MediFlow.Modules.Scheduling.Domain.PractitionerAvailability;
using MediFlow.Modules.Scheduling.Infastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MediFlow.Modules.Scheduling.Features.PractitionerAvailability.DefineWeeklyAvailability;

public record DefineWeeklyAvailabilityRuleRequest(DayOfWeek DayOfWeek, TimeOnly StartTime, TimeOnly EndTime);

public record DefineWeeklyAvailabilityCommand(Guid PractitionerId, List<DefineWeeklyAvailabilityRuleRequest> Rules) : IRequest<Result<DefineWeeklyAvailabilityResponse>>;
public record DefineWeeklyAvailabilityResponse(Guid Id, Guid PractitionerId, int RulesCount);
public class DefineWeeklyAvailabilityHandler(SchedulingDbContext dbContext)
    : IRequestHandler<DefineWeeklyAvailabilityCommand, Result<DefineWeeklyAvailabilityResponse>>
{
    public async Task<Result<DefineWeeklyAvailabilityResponse>> Handle(
        DefineWeeklyAvailabilityCommand request,
        CancellationToken cancellationToken)
    {
        var practitioner = await dbContext.SchedulingPractitionerReadModels
            .FirstOrDefaultAsync(
                x => x.PractitionerId == request.PractitionerId,
                cancellationToken);

        if (practitioner is null)
            return Result<DefineWeeklyAvailabilityResponse>.Failure(
                PractitionerAvailabilityError.PractitionerNotFound);

        if (!practitioner.IsActive)
            return Result<DefineWeeklyAvailabilityResponse>.Failure(
                PractitionerAvailabilityError.PractitionerIsInactive);

        var practitionerAvailability = await dbContext.PractitionerAvailabilities
            .FirstOrDefaultAsync(
                x => x.PractitionerId == request.PractitionerId,
                cancellationToken);

        var isNew = practitionerAvailability is null;

        if (isNew)
        {
            var createResult = MediFlow.Modules.Scheduling.Domain.PractitionerAvailability.PractitionerAvailability.Create(request.PractitionerId);
            if (!createResult.IsSuccess)
                return Result<DefineWeeklyAvailabilityResponse>.Failure(createResult.Error);

            practitionerAvailability = createResult.Value!;
        }

        var rules = new List<WeeklyAvailabilityRule>();

        foreach (var item in request.Rules)
        {
            var ruleResult = WeeklyAvailabilityRule.Create(
                item.DayOfWeek,
                item.StartTime,
                item.EndTime);

            if (!ruleResult.IsSuccess)
                return Result<DefineWeeklyAvailabilityResponse>.Failure(ruleResult.Error);

            rules.Add(ruleResult.Value!);
        }

        var defineResult = practitionerAvailability.DefineWeeklyAvailability(rules);
        if (!defineResult.IsSuccess)
            return Result<DefineWeeklyAvailabilityResponse>.Failure(defineResult.Error);

        if (isNew)
            await dbContext.PractitionerAvailabilities.AddAsync(practitionerAvailability, cancellationToken);

        await dbContext.SaveChangesAsync(cancellationToken);

        return Result<DefineWeeklyAvailabilityResponse>.Success(
            new DefineWeeklyAvailabilityResponse(
                practitionerAvailability.Id,
                practitionerAvailability.PractitionerId,
                practitionerAvailability.Rules.Count));
    }
}