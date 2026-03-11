using BuildingBlocks.Infrastructure.Persistence.Interceptors;
using FluentValidation;
using MediatR;
using MediFlow.Modules.Patients.GetPatientDetails;
using MediFlow.Modules.Patients.GetPatients;
using MediFlow.Modules.Patients.Infrastructure.Persistence;
using MediFlow.Modules.Patients.RegisterPatient;
using MediFlow.Modules.Patients.SearchPatients;
using MediFlow.Modules.Patients.UpdatePatient;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediFlow.Modules.Patients;

public static class PatientsModule
{
    public static IServiceCollection AddPatientsModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultDb");
        services.AddDbContext<PatientsDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            options.UseNpgsql(connectionString).AddInterceptors(interceptors);
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(PatientsModule).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(PatientsModule).Assembly);
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitiesInterceptor>();
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
        app.MapUpdatePatientProfileEndpoint();
        app.MapListPatientsEndpoint();
        app.MapSearchPatientsEndpoint();
        return app;
    }
}
