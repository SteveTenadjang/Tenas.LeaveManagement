using FluentValidation;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class ILeaveRequetDtoValidator : AbstractValidator<ILeaveRequestDto>
    {
        private readonly IGenericRepository<Domain.LeaveRequest> _leaveRequestRepository;

        public ILeaveRequetDtoValidator(IGenericRepository<Domain.LeaveRequest> leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;

            RuleFor(x => x.StartDate)
                .LessThan(x => x.EndDate).WithMessage("{PropertyName} must be before {ComparisonValue}");

            RuleFor(x => x.EndDate)
                .GreaterThan(x => x.StartDate).WithMessage("{PropertyName} must be after {ComparisonValue}");

            RuleFor(x => x.LeaveTypeId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MustAsync(async (id, token) => await _leaveRequestRepository.Exists(id))
                .WithMessage("{PropertyName} doesn't existe.");

            //RuleFor(x => x.LeaveTypeId)
            //    .NotEmpty().WithMessage("{PropertyName} is required.")
            //    .MustAsync(async (id, token) =>
            //    {
            //        var leaveType = await _leaveRequestRepository.Exists(id);
            //        return !leaveType;
            //    }).WithMessage("{PropertyName} doesn't existe.");

        }
    }
}
