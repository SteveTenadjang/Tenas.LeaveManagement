using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveType.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Exceptions;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Commands
{
    public class CreateLeaveTypeCommandHandler : IRequestHandler<CreateLeaveTypeCommand, BaseCommandResponse>
    {
        //private readonly IGenericRepository<LeaveType> _leaveTypeRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveTypeCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveTypeCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            var validatedModel = await new CreateLeaveTypeDtoValidator().ValidateAsync(request.CreateLeaveTypeDto, cancellationToken);

            if (!validatedModel.IsValid)
                throw new ValidationException(validatedModel);
            var leaveType = _mapper.Map<LeaveType>(request.CreateLeaveTypeDto);
            leaveType = await _unitOfWork.GenericRepository<LeaveType>().Add(leaveType);
            await _unitOfWork.Save();

            response.Success = true;
            response.Message = "Creation Successful";
            response.Id = leaveType.Id;
            return response;
        }
    }
}
