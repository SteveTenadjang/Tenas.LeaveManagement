using Tenas.LeaveManagement.Application.DTOs.Common;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveRequest
{
    public class ChangeLeaveRequestApprovalDto : BaseDto
    {
        public bool? Approved { get; set; }
    }
}
