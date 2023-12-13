using MediatR;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands
{
    public class DeleteLeaveRequestCommand : IRequest<BaseCommandResponse>
    {
        public Guid Id { get; set; }
    }
}
