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
           
            RuleFor(x => x.LeaveTypeId)
            .NotEmpty()
            .MustAsync(async (id, token) => await _leaveAllocationRepository.Exists(id))
            .WithMessage("{PropertyName} doesn't existe.");
        }
    }
}
