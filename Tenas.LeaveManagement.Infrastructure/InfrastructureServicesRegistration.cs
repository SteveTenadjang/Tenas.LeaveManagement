using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Tenas.LeaveManagement.Application.Contracts.Infrastructure;
using Tenas.LeaveManagement.Application.Models.Mail;
using Tenas.LeaveManagement.Infrastructure.Mail;

namespace Tenas.LeaveManagement.Infrastructure
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
            services.AddTransient<IEmailSender, EmailSender>();
            return services;
        }
    }
}
