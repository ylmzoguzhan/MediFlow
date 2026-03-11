namespace MediFlow.Modules.Practitioners.Domain.Practitioner;

public class Practitioner : BaseEntity
{
    public string FirstName { get; private set; }
    public string LastName { get; private set; }

    public string Email { get; private set; }
    public string PhoneNumber { get; private set; }

    public Guid SpecialtyId { get; private set; }


    public bool IsActive { get; private set; }

    private Practitioner(string firstName, string lastName, string email, string phoneNumber, Guid specialtyId)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        SpecialtyId = specialtyId;
        IsActive = true;
    }
    public static Result<Practitioner> Create(
    string firstName,
    string lastName,
    string email,
    string phoneNumber,
    Guid specialtyId)
    {
        if (string.IsNullOrEmpty(firstName))
            return Result<Practitioner>.Failure(PractitionerError.NameRequired);
        if (string.IsNullOrEmpty(lastName))
            return Result<Practitioner>.Failure(PractitionerError.LastNameRequired);
        if (string.IsNullOrEmpty(email))
            return Result<Practitioner>.Failure(PractitionerError.EmailRequired);
        if (string.IsNullOrEmpty(phoneNumber))
            return Result<Practitioner>.Failure(PractitionerError.PhoneNumberRequired);
        if (specialtyId == Guid.Empty)
            return Result<Practitioner>.Failure(PractitionerError.PhoneNumberRequired);
        var practitioner = new Practitioner(firstName, lastName, email, phoneNumber, specialtyId);
        return Result<Practitioner>.Success(practitioner);
    }
    public void Deactivate()
    {
        IsActive = false;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void UpdateContact(string email, string phone)
    {
        Email = email;
        PhoneNumber = phone;
    }
}
