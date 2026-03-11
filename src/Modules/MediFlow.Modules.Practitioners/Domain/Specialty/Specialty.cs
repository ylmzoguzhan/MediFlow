namespace MediFlow.Modules.Practitioners.Domain.Specialty;

public class Specialty : BaseEntity
{
    public string Name { get; private set; }
    public string Code { get; private set; }
    public bool IsActive { get; private set; }
    private Specialty(string name, string code)
    {
        Id = Guid.NewGuid();
        Name = name;
        Code = code;
        IsActive = true;
    }
    public static Result<Specialty> Create(string name, string code)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result<Specialty>.Failure(SpecialtyError.NameRequired);

        if (string.IsNullOrWhiteSpace(code))
            return Result<Specialty>.Failure(SpecialtyError.CodeRequired);

        var specialty = new Specialty(
            name.Trim(),
            code.Trim().ToUpperInvariant());

        return Result<Specialty>.Success(specialty);
    }
    public void Deactivate()
    {
        IsActive = false;
    }
    public void Activate()
    {
        IsActive = true;
    }
}
