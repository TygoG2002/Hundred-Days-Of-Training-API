using Application.Days.Interfaces;
using Application.Plans.GetPlansWithProgress;
using Application.Plans.Interfaces;
using MediatR;

namespace Application.Plans.GetPlansOverview;

public class GetPlansOverviewHandler : IRequestHandler<GetPlansOverviewQuery, List<PlanOverviewDto>>
{
    private readonly IPlanQueryRepository _plans;
    private readonly IDayQueryRepository _days;

    public GetPlansOverviewHandler(IPlanQueryRepository plans, IDayQueryRepository days, IDayQueryRepository progress)
    {
        _plans = plans;
        _days = days;
    }

    public async Task<List<PlanOverviewDto>> Handle(GetPlansOverviewQuery request, CancellationToken cancellationToken)
    {
        var plans = await _plans.GetAllAsync();
        var result = new List<PlanOverviewDto>();

        foreach (var plan in plans)
        {
            var days = await _days.GetDaysAsync(plan.Id);


            int completed = 0; 
            foreach (var day in days)
            {
                if (day.IsCompleted)
                    completed++;
            }
            
            result.Add(new PlanOverviewDto(
                plan.Id,
                plan.Name,
                completed,
                days.Count
            ));
        }

        return result;
    }
}
