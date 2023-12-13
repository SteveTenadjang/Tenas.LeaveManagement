using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Tenas.LeaveManagement.Application
{ 
    public static class ApplicationServiceRegistration
    {
        public static IServiceCollection ConfigureApplicationServices(this IServiceCollection services)
        {
            //If you have a single mapping profile
            //services.AddAutoMapper(typeof(MappingProfile));

            // For several multiple mapping profiles
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }
    }
}
