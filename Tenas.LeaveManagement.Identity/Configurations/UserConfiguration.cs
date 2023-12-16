using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tenas.LeaveManagement.Identity.Models;

namespace Tenas.LeaveManagement.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();

            builder.HasData(
                new ApplicationUser
                {
                    Id = "941c74e2-1ef3-44f2-b213-3efbe1926fce",
                    Email = "stevetenadjang@gmail.com",
                    NormalizedEmail = "stevetenadjang@gmail.com".ToUpper(),
                    FirstName = "Steve",
                    LastName = "Tenadjang",
                    UserName = "stevetenadjang",
                    NormalizedUserName = "stevetenadjang".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "tenas@123"),
                    EmailConfirmed = true
                },

                new ApplicationUser
                {
                    Id = "b279a88e-16a3-4f81-bd6d-6f97c350bb08",
                    Email = "stevetenas@gmail.com",
                    NormalizedEmail = "stevetenas@gmail.com".ToUpper(),
                    FirstName = "Steve",
                    LastName = "Tenas",
                    UserName = "SteveTenas",
                    NormalizedUserName = "stevetenas".ToUpper(),
                    PasswordHash = hasher.HashPassword(null, "tenas@123"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
