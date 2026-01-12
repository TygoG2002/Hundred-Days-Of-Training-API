using Application.Days.CompleteDay;
using Application.Days.GetDayProgress;
using Application.Days.GetDays;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HundredDays.Api.Controllers
{
    [ApiController]
    [Route("api/days")]
    public class DayController : ControllerBase
    {
    
        private readonly IMediator _mediator;

        public DayController(IMediator mediator)
        {
            _mediator = mediator; 
        }

        // GET /api/days/{planId}/days
        [HttpGet("{planId}/days")]
        public async Task<IActionResult> GetDays(int planId)
        {
            var result = await _mediator.Send(new GetDaysQuery(planId));
            return Ok(result);
        }


        [HttpGet("{planId}/days/{day}/progress")]
        public async Task<IActionResult> GetDayProgress(int planId, int day)
        {
            var result = await _mediator.Send(
                new GetDayProgressQuery(planId, day));

            return Ok(result);
        }

        [HttpPost("Completed")]
        public async Task<IActionResult> CompleteDay([FromBody] CompleteDayRequest request )
        {
            var command = new CompleteDayCommand(request.planId, request.dayId, request.completed);
            await _mediator.Send(command);
            return Ok(); 
        }


    }
}
