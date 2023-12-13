using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    internal class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, BaseQueryResponse>
    {
        private readonly IGenericRepository<LeaveRequest> _leaveRequetRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestListRequestHandler(IGenericRepository<LeaveRequest> leaveRequetRepository, IMapper mapper)
        {
            _leaveRequetRepository = leaveRequetRepository;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveRequests = await _leaveRequetRepository.GetAll();
                response.Success = true;
                response.Data = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
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
