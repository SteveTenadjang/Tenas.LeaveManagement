using Tenas.LeaveManagement.Application.DTOs.Common;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveAllocation
{
    public class LeaveAllocationDto : BaseDto
    {
        public int NumberOfDays { get; set; }
        public LeaveTypeDto LeaveTypeDto { get; set; }
        public Guid LeaveTypeId { get; set; }
        public int Period { get; set; }
    }
}
