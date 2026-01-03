using Application.Days.GetDayProgress;
using Application.Days.GetDays;
using Application.Plans.GetPlansOverview;
using Application.Sets.GetSets;
using Application.Sets.UpdateSet;
using HundredDays.Application.Plans.GetPlans;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HundredDays.Api.Controllers;

[ApiController]
[Route("api/plans")]
public class PlansController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlansController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    //GET to api/plans
    public async Task<IActionResult> GetPlans()
    {
        GetPlansQuery query = new GetPlansQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }


    [HttpGet("overview")]
    public async Task<IActionResult> GetOverview()
    {
        var result = await _mediator.Send(new GetPlansOverviewQuery());
        return Ok(result);
    }




    // GET /api/plans/{planId}/days
    [HttpGet("{planId}/days")]
    public async Task<IActionResult> GetDays(int planId)
    {
        var result = await _mediator.Send(new GetDaysQuery(planId));
        return Ok(result);
    }


    // GET /api/plans/{planId}/days/{day}/sets
    [HttpGet("{planId}/days/{day}/sets")]
    public async Task<IActionResult> GetSets(int planId, int day)
    {
        GetSetsQuery query = new GetSetsQuery(planId, day);
        var result = await _mediator.Send(query);
        return Ok(result);

    }


    [HttpPost("sets/{setId}")]
    public async Task<IActionResult> UpdateSet(int setId, [FromBody] bool completed)
    {
        await _mediator.Send(new UpdateSetCommand(setId, completed));
        return NoContent();
    }



    [HttpGet("{planId}/days/{day}/progress")]
    public async Task<IActionResult> GetDayProgress(int planId, int day)
    {
        var result = await _mediator.Send(
            new GetDayProgressQuery(planId, day));

        return Ok(result);
    }

}
