namespace Tenas.LeaveManagement.API.Endpoints.Base;

public interface IEndpointDefinition
{
    void RegisterServices(IServiceCollection services);
    void RegisterEndpoints(WebApplication app);
}
