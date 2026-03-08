using FluentValidation;

namespace MediFlow.Modules.Patients.RegisterPatient;

public class RegisterPatientValidator : AbstractValidator<RegisterPatientCommand>
{
    public RegisterPatientValidator()
    {
        RuleFor(op => op.FirstName).NotEmpty();
        RuleFor(op => op.LastName).NotEmpty();
        RuleFor(op => op.Email).NotEmpty().EmailAddress();
        RuleFor(op => op.PhoneNumber).NotEmpty();
        RuleFor(x => x.DateOfBirth).LessThanOrEqualTo(DateTime.Today);
    }
}
