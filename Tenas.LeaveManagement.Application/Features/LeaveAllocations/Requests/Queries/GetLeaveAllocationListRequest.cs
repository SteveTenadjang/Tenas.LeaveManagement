using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationListRequest : IRequest<BaseCommandResponse<List<LeaveAllocationDto>>>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
