using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tenas.LeaveManagement.Application.DTOs.LeaveRequest;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Commands;
using Tenas.LeaveManagement.Application.Features.LeaveRequests.Requests.Queries;
using Tenas.LeaveManagement.Application.Reponses;

namespace Tenas.LeaveManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveRequestController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveRequestController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveRequestListDto>>> Get(bool isLoggedInUser = false)
            => Ok(await _mediator.Send(new GetLeaveRequestListRequest() { IsLoggedInUser = isLoggedInUser }));

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<List<LeaveRequestDto>>> GetById(Guid Id)
            => Ok(await _mediator.Send(new GetLeaveRequestDetailRequest { Id = Id }));

        [HttpPost]
        public async Task<ActionResult<BaseCommandResponse>> Post([FromBody] CreateLeaveRequestDto createLeaveRequestDto)
        {
            var response = await _mediator.Send(new CreateLeaveRequestCommand { CreateLeaveRequestDto = createLeaveRequestDto });
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult<BaseCommandResponse>> Put([FromBody] UpdateLeaveRequestDto updateLeave)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand { UpdateLeaveRequestDto = updateLeave });
            return NoContent();
        }

        [HttpPut("changeapproval")]
        public async Task<ActionResult> ChangeApproval([FromBody] ChangeLeaveRequestApprovalDto changeLeaveRequest)
        {
            await _mediator.Send(new UpdateLeaveRequestCommand { ChangeLeaveRequestApprovalDto = changeLeaveRequest });
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            await _mediator.Send(new DeleteLeaveRequestCommand { Id = Id });
            return NoContent();
        }
    }
}
