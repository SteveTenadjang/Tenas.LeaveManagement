using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveType.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        private readonly IGenericRepository<LeaveType> _leaveTypeRepository;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IGenericRepository<LeaveType> leaveTypeRepository, IMapper mapper)
        {
            _leaveTypeRepository = leaveTypeRepository;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();

            try
            {
                var validatedModel = await new CreateLeaveTypeDtoValidator().ValidateAsync(request.CreateLeaveTypeDto, cancellationToken);

                if (!validatedModel.IsValid)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
                leaveType = await _leaveTypeRepository.Add(leaveType);

                response.Success = true;
                response.Message = "Creation Successful";
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
