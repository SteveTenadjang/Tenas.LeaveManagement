using MediatR;
using AutoMapper;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, BaseCommandResponse<List<LeaveTypeDto>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetLeaveTypeListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse<List<LeaveTypeDto>>> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            var leaveTypes = await _unitOfWork.GenericRepository<LeaveType>().GetAll();
            return new BaseCommandResponse<List<LeaveTypeDto>>
            {
                IsSuccess = true,
                Data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes)
            };
        }
    }
}
