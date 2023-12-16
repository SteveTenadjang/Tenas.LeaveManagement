using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Tenas.LeaveManagement.Identity.Configurations;
using Tenas.LeaveManagement.Identity.Models;

namespace Tenas.LeaveManagement.Identity
{
    public class TenasLeaveManagementIdentityDbContext : IdentityDbContext<ApplicationUser>
    {
        public TenasLeaveManagementIdentityDbContext(DbContextOptions<TenasLeaveManagementIdentityDbContext> options)
            :base(options)
        { }

        protected override void OnModelCreating(ModelBuilder modelBuilder) 
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new RoleConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new UserRoleConfiguration());
        }
    }
}
