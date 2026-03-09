using FluentValidation.TestHelper;
using MediFlow.Modules.Patients.RegisterPatient;
using Xunit;

public class RegisterPatientValidatorTests
{
    private readonly RegisterPatientValidator _validator;

    public RegisterPatientValidatorTests()
    {
        _validator = new RegisterPatientValidator();
    }

    [Fact]
    public void Should_Not_Have_Error_When_Command_Is_Valid()
    {
        // Arrange
        var command = new RegisterPatientCommand(
            "Oğuz",
            "Yılmaz",
            "test@test.com",
            "0555555",
            new DateTime(1995, 5, 25)
        );

        // Act
        var result = _validator.TestValidate(command);

        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Fact]
    public void Should_Have_Error_When_FirstName_Is_Empty()
    {
        var command = new RegisterPatientCommand(
            "",
            "Yılmaz",
            "test@test.com",
            "0555555",
            new DateTime(1995, 5, 25)
        );

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.FirstName);
    }

    [Fact]
    public void Should_Have_Error_When_Email_Is_Invalid()
    {
        var command = new RegisterPatientCommand(
            "Oğuz",
            "Yılmaz",
            "invalid-email",
            "0555",
            new DateTime(1995, 5, 25)
        );

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.Email);
    }

    [Fact]
    public void Should_Have_Error_When_DateOfBirth_Is_In_The_Future()
    {
        var command = new RegisterPatientCommand(
            "Oğuz",
            "Yılmaz",
            "test@test.com",
            "55555",
            DateTime.Today.AddDays(1)
        );

        var result = _validator.TestValidate(command);

        result.ShouldHaveValidationErrorFor(x => x.DateOfBirth);
    }
}