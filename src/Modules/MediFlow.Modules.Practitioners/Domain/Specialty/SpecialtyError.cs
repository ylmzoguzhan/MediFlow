namespace MediFlow.Modules.Practitioners.Domain.Specialty;

public static class SpecialtyError
{
    public static readonly Error NameAlreadyExists = new(
     "Specialty.Name",
     "İsim kullanımda");
    public static readonly Error CodeAlreadyExists = new(
    "Specialty.Code",
    "Code kullanımda");
    public static readonly Error NameRequired = new(
    "Specialty.Name",
    "İsim zorunlu");
    public static readonly Error CodeRequired = new(
    "Specialty.Code",
    "Code zorunlu");
}
