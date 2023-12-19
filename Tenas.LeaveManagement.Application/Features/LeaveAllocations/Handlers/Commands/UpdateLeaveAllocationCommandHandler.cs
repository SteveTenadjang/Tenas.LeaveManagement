using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Exceptions;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class UpdateLeaveAllocationCommandHandler : IRequestHandler<UpdateLeaveAllocationCommand, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveAllocationCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(UpdateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validatedModel = await new UpdateLeaveAllocationDtoValidator(_unitOfWork.GenericRepository<LeaveAllocation>()).ValidateAsync(request.UpdateLeaveAllocationDto, cancellationToken);

            if (!validatedModel.IsValid)
                throw new ValidationException(validatedModel);

            var leaveAllocation = _mapper.Map<LeaveAllocation>(request.UpdateLeaveAllocationDto);
            await _unitOfWork.GenericRepository<LeaveAllocation>().Update(leaveAllocation);
            await _unitOfWork.Save();

            return new BaseQueryResponse
            {
                Message = "Updated Successful",
                Data = request.UpdateLeaveAllocationDto
            };
        }
    }
}
