using BuildingBlocks.Infrastructure.Persistence.Interceptors;
using FluentValidation;
using MediFlow.Modules.Scheduling.Infastructure.Persistence;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using MediFlow.Modules.Scheduling.Features.PractitionerAvailability.DefineWeeklyAvailability;
namespace MediFlow.Modules.Scheduling;

public static class SchedulingModule
{
    public static IServiceCollection AddSchedulingModule(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultDb");
        services.AddDbContext<SchedulingDbContext>((sp, options) =>
        {
            var interceptors = sp.GetServices<ISaveChangesInterceptor>();
            options.UseNpgsql(connectionString).AddInterceptors(interceptors);
        });
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(SchedulingModule).Assembly);
        });
        services.AddValidatorsFromAssembly(typeof(SchedulingModule).Assembly);
        services.AddScoped<ISaveChangesInterceptor, AuditableEntitiesInterceptor>();

        return services;
    }
    public static IEndpointRouteBuilder MapSchedulingEndpoints(this IEndpointRouteBuilder app)
    {
        app.MapDefineWeeklyAvailability();
        return app;
    }
}
