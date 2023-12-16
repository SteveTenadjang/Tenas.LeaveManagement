using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class UpdateLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public  Guid Id { get; set; }
        public UpdateLeaveRequestDto UpdateLeaveRequestDto { get; set; }
        public ChangeLeaveRequestApprovalDto ChangeLeaveRequestApprovalDto { get; set; }
    }
}
