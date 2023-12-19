using MediatR;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveTypeCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseQueryResponse> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            await _unitOfWork.GenericRepository<LeaveType>().Delete(request.Id);
            await _unitOfWork.Save();
            return new BaseQueryResponse { Message = "Deleted Successfully" };
        }
    }
}
