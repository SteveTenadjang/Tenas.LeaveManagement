using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest : IRequest<BaseCommandResponse<List<LeaveRequestListDto>>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
