namespace MediFlow.Modules.Practitioners.Domain.Practitioner;

public static class PractitionerError
{
    public static readonly Error NameRequired = new(
    "Practitioner.Name",
    "Ad zorunlu");
    public static readonly Error LastNameRequired = new(
    "Practitioner.LastName",
    "Soyad zorunlu");
    public static readonly Error EmailRequired = new(
    "Practitioner.Email",
    "Email zorunlu");
    public static readonly Error PhoneNumberRequired = new(
    "Practitioner.PhoneNumber",
    "Telefon numarası zorunlu");
    public static readonly Error SpecialtyIdRequired = new(
    "Practitioner.SpecialtyId",
    "SpecialtyId zorunlu");
    public static readonly Error NotFound = new(
    "Practitioner.NotFound",
    "Practitioner Bulunamadı");
}
