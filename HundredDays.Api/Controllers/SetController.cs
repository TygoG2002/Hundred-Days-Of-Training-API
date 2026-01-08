using Application.Sets.GetSets;
using Application.Sets.UpdateSet;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HundredDays.Api.Controllers
{

    [ApiController]
    [Route("api/sets")]
    public class SetController : ControllerBase
    {

        private readonly IMediator _mediator;

        public SetController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        // GET /api/sets/{planId}/days/{day}/sets
        [HttpGet("{planId}/days/{day}/sets")]
        public async Task<IActionResult> GetSets(int planId, int day)
        {
            GetSetsQuery query = new GetSetsQuery(planId, day);
            var result = await _mediator.Send(query);
            return Ok(result);
        }

        // GET /api/sets/sets/{setId}
        [HttpPost("sets/{setId}")]
        public async Task<IActionResult> UpdateSet(int setId, [FromBody] bool completed)
        {
            await _mediator.Send(new UpdateSetCommand(setId, completed));
            return NoContent();
        }

    }
}
