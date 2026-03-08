using FluentValidation;
using MediatR;
using MediFlow.Modules.Patients.GetPatientDetails;
using MediFlow.Modules.Patients.Infrastructure.Persistence;
using MediFlow.Modules.Patients.RegisterPatient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediFlow.Modules.Patients;

public static class PatientsModule
{
    public static IServiceCollection AddPatientsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PatientsDb");
        services.AddDbContext<PatientsDbContext>(options =>
        {
            options.UseNpgsql(connectionString);
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(PatientsModule).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(PatientsModule).Assembly);
        services.AddTransient(
    typeof(IPipelineBehavior<,>),
    typeof(ValidationBehavior<,>)
);
        return services;
    }
    public static IEndpointRouteBuilder MapPatientsEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapRegisterPatientEndpoint();
        app.MapGetPatientDetailsEndpoint();
        return app;
    }
}
