using Application.Habbit.GetHabbits;
using Application.Habits.GetHabits;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HundredDays.Api.Controllers
{
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
    }
}
