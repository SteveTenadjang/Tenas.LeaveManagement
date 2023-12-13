using FluentValidation;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class UpdateLeaveAllocationDtoValidator : AbstractValidator<UpdateLeaveAllocationDto>
    {
        private readonly IGenericRepository<Domain.LeaveAllocation> _leaveAllocationRepository;

        public UpdateLeaveAllocationDtoValidator(IGenericRepository<Domain.LeaveAllocation> leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;
            Include(new ILeaveAllocationDtoValidator(leaveAllocationRepository));

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
