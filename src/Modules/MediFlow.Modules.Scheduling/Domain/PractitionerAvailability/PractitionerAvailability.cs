using BuildingBlocks;

namespace MediFlow.Modules.Scheduling.Domain.PractitionerAvailability;

public class PractitionerAvailability : BaseEntity
{

    public Guid PractitionerId { get; private set; }

    private readonly List<WeeklyAvailabilityRule> _rules = new();

    public IReadOnlyCollection<WeeklyAvailabilityRule> Rules => _rules.AsReadOnly();

    private PractitionerAvailability() { }

    private PractitionerAvailability(Guid practitionerId)
    {
        Id = Guid.NewGuid();
        PractitionerId = practitionerId;
    }

    public static Result<PractitionerAvailability> Create(Guid practitionerId)
    {
        if (practitionerId == Guid.Empty)
            return Result<PractitionerAvailability>.Failure(PractitionerAvailabilityError.PractitionerIdRequired);
        var availability = new PractitionerAvailability(practitionerId);
        return Result<PractitionerAvailability>.Success(availability);
    }
    public Result DefineWeeklyAvailability(IEnumerable<WeeklyAvailabilityRule> rules)
    {
        var ruleList = rules.ToList();

        if (!ruleList.Any())
            return Result.Failure(PractitionerAvailabilityError.EmptyAvailabilityRules);

        var overlapValidation = ValidateOverlaps(ruleList);
        if (!overlapValidation.IsSuccess)
            return overlapValidation;

        _rules.Clear();
        _rules.AddRange(ruleList);

        return Result.Success();
    }

    private static Result ValidateOverlaps(List<WeeklyAvailabilityRule> rules)
    {
        var groupedByDay = rules.GroupBy(x => x.DayOfWeek);

        foreach (var group in groupedByDay)
        {
            var ordered = group
                .OrderBy(x => x.StartTime)
                .ToList();

            for (int i = 1; i < ordered.Count; i++)
            {
                var previous = ordered[i - 1];
                var current = ordered[i];
 
                if (current.StartTime < previous.EndTime)
                    return Result.Failure(PractitionerAvailabilityError.OverlappingAvailabilityRules);
            }
        }

        return Result.Success();
    }
}

