using BuildingBlocks;

namespace MediFlow.Modules.Scheduling.Domain.PractitionerAvailability;

public class WeeklyAvailabilityRule
{
    public DayOfWeek DayOfWeek { get; }
    public TimeOnly StartTime { get; }
    public TimeOnly EndTime { get; }

    private WeeklyAvailabilityRule() { }

    private WeeklyAvailabilityRule(
        DayOfWeek dayOfWeek,
        TimeOnly startTime,
        TimeOnly endTime)
    {
        DayOfWeek = dayOfWeek;
        StartTime = startTime;
        EndTime = endTime;
    }
    public static Result<WeeklyAvailabilityRule> Create(DayOfWeek dayOfWeek, TimeOnly startTime, TimeOnly endTime)
    {
        if (startTime >= endTime)
            return Result<WeeklyAvailabilityRule>.Failure(PractitionerAvailabilityError.InvalidTimeRange);
        var weeklyAvailabilityRule = new WeeklyAvailabilityRule(dayOfWeek, startTime, endTime);
        return Result<WeeklyAvailabilityRule>.Success(weeklyAvailabilityRule);
    }
}