using Tenas.LeaveManagement.Application.DTOs.Common;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;
using Tenas.LeaveManagement.Application.Models.Identity;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class LeaveRequestListDto : BaseDto
    {
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public LeaveTypeDto LeaveTypeDto { get; set; }
        public DateTime DateRequested { get; set; }
        public bool? Approved { get; set; }
        public Employee Employee { get; set; }
        public Guid EmployeeId { get; set; }
    }
}
