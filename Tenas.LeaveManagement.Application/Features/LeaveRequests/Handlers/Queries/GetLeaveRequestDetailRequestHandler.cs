using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Contracts.Identity;
using Tenas.LeaveManagement.Domain;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    internal class GetLeaveRequestDetailRequestHandler : IRequestHandler<GetLeaveRequestDetailRequest, BaseQueryResponse>
    {
        //private readonly IGenericRepository<Domain.LeaveRequest> _leaveRequetRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;

        public GetLeaveRequestDetailRequestHandler(IUnitOfWork unitOfWork, IUserService userService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _userService = userService;
            _mapper = mapper;
        }

        public async Task<BaseQueryResponse> Handle(GetLeaveRequestDetailRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveRequest = _mapper.Map<LeaveRequestDto>(await _unitOfWork.GenericRepository<LeaveRequest>().GetById(request.Id));
                leaveRequest.Employee = await _userService.GetEmployee(leaveRequest.EmployeeId);

                response.Success = true;
                response.Data = leaveRequest;
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
