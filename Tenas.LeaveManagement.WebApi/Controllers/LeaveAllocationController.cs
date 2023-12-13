using MediatR;
using Microsoft.AspNetCore.Mvc;
using Tenas.LeaveManagement.Application.DTOs.LeaveAllocation;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Commands;
using Tenas.LeaveManagement.Application.Features.LeaveAllocations.Requests.Queries;

namespace Tenas.LeaveManagement.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaveAllocationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public LeaveAllocationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get()
            => Ok(await _mediator.Send(new GetLeaveAllocationListRequest()));

        [HttpGet("{id:Guid}")]
        public async Task<ActionResult<List<LeaveAllocationDto>>> GetById(Guid Id)
            => Ok(await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = Id }));

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationDto createLeaveAllocationDto)
        {
            var response = await _mediator.Send(new CreateLeaveAllocationCommand { CreateLeaveAllocationDto = createLeaveAllocationDto });
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationDto updateLeaveAllocationDto)
        {
            await _mediator.Send(new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = updateLeaveAllocationDto });
            return NoContent();
        }

        [HttpDelete("{id:Guid}")]
        public async Task<ActionResult> Delete(Guid Id)
        {
            await _mediator.Send(new DeleteLeaveAllocationCommand { Id = Id });
            return NoContent();
        }
    }
}
