using MediatR;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class DeleteLeaveAllocationCommandHandler : IRequestHandler<DeleteLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteLeaveAllocationCommandHandler(IUnitOfWork unitOfWork)
            => _unitOfWork = unitOfWork;

        public async Task<BaseCommandResponse> Handle(DeleteLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            try
            {
                await _unitOfWork.GenericRepository<LeaveAllocation>().Delete(request.Id);
                await _unitOfWork.Save();
                response.Success = true;
                response.Message = "Deleted Successfully";
                response.Id = request.Id;
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = "Operation Failed";
                response.Errors.Add(ex.Message);
            }
            return response;
        }
    }
}
