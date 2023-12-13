using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IGenericRepository<LeaveRequest> _leaveRequestRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(IGenericRepository<LeaveRequest> leaveRequestRepository, IMapper mapper)
        {
            _leaveRequestRepository = leaveRequestRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            try
            {
                var validatedModel = await new UpdateLeaveRequestDtoValidator(_leaveRequestRepository).ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);

                if (!validatedModel.IsValid)
                {
                    response.Success = false;
                    response.Message = "Update Failed";
                    response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                if (request.UpdateLeaveRequestDto is not null)
                {
                    var leaveRequest = _mapper.Map<LeaveRequest>(request.UpdateLeaveRequestDto);
                    await _leaveRequestRepository.Update(leaveRequest);

                    response.Success = true;
                    response.Message = "Updated Successfully";
                    response.Id = leaveRequest.Id;

                }
                else if(request.ChangeLeaveRequestApprovalDto is not null)
                {
                    var leaveRequest = _mapper.Map<LeaveRequest>(request.ChangeLeaveRequestApprovalDto);
                    await _leaveRequestRepository.Update(leaveRequest);
                    response.Success = true;
                    response.Message = "Approval changed Successfully";
                    response.Id = leaveRequest.Id;
                }

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
