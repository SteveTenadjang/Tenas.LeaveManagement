using Microsoft.OpenApi.Models;
using Tenas.LeaveManagement.API.Endpoints.Base;

namespace Tenas.LeaveManagement.API.Endpoints;

public class SwaggerEndpointDefinition : IEndpointDefinition
{
    public void RegisterServices(IServiceCollection services)
    {
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo { Title = "Tenas Leave Management API", Version = "v1" });
        });
    }

    public void RegisterEndpoints(WebApplication app)
    {
        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "Tenas Leave Management API");
        });
    }
}
