using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Domain;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationDetailRequestHandler : IRequestHandler<GetLeaveAllocationDetailRequest, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveAllocationDetailRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(GetLeaveAllocationDetailRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveAllocation = await _unitOfWork.GenericRepository<LeaveAllocation>().GetById(request.Id);
                response.Success = true;
                response.Data = _mapper.Map<LeaveAllocationDto>(leaveAllocation);
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
