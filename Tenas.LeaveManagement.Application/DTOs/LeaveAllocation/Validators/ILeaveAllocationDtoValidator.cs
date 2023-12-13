using FluentValidation;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveAllocation.Validators
{
    public class ILeaveAllocationDtoValidator : AbstractValidator<ILeaveAllocationDto>
    {
        private readonly IGenericRepository<Domain.LeaveAllocation> _leaveAllocationRepository;

        public ILeaveAllocationDtoValidator(IGenericRepository<Domain.LeaveAllocation> leaveAllocationRepository)
        {
            _leaveAllocationRepository = leaveAllocationRepository;

            //ToDo Add custom massages
            RuleFor(x => x.NumberOfDays)
                .NotEmpty()
                .LessThanOrEqualTo(0)
                .GreaterThanOrEqualTo(100);

            RuleFor(x => x.Period)
                .NotEmpty()
                .GreaterThanOrEqualTo(DateTime.Now.Year);

            RuleFor(x => x.LeaveTypeId)
                .NotEmpty();

            RuleFor(x => x.LeaveTypeId)
                .NotEmpty()
                .MustAsync(async (id, token) => !await _leaveAllocationRepository.Exists(id))
                .WithMessage("{PropertyName} doesn't existe.");
        }
    }
}
