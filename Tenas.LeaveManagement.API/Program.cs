using Tenas.LeaveManagement.Application;
using Tenas.LeaveManagement.Infrastructure;
using Tenas.LeaveManagement.Persistance;
using Tenas.LeaveManagement.API.Endpoints.Base;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConfigureApplicationServices();
builder.Services.ConfigurePersistanceServices(builder.Configuration);
builder.Services.ConfigureInfrastructureServices(builder.Configuration);

builder.Services.AddEndpointDefinitions(typeof(Program).Assembly);

builder.Services.AddCors( o =>
{
    o.AddPolicy(
        "CorsPolicy",
        builder => builder.AllowAnyOrigin()
        .AllowAnyHeader() 
        .AllowAnyMethod()
    );
});
var app = builder.Build();
app.UseHttpsRedirection();
app.UseEndpointDefinitions();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.Run();