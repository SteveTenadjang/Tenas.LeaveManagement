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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            try
            {
                var validatedModel = await new UpdateLeaveAllocationDtoValidator(_unitOfWork.GenericRepository<LeaveAllocation>()).ValidateAsync(request.UpdateLeaveAllocationDto, cancellationToken);

                if (!validatedModel.IsValid)
                {
                    response.Success = false;
                    response.Message = "Update Failed";
                    response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var leaveAllocation = _mapper.Map<LeaveAllocation>(request.UpdateLeaveAllocationDto);
                await _unitOfWork.GenericRepository<LeaveAllocation>().Update(leaveAllocation);
                await _unitOfWork.Save();

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
