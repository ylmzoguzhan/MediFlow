using BuildingBlocks.Infrastructure.Persistence.Interceptors;
using MediFlow.Modules.Practitioners.Features.Practitioners.CreatePractitioner;
using MediFlow.Modules.Practitioners.Features.Practitioners.GetDetailPractitioner;
using MediFlow.Modules.Practitioners.Features.Practitioners.ListPractitioners;
using MediFlow.Modules.Practitioners.Features.Practitioners.UpdatePractitionerContact;
using MediFlow.Modules.Practitioners.Features.Specialty.AddSpecialty;
using MediFlow.Modules.Practitioners.Features.Specialty.ListSpecialties;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediFlow.Modules.Practitioners;

public static class PractitionersModule
{
    public static IServiceCollection AddPractitionersModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultDb");
        services.AddDbContext<PractitionersDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            options.UseNpgsql(connectionString).AddInterceptors(interceptors);
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(PractitionersModule).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(PractitionersModule).Assembly);
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitiesInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddTransient(
typeof(IPipelineBehavior<,>),
typeof(ValidationBehavior<,>)
);
        return services;
    }
    public static IEndpointRouteBuilder MapPractitionersEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapAddSpecialtyEndpoint();
        app.MapListSpecialtiesEndpoint();
        app.MapCreatePractitionerEndpoint();
        app.MapListPractitionersEndpoint();
        app.MapGetDetailPractitioner();
        app.MapUpdatePractitionerContact();
        return app;
    }
}
