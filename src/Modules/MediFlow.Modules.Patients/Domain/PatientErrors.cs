namespace MediFlow.Modules.Patients.Domain;

public static class PatientErrors
{
    public static readonly Error EmailAlreadyExists = new(
         "Patient.EmailExists",
         "Bu e-posta adresi zaten kullanımda.");
}
