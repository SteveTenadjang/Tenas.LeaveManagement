using MediatR;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries
{
    public class GetLeaveRequestListRequest : IRequest<BaseQueryResponse>
    {
        public bool IsLoggedInUser { get; set; }
    }
}
