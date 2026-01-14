using MediatR;
using Microsoft.AspNetCore.Mvc;
using Application.WorkoutSession.StartWorkoutSession;

namespace HundredDays.Api.Controllers
{
    [ApiController]
    [Route("api/sessions")]
    public class SessionController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SessionController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("start")]
        public async Task<IActionResult> Start([FromBody] int workoutTemplateId)
        {
            var command = new StartWorkoutSessionCommand(workoutTemplateId);
            var result = await _mediator.Send(command);
            return Ok(result);
        }
    }
}
