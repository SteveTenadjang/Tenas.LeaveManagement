using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Contracts.Identity;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Commands
{
    public class CreateLeaveAllocationCommandHandler : IRequestHandler<CreateLeaveAllocationCommand, BaseCommandResponse>
    {
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateLeaveAllocationCommandHandler(IUserService userService, IUnitOfWork unitOfWork,IMapper mapper)
        {
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse> Handle(CreateLeaveAllocationCommand request, CancellationToken cancellationToken)
        {
            BaseCommandResponse response = new();
            var validatedModel = await new CreateLeaveAllocationDtoValidator(_unitOfWork.GenericRepository<LeaveAllocation>()).ValidateAsync(request.CreateLeaveAllocationDto, cancellationToken);

            if (!validatedModel.IsValid)
            {
                response.Success = false;
                response.Message = "Creation Failed";
                response.Errors = validatedModel.Errors.Select(e => e.ErrorMessage).ToList();
                return response;
            }

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
            //var leaveAllocation = _mapper.Map<LeaveAllocation>(request.CreateLeaveAllocationDto);
            //leaveAllocation = await _leaveAllocationRepository.Add(leaveAllocation);
                
            response.Success = true;
            response.Message = "Allocation Successfully";
            response.Id = Guid.NewGuid();
            return response;
        }
    }
}
