using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveType.Validators;
using Tenas.LeaveManagement.Application.Exceptions;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class UpdateLeaveTypeCommandHandler : IRequestHandler<UpdateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IGenericRepository<LeaveType> _leaveTypeRepository;
        private readonly IMapper _mapper;

        public UpdateLeaveTypeCommandHandler(IGenericRepository<LeaveType> leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            try
            {
                var validatedModel = await new UpdateLeaveTypeDtoValidator().ValidateAsync(request.LeaveTypeDto, cancellationToken);

                if (!validatedModel.IsValid)
                {
                    response.Success = false;
                    response.Message = "Update Failed";
                    response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var leaveType = _mapper.Map<LeaveType>(request.LeaveTypeDto);
                await _leaveTypeRepository.Update(leaveType);

                response.Success = true;
                response.Message = "Updated Successfully";
                response.Id = leaveType.Id;
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
