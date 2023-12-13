using FluentValidation;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.DTOs.LeaveRequest.Validators
{
    public class CreateLeaveRequestDtoValidator : AbstractValidator<CreateLeaveRequestDto>
    {
        private readonly IGenericRepository<Domain.LeaveRequest> _leaveRequestRepository;

        public CreateLeaveRequestDtoValidator(IGenericRepository<Domain.LeaveRequest> leaveRequestRepository)
        {
            _leaveRequestRepository = leaveRequestRepository;
            Include(new ILeaveRequetDtoValidator(_leaveRequestRepository));
        }
    }
}
