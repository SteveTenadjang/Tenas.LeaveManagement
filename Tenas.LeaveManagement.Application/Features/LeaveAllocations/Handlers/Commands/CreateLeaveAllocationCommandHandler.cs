using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Contracts.Identity;
using Tenas.LeaveManagement.Application.Exceptions;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseQueryResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;

        public CreateLeaveAllocationCommandHandler(IUserService userService, IUnitOfWork unitOfWork)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseQueryResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            var validatedModel = await new CreateLeaveAllocationDtoValidator(_unitOfWork.GenericRepository<LeaveAllocation>()).ValidateAsync(request.CreateLeaveAllocationDto, cancellationToken);

            if (!validatedModel.IsValid)
                throw new ValidationException(validatedModel);

            var leaveType = await _unitOfWork.GenericRepository<LeaveType>().GetById(request.CreateLeaveAllocationDto.LeaveTypeId);
            var employees = await _userService.GetEmployees();
            var period = DateTime.UtcNow.Year;
            var allocations = new List<LeaveAllocation>();

            foreach (var employee in employees) 
            {
                if (await _unitOfWork.GenericRepository<LeaveAllocation>()
                    .Exists(e => e.Id == employee.Id
                        && e.LeaveTypeId == leaveType.Id
                        && e.Period == period)
                    )
                    continue;

                allocations.Add(new LeaveAllocation
                {
                    EmployeeId = employee.Id,
                    LeaveTypeId = leaveType.Id,
                    LeaveType = leaveType,
                    NumberOfDays = leaveType.DefaultDays,
                    Period = period,
                });
            }

            await _unitOfWork.GenericRepository<LeaveAllocation>().AddRange(allocations);
            await _unitOfWork.Save();

            return new BaseQueryResponse { Message = "Allocation Successful"};
        }
    }
}
