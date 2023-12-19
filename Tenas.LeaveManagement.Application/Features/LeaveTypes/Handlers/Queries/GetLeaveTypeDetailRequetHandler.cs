using MediatR;
using AutoMapper;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequetHandler : IRequestHandler<GetLeaveTypeDetailRequest, BaseCommandResponse<LeaveTypeDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetLeaveTypeDetailRequetHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse<LeaveTypeDto>> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            var leaveType = await _unitOfWork.GenericRepository<LeaveType>().GetById(request.Id);
            return new BaseCommandResponse<LeaveTypeDto>
            {
                IsSuccess = true,
                Data = _mapper.Map<LeaveTypeDto>(leaveType)
            };
        }
    }
}
