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
            => _mediator = mediator;

        [HttpGet]
        public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
            => Ok(await _mediator.Send(new GetLeaveAllocationListRequest() { IsLoggedInUser = isLoggedInUser }));

        [HttpGet("GetById")]
        public async Task<ActionResult<LeaveAllocationDto>> GetById(Guid Id)
            => Ok(await _mediator.Send(new GetLeaveAllocationDetailRequest { Id = Id }));

        [HttpPost]
        public async Task<ActionResult> Post(CreateLeaveAllocationDto createLeaveAllocationDto)
        {
            var response = await _mediator.Send(new CreateLeaveAllocationCommand { CreateLeaveAllocationDto = createLeaveAllocationDto });
            return Ok(response);
        }

        [HttpPut]
        public async Task<ActionResult> Put(UpdateLeaveAllocationDto updateLeaveAllocationDto)
        {
            var response = await _mediator.Send(new UpdateLeaveAllocationCommand { UpdateLeaveAllocationDto = updateLeaveAllocationDto });
            return Ok(response);
        }

        [HttpDelete]
        public async Task<ActionResult> Delete(Guid Id)
        {
            var response = await _mediator.Send(new DeleteLeaveAllocationCommand { Id = Id });
            return Ok(response);
        }
    }
}
