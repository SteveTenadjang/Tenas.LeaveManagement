using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.Application.Features.LeaveTypes.Handlers.Queries
{
    public class GetLeaveTypeListRequestHandler : IRequestHandler<GetLeaveTypeListRequest, BaseQueryResponse>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public GetLeaveTypeListRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(GetLeaveTypeListRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveTypes = await _unitOfWork.GenericRepository<LeaveType>().GetAll();
                response.Success = true;
                response.Data = _mapper.Map<List<LeaveTypeDto>>(leaveTypes);
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
