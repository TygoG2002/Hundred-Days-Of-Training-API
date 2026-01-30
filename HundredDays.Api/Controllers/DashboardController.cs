using Application.Dashboard.GetTodaysWorkouts;
using Application.Dashboard.GetWeekPlanningWorkouts;
using Application.Dashboard.GetWeekProgress;
using Application.Dashboard.UpdateWeekPlanning;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/dashboard")]
public class DashboardController : ControllerBase
{
    private readonly IMediator _mediator;

    public DashboardController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("today")]
    public async Task<IActionResult> GetTodayWorkouts()
    {
        var result = await _mediator.Send(new GetTodayWorkoutsQuery());
        return Ok(result);
    }


    [HttpGet("week")]
    public async Task<IActionResult> GetWeekPlanning()
    {
        var result = await _mediator.Send(new GetWeekPlanningQuery());
        return Ok(result);
    }

    [HttpPut("updatePlanning")]
    public async Task<IActionResult> UpdateWeekPlanning([FromBody] UpdateWeekPlanningDto request)
    {
        await _mediator.Send(new UpdateWeekPlanningCommand(request));
        return NoContent();
    }

    [HttpGet("progress")]
    public async Task<IActionResult> GetWeekProgress()
    {
        var result = await _mediator.Send(new GetWeekProgressQuery());
        return Ok(result);
    }

}
