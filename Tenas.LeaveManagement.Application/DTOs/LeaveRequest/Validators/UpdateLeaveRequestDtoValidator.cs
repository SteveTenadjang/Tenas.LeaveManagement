using FluentValidation;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class UpdateLeaveRequestDtoValidator : AbstractValidator<UpdateLeaveRequestDto>
    {
        private readonly IGenericRepository<Domain.LeaveRequest> _leaveRequestRepository;

        public UpdateLeaveRequestDtoValidator(IGenericRepository<Domain.LeaveRequest> leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            Include(new ILeaveRequetDtoValidator(_leaveRequestRepository));

            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.");
        }
    }
}
