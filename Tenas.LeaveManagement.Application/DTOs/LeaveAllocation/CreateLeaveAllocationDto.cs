using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class CreateLeaveAllocationDto : ILeaveAllocationDto
    {
        public int NumberOfDays { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int Period { get; set; }

    }
}
