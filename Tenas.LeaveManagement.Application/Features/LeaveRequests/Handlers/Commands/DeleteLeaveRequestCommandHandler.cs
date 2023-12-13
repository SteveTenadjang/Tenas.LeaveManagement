using MediatR;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class DeleteLeaveRequestCommandHandler : IRequestHandler<DeleteLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IGenericRepository<LeaveRequest> _leaveRequestRepository;

        public DeleteLeaveRequestCommandHandler(IGenericRepository<LeaveRequest> leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
        }

        public async Task<BaseCommandResponse> Handle(DeleteLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            try
            {
                await _leaveRequestRepository.Delete(request.Id);
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
