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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateLeaveRequestCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(UpdateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            try
            {


                if (request.UpdateLeaveRequestDto is not null)
                {
                    var leaveRequest = await _unitOfWork.GenericRepository<LeaveRequest>().GetById(request.UpdateLeaveRequestDto.Id);
                    //var leaveRequest = await _leaveRequestRepository.GetById(request.UpdateLeaveRequestDto.Id);

                    var validatedModel = await new UpdateLeaveRequestDtoValidator(_unitOfWork.GenericRepository<LeaveRequest>()).ValidateAsync(request.UpdateLeaveRequestDto, cancellationToken);

                    if (!validatedModel.IsValid)
                    {
                        response.Success = false;
                        response.Message = "Update Failed";
                        response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                        return response;
                    }
                    _mapper.Map(request.UpdateLeaveRequestDto, leaveRequest);
                    await _unitOfWork.GenericRepository<LeaveRequest>().Update(leaveRequest);
                    await _unitOfWork.Save();

                    response.Success = true;
                    response.Message = "Updated Successfully";
                    response.Id = leaveRequest.Id;

                }
                else if(request.ChangeLeaveRequestApprovalDto is not null)
                {
                    var leaveRequest = await _unitOfWork.GenericRepository<LeaveRequest>().GetById(request.ChangeLeaveRequestApprovalDto.Id);
                    if (request.ChangeLeaveRequestApprovalDto.Approved)
                    {
                        var allocations = await _unitOfWork.GenericRepository<LeaveAllocation>().Find(x => x.Id == leaveRequest.Id && x.EmployeeId == leaveRequest.EmployeeId);
                        //var allocations = await _leaveAllocationRepository.Find(x => x.Id == leaveRequest.Id && x.EmployeeId == leaveRequest.EmployeeId);
                        var allocation = allocations.FirstOrDefault();
                        int daysRequested = (int)(leaveRequest.StartDate - leaveRequest.EndDate).TotalDays;

                        allocation.NumberOfDays = daysRequested;
                        await _unitOfWork.GenericRepository<LeaveAllocation>().Update(allocation);
                        //await _leaveAllocationRepository.Update(allocation);

                        response.Success = true;
                        response.Message = "Approval changed Successfully";
                        response.Id = leaveRequest.Id;
                    }
                    await _unitOfWork.Save();
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
