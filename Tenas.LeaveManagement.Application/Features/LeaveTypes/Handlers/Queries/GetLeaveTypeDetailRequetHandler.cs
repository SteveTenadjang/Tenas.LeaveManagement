using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeDetailRequetHandler : IRequestHandler<GetLeaveTypeDetailRequest, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetLeaveTypeDetailRequetHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(GetLeaveTypeDetailRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveType = await _unitOfWork.GenericRepository<LeaveType>().GetById(request.Id);
                response.Success = true;
                response.Data = _mapper.Map<LeaveTypeDto>(leaveType);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }
}
