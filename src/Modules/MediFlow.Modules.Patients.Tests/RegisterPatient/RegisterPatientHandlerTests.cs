
using MediFlow.Modules.Patients.Domain;
using MediFlow.Modules.Patients.Infrastructure.Persistence;
using MediFlow.Modules.Patients.RegisterPatient;
using Microsoft.EntityFrameworkCore;
using Xunit;
namespace MediFlow.Modules.Patients.Tests.RegisterPatient;

public class RegisterPatientHandlerTests
{
    private PatientsDbContext CreateDbContext()
    {
        var options = new DbContextOptionsBuilder<PatientsDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;

        return new PatientsDbContext(options);
    }

    [Fact]
    public async Task Should_Return_Failure_When_Email_Already_Exists()
    {
        // Arrange
        using var dbContext = CreateDbContext();

        var existingPatient = new Patient(
            "Oğuz",
            "Yılmaz",
            "test@test.com",
            "0555",
            new DateTime(1995, 5, 25));

        dbContext.Patients.Add(existingPatient);
        await dbContext.SaveChangesAsync();

        var handler = new RegisterPatientHandler(dbContext);

        var command = new RegisterPatientCommand(
            "Ahmet",
            "Demir",
            "test@test.com",
            "055555",
            new DateTime(1990, 1, 1));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(!result.IsSuccess);
        Assert.NotNull(result.Error);
        Assert.Equal(PatientErrors.EmailAlreadyExists.Code, result.Error!.Code);
    }

    [Fact]
    public async Task Should_Create_Patient_When_Email_Does_Not_Exist()
    {
        // Arrange
        using var dbContext = CreateDbContext();

        var handler = new RegisterPatientHandler(dbContext);

        var command = new RegisterPatientCommand(
            "Oğuz",
            "Yılmaz",
            "oguz@test.com",
            "05555",
            new DateTime(1995, 5, 25));

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.NotNull(result.Value);
        Assert.NotEqual(Guid.Empty, result.Value!.PatientId);

        var patientInDb = await dbContext.Patients
            .FirstOrDefaultAsync(x => x.Email == "oguz@test.com");

        Assert.NotNull(patientInDb);
        Assert.Equal("Oğuz", patientInDb!.FirstName);
        Assert.Equal("Yılmaz", patientInDb.LastName);
    }
}