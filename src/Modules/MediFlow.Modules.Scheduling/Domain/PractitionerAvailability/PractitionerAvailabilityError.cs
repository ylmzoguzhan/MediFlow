using BuildingBlocks;

namespace MediFlow.Modules.Scheduling.Domain.PractitionerAvailability;

public static class PractitionerAvailabilityError
{
    public static readonly Error InvalidTimeRange = new(
        "WeeklyAvailability.InvalidRange",
        "Başlangıç saati, bitiş saatinden önce olmalıdır.");
    public static readonly Error PractitionerIdRequired = new(
        "PractitionerAvailability.PractitionerId",
        "PractitionerId boş olamaz");
    public static readonly Error EmptyAvailabilityRules = new(
        "PractitionerAvailability.EmptyAvailabilityRules",
        "Kural boş olamaz");
    public static readonly Error OverlappingAvailabilityRules = new(
    "PractitionerAvailability.OverlappingAvailabilityRules",
    "Çakışan kurallar");
    public static readonly Error PractitionerNotFound = new(
    "PractitionerAvailability.Practitione",
    "Practitioner Bulunamadı");
    public static readonly Error PractitionerIsInactive = new(
    "PractitionerAvailability.PractitionerIsInactive",
    "Practitioner Aktif değil");
}
