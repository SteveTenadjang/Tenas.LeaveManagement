using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;

namespace Tenas.LeaveManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
            => Ok(await _mediator.Send(new GetLeaveRequestListRequest() { IsLoggedInUser = isLoggedInUser }));

        [HttpGet("GetById")]
        public async Task<ActionResult<LeaveRequestDto>> GetById(Guid Id)
            => Ok(await _mediator.Send(new GetLeaveRequestDetailRequest { Id = Id }));

        [HttpPost]
        public async Task<ActionResult> Post(CreateLeaveRequestDto createLeaveRequestDto)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand { CreateLeaveRequestDto = createLeaveRequestDto });
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateLeaveRequestDto updateLeave)
        {
            var response = await _mediator.Send(new UpdateLeaveRequestCommand { UpdateLeaveRequestDto = updateLeave });
            return Ok(response);
        }

        [HttpPut("changeapproval")]
        public async Task<ActionResult> ChangeApproval(ChangeLeaveRequestApprovalDto changeLeaveRequest)
        {
            var response = await _mediator.Send(new UpdateLeaveRequestCommand { ChangeLeaveRequestApprovalDto = changeLeaveRequest });
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var response = await _mediator.Send(new DeleteLeaveRequestCommand { Id = Id });
            return Ok(response);
        }
    }
}
