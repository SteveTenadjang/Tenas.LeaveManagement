using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Contracts.Infrastructure;
using Tenas.LeaveManagement.Application.Models.Mail;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Tenas.LeaveManagement.Application.Constants;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Commands
{
    public class CreateLeaveRequestCommandHandler : IRequestHandler<CreateLeaveRequestCommand, BaseCommandResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public CreateLeaveRequestCommandHandler(IHttpContextAccessor httpContextAccessor,IUnitOfWork unitOfWork,IEmailSender emailSender, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveRequestCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            try
            {
                var validatedModel = await new CreateLeaveRequestDtoValidator(_unitOfWork.GenericRepository<LeaveRequest>()).ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);
                //var validatedModel = await new CreateLeaveRequestDtoValidator(_leaveRequestRepository).ValidateAsync(request.CreateLeaveRequestDto, cancellationToken);
                var userId = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(q => q.Type == CustomClaimTypes.Uid)?.Value;

                //var allocation = await _leaveAllocationRepository.Find(x => x.EmployeeId == new Guid(userId) && x.LeaveTypeId == request.CreateLeaveRequestDto.LeaveTypeId);
                var allocations = await _unitOfWork.GenericRepository<LeaveAllocation>().Find(x => x.EmployeeId == new Guid(userId) && x.LeaveTypeId == request.CreateLeaveRequestDto.LeaveTypeId);

                if(allocations.FirstOrDefault() is null)
                    validatedModel.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        nameof(request.CreateLeaveRequestDto.LeaveTypeId), "You don't have any allocation for this leave type"
                    ));

                int daysRequested = (int) (request.CreateLeaveRequestDto.EndDate - request.CreateLeaveRequestDto.StartDate).TotalDays;

                if (daysRequested > allocations?.FirstOrDefault()?.NumberOfDays)
                    validatedModel.Errors.Add(new FluentValidation.Results.ValidationFailure(
                        nameof(request.CreateLeaveRequestDto.EndDate), "You don't have enough days for this request"    
                    ));

                
                if (!validatedModel.IsValid)
                {
                    response.Success = false;
                    response.Message = "Creation Failed";
                    response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                    return response;
                }

                var leaveRequest = _mapper.Map<LeaveRequest>(request.CreateLeaveRequestDto);
                leaveRequest.EmployeeId = new Guid(userId);
                //leaveRequest = await _leaveRequestRepository.Add(leaveRequest);
                leaveRequest = await _unitOfWork.GenericRepository<LeaveRequest>().Add(leaveRequest);
                await _unitOfWork.Save();

                response.Success = true;
                response.Message = "Creation Successful";
                response.Id = leaveRequest.Id;

                var userEmail = _httpContextAccessor?.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
                var email = new Email
                {
                    To = userEmail,
                    Body = $"Your leave request for {request.CreateLeaveRequestDto.StartDate:D} to {request.CreateLeaveRequestDto.EndDate:D}" +
                        $"has been submitted successfully.",
                    Subject = "Leave Request Submitted"
                };

                await _emailSender.SendEmail(email);
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
