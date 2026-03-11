using MediFlow.Api.Middleware;
using MediFlow.Modules.Patients;
using MediFlow.Modules.Practitioners;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();
builder.Services.AddPatientsModule(builder.Configuration);
builder.Services.AddPractitionersModule(builder.Configuration);
builder.Services.AddProblemDetails();
builder.Services.AddExceptionHandler<ValidationExceptionHandlingMiddleware>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}
app.UseExceptionHandler();

app.MapPatientsEndpoints();
app.MapPractitionersEndpoints();

app.UseHttpsRedirection();

app.Run();

