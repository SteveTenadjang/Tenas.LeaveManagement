using MediatR;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries
{
    public class GetLeaveAllocationListRequest : IRequest<BaseQueryResponse>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
