using Application.Habbit.CompleteHabit;
using Application.Habits.GetHabits;
using Application.Habits.UpdateValue;
using HundredDays.Api.Contracts.Habits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/habits")]
public class HabitController : ControllerBase
{
    private readonly IMediator _mediator;

    public HabitController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<ActionResult<List<HabitDto>>> GetAllHabits()
    {
        var result = await _mediator.Send(new GetHabitsQuery());
        return Ok(result);
    }

    [HttpPost("{habitId}/add-value")]
    public async Task<IActionResult> AddValue(
        int habitId,
        [FromBody] UpdateHabitValueRequest request)
    {
        await _mediator.Send(
            new UpdateHabitValueCommand(habitId, request.Amount)
        );

        return NoContent();
    }

    [HttpPost("{habitId}/complete")]
    public async Task<IActionResult> Complete(int habitId)
    {
        await _mediator.Send(
            new CompleteHabitCommand(habitId)
        );

        return NoContent();
    }
}
