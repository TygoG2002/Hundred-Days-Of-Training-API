using Application.Sets.GetSets;
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
    }
}
