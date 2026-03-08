namespace MediFlow.Modules.Patients.Domain;

public class Patient
{
    public Guid Id { get; private set; }
    public string FirstName { get; private set; }
    public string LastName { get; private set; }
    public string Email { get; private set; }
    public string PhoneNumber { get; set; }
    public DateTime DateOfBirth { get; private set; }

    private Patient() { }

    public Patient(string firstName, string lastName, string email, string phoneNumber, DateTime dateOfBirth)
    {
        Id = Guid.NewGuid();
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
    public void UpdatePatient(string firstName, string lastname, string phoneNumber, DateTime dateOfBirth)
    {
        FirstName = firstName;
        LastName = lastname;
        PhoneNumber = phoneNumber;
        DateOfBirth = dateOfBirth;
    }
}
