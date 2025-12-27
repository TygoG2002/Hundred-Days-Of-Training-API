using Microsoft.AspNetCore.Mvc;

namespace HundredDays.Api.Controllers;

[ApiController]
[Route("api/plans")]
public class PlansController : ControllerBase
{
    [HttpGet]
    public IActionResult GetPlans()
    {
        return Ok(new[]
        {
            new { Id = 1, Name = "Push-ups", TargetReps = 100 },
            new { Id = 2, Name = "test", TargetReps = 200 },
            new { Id = 3, Name = "test", TargetReps = 20 }
        });
    }

    // GET /api/plans/{planId}/days
    [HttpGet("{planId}/days")]
    public async Task<IActionResult> GetDays(int planId)
    {
        return Ok(new List<int>() { 1, 2 });
    }




}
