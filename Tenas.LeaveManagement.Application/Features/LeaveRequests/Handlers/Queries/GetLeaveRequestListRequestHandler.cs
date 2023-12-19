using MediatR;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Tenas.LeaveManagement.Domain;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Application.Constants;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Contracts.Identity;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;

namespace Tenas.LeaveManagement.Application.Features.LeaveRequests.Handlers.Queries
{
    public class GetLeaveRequestListRequestHandler : IRequestHandler<GetLeaveRequestListRequest, BaseCommandResponse<List<LeaveRequestListDto>>>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveRequestListRequestHandler(
            IHttpContextAccessor httpContextAccessor,
            IUserService userService,
            IUnitOfWork unitOfWork, 
            IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _userService = userService;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseCommandResponse<List<LeaveRequestListDto>>> Handle(GetLeaveRequestListRequest request, CancellationToken cancellationToken)
        {
            var leaveRequests = new List<LeaveRequest>();
            var requests = new List<LeaveRequestListDto>();

            if (request.IsLoggedInUser)
            {
                var userId = _httpContextAccessor.HttpContext.User.FindFirst(u => u.Type == CustomClaimTypes.Uid)?.Value;
                leaveRequests = (List<LeaveRequest>) await _unitOfWork.GenericRepository<LeaveRequest>().Find(x => x.EmployeeId == new Guid(userId));

                var employee = await _userService.GetEmployee(new Guid(userId));
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);

                foreach(var req in requests)
                    req.Employee = employee;
            }
            else
            {
                leaveRequests = (List<LeaveRequest>) await _unitOfWork.GenericRepository<LeaveRequest>().GetAll();
                requests = _mapper.Map<List<LeaveRequestListDto>>(leaveRequests);
                    
                foreach(var req in requests)
                    req.Employee = await _userService.GetEmployee(req.EmployeeId);
            }

            return new BaseCommandResponse<List<LeaveRequestListDto>>
            {
                IsSuccess = true,
                Data = requests
            };
        }
    }
}
