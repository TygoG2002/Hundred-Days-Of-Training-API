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
public class PlanController : ControllerBase
{
    private readonly IMediator _mediator;

    public PlanController(IMediator mediator)
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
    public async Task<IActionResult> GetPlansOverview()
    {
        var result = await _mediator.Send(new GetPlansOverviewQuery());
        return Ok(result);
    }

}
