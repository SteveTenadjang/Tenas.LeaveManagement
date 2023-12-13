using FluentValidation;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class CreateLeaveAllocationDtoValidator : AbstractValidator<CreateLeaveAllocationDto>
    {
        private readonly IGenericRepository<Domain.LeaveAllocation> _leaveAllocationRepository;

        public CreateLeaveAllocationDtoValidator(IGenericRepository<Domain.LeaveAllocation> leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            Include(new ILeaveAllocationDtoValidator(leaveAllocationRepository));
        }
    }
}
