using AutoMapper;
using MediatR;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;
using Tenas.LeaveManagement.Application.Contracts.Persistance;
using Tenas.LeaveManagement.Application.Reponses;
using Tenas.LeaveManagement.Domain;
using Microsoft.AspNetCore.Http;
using Tenas.LeaveManagement.Application.Contracts.Identity;
using Tenas.LeaveManagement.Application.Constants;

namespace Tenas.LeaveManagement.Application.Features.LeaveAllocations.Handlers.Queries
{
    public class GetLeaveAllocationListRequestHandler : IRequestHandler<GetLeaveAllocationListRequest, BaseQueryResponse>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IUserService _userService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetLeaveAllocationListRequestHandler(
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

        public async Task<BaseQueryResponse> Handle(GetLeaveAllocationListRequest request, CancellationToken cancellationToken)
        {
            BaseQueryResponse response = new();
            try
            {
                var leaveAllocations = new List<LeaveAllocation>();
                var allocations = new List<LeaveAllocationDto>();

                if(request.IsLoggedInUser)
                {
                    var userId = _httpContextAccessor.HttpContext.User.FindFirst(u => u.Type == CustomClaimTypes.Uid)?.Value;
                    leaveAllocations = (List<LeaveAllocation>) await _unitOfWork.GenericRepository<LeaveAllocation>().Find(x => x.EmployeeId == new Guid(userId));

                    var employee = await _userService.GetEmployee(new Guid(userId));
                    allocations = _mapper.Map<List<LeaveAllocationDto>>(leaveAllocations);

                    foreach (var alloc in allocations)
                        alloc.Employee = employee;
                }
                else
                {
                    leaveAllocations = (List<LeaveAllocation>) await _unitOfWork.GenericRepository<LeaveAllocation>().GetAll();
                    allocations = _mapper.Map<List<LeaveAllocationDto>>(allocations);

                    foreach (var alloc in allocations)
                        alloc.Employee = await _userService.GetEmployee(alloc.EmployeeId);
                }


                response.Success = true;
                response.Data = allocations;
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
