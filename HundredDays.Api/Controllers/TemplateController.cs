using Application.Template.GetTemplates;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HundredDays.Api.Controllers
{
    [Route("api/templates")]
    [ApiController]
    public class TemplateController : ControllerBase
    {

        private readonly IMediator _mediator;

        public TemplateController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetTemplates()
        {
            GetTemplatesQuery query = new GetTemplatesQuery();
            var result = await _mediator.Send(query);
            return Ok(result); 
        }
    }
}
