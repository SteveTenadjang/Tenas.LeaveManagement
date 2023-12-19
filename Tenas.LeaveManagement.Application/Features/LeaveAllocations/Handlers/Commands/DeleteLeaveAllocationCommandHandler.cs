using MediatR;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<BaseQueryResponse> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.GenericRepository<LeaveAllocation>().Delete(request.Id);
            await _unitOfWork.Save();

            return new BaseQueryResponse
            {
                Message = "Deleted Successfully",
            };
        }
    }
}
