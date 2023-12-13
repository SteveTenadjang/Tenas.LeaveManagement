using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Tenas.LeaveManagement.Domain;

namespace Tenas.LeaveManagement.Persistance.Configurations.Entities
{
    public class LeaveTypeConfiguration : IEntityTypeConfiguration<LeaveType>
    {
        public void Configure(EntityTypeBuilder<LeaveType> builder)
        {
            builder.HasData(
              new LeaveType
              {
                  Id = Guid.NewGuid(),
                  DefaultDays = new Random().Next(2, 50),
                  Name = "Vacation"
              },
              new LeaveType
              {
                  Id = Guid.NewGuid(),
                  DefaultDays = new Random().Next(2, 50),
                  Name = "Sick"
              }
            );
        }
    }
}
