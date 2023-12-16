using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Tenas.LeaveManagement.Identity.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
    {
        public void Configure(EntityTypeBuilder<IdentityRole> builder)
        {
            builder.HasData(
                new IdentityRole
                {
                    Id = "85f324d0-db9c-4722-bca2-9fba2c31e074",
                    Name = "Employee",
                    NormalizedName = "EMPLOYEE",
                },   
                
                new IdentityRole
                {
                    Id = "62c05257-0b5a-485b-8995-74aa2ab78e0e",
                    Name = "Administrator",
                    NormalizedName = "ADMINISTRATOR",
                }    
                
            );
        }
    }
}
