using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenas.LeaveManagement.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "62c05257-0b5a-485b-8995-74aa2ab78e0e",
                    UserId = "941c74e2-1ef3-44f2-b213-3efbe1926fce"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "85f324d0-db9c-4722-bca2-9fba2c31e074",
                    UserId = "b279a88e-16a3-4f81-bd6d-6f97c350bb08"
                }
            );
        }
    }
}
