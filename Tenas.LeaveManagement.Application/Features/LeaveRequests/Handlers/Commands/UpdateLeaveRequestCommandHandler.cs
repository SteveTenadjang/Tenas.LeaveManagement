using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Exceptions;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class UpdateLeaveRequestCommandHandler : IRequestHandler<UpdateLeaveRequestCommand, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            if (request.UpdateLeaveRequestDto is not null)
            {
                var leaveRequest = await _unitOfWork.GenericRepository<LeaveRequest>().GetById(request.UpdateLeaveRequestDto.Id);
                var validatedModel = await new UpdateLeaveRequestDtoValidator(_unitOfWork.GenericRepository<LeaveRequest>()).ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);

                if (!validatedModel.IsValid)
                    throw new ValidationException(validatedModel);

                _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                await _unitOfWork.GenericRepository<LeaveRequest>().Update(leaveRequest);
                await _unitOfWork.Save();

                return new BaseQueryResponse
                {
                    Message = "Updated Successfully",
                    Data = request.UpdateLeaveRequestDto
                };
            }
            else if(request.ChangeLeaveRequestApprovalDto is not null)
            {
                var leaveRequest = await _unitOfWork.GenericRepository<LeaveRequest>().GetById(request.ChangeLeaveRequestApprovalDto.Id);
                if (request.ChangeLeaveRequestApprovalDto.Approved)
                {
                    var allocations = await _unitOfWork.GenericRepository<LeaveAllocation>().Find(x => x.Id == leaveRequest.Id && x.EmployeeId == leaveRequest.EmployeeId);
                    var allocation = allocations.FirstOrDefault();
                    int daysRequested = (int)(leaveRequest.StartDate - leaveRequest.EndDate).TotalDays;

                    allocation.NumberOfDays = daysRequested;
                    await _unitOfWork.GenericRepository<LeaveAllocation>().Update(allocation);

                    return new BaseQueryResponse
                    {
                        Message = "Approval changed Successfully",
                        Data = request.ChangeLeaveRequestApprovalDto
                    };
                }
                await _unitOfWork.Save();
            }

            throw new BadRequestException("An error occured");
        }
    }
}
