using MediatR;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveRequestCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<BaseQueryResponse> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.GenericRepository<LeaveRequest>().Delete(request.Id);
            await _unitOfWork.Save();

            return new BaseQueryResponse { Message = "Deleted Successful" };
        }
    }
}
