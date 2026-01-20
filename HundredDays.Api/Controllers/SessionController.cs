using Application.WorkoutSession.DeleteWorkoutSession;
using Application.WorkoutSession.FinishWorkoutSession;
using Application.WorkoutSession.GetWorkoutInfo;
using Application.WorkoutSession.StartWorkoutSession;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

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


        [HttpGet("{sessionId}")]
        public async Task<IActionResult> Get(int sessionId)
        {
            var result = await _mediator.Send(
                new GetWorkoutInfoQuery(sessionId));

            return Ok(result);
        }

        [HttpPost("{sessionId}/finish")]
        public async Task<IActionResult> Finish([FromRoute] int sessionId, [FromBody] FinishWorkoutSessionRequest request)
        {
            if (sessionId != request.SessionId)
                return BadRequest("SessionId mismatch");

            await _mediator.Send(new FinishWorkoutSessionCommand(request.SessionId,request.Exercises));

            return NoContent();
        }

        [HttpDelete("Delete/{sessionId}")]
        public async Task<IActionResult> Delete([FromRoute] int sessionId)
        {
            var command = new DeleteWorkoutSessionCommand(sessionId);
            await _mediator.Send(command);
            return Ok();
        }

   
    }
}
