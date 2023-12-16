using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Tenas.LeaveManagement.Application.Contracts.Identity;
using Tenas.LeaveManagement.Application.Models.Identity;
using Tenas.LeaveManagement.Identity.Models;
using Tenas.LeaveManagement.Identity.Services;

namespace Tenas.LeaveManagement.Identity
{
    public static class IdentityServiceRegistration
    {
        public static IServiceCollection ConfigureIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));

            services.AddDbContext<TenasLeaveManagementIdentityDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("TenasLeaveManagementIdentityConnectionString"));
            });
            //options.UseSqlServer(configuration.GetConnectionString("TenasLeaveManagementIdentityConnectionString"),
            //        b => b.MigrationsAssembly(typeof(TenasLeaveManagementIdentityDbContext).Assembly.FullName)
            //    );
            //});

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TenasLeaveManagementIdentityDbContext>().AddDefaultTokenProviders();

            services.AddTransient<IAuthService, AuthService>();
            services.AddTransient<IUserService, UserService>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ClockSkew = TimeSpan.Zero,
                        ValidIssuer = configuration["JwtSettings:Issuer"],
                        ValidAudience = configuration["JwtSettings:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:key"]))
                    };
                });

            return services;
        }
    }
}
