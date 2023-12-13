using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    internal class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, BaseQueryResponse>
    {
        private readonly IGenericRepository<Domain.LeaveRequest> _leaveRequetRepository;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailRequestHandler(IGenericRepository<Domain.LeaveRequest> leaveRequetRepository, IMapper mapper)
        {
            _leaveRequetRepository = leaveRequetRepository;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveRequest = await _leaveRequetRepository.GetById(request.Id);
                response.Success = true;
                response.Data = _mapper.Map<LeaveRequestDto>(leaveRequest);
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
