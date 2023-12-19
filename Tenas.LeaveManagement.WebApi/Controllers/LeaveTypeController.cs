using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Tenas.LeaveManagement.Application.DTOs.LeaveType;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Queries;
using Tenas.LeaveManagement.Application.Features.LeaveTypes.Requests.Commands;

namespace Tenas.LeaveManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveTypeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveTypeController(IMediator mediator)
            => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<LeaveTypeDto>>> Get()
            => Ok(await _mediator.Send(new GetLeaveTypeListRequest()));

        [HttpGet("GetById")]
        public async Task<ActionResult<LeaveTypeDto>> Get(Guid Id)
            => Ok(await _mediator.Send(new GetLeaveTypeDetailRequest { Id = Id }));
        
        [HttpPost]
        public async Task<ActionResult> Post(CreateLeaveTypeDto createLeaveTypeDto)
        {
            var response = await _mediator.Send(new CreateLeaveTypeCommand { CreateLeaveTypeDto = createLeaveTypeDto });
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put(LeaveTypeDto leaveType)
        {
            var response = await _mediator.Send(new UpdateLeaveTypeCommand { LeaveTypeDto = leaveType });
            return Ok(response);
        }

        [HttpDelete]
        [Authorize]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var response = await _mediator.Send(new DeleteLeaveTypeCommand { Id = Id });
            return Ok(response);
        }
    }
}
    