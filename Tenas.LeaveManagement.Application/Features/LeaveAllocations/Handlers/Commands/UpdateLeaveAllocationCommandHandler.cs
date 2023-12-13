using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IGenericRepository<LeaveAllocation> _leaveAllocationRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(IGenericRepository<LeaveAllocation> leaveAllocationRepository, IMapper mapper)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            try
            {
                var validatedModel = await new UpdateLeaveAllocationDtoValidator(_leaveAllocationRepository).ValidateAsync(request.UpdateLeaveAllocationDto, cancellationToken);

                if (!validatedModel.IsValid)
                {
                    response.Success = false;
                    response.Message = "Update Failed";
                    response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var leaveAllocation = _mapper.Map<LeaveAllocation>(request.UpdateLeaveAllocationDto);
                await _leaveAllocationRepository.Update(leaveAllocation);

                response.Success = true;
                response.Message = "Updated Successfully";
                response.Id = leaveAllocation.Id;
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
