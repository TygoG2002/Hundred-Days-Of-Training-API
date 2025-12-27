using Application.Plans.GetDays;
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


    // GET /api/plans/{planId}/days
    [HttpGet("{planId}/days")]
    public async Task<IActionResult> GetDays(int planId)
    {
        var result = await _mediator.Send(new GetDaysQuery(planId));
        return Ok(result);
    }
}
