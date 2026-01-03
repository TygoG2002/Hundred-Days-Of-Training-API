using Application.Days.Interfaces;
using Application.Plans.GetPlansWithProgress;
using Application.Plans.Interfaces;
using MediatR;

namespace Application.Plans.GetPlansOverview;

public class GetPlansOverviewHandler : IRequestHandler<GetPlansOverviewQuery, List<PlanOverviewDto>>
{
    private readonly IPlanQueryRepository _plans;
    private readonly IDayQueryRepository _days;
    private readonly IDayProgressRepository _progress;

    public GetPlansOverviewHandler(IPlanQueryRepository plans, IDayQueryRepository days, IDayProgressRepository progress)
    {
        _plans = plans;
        _days = days;
        _progress = progress;
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
                var (done, total) =
                    await _progress.GetDayProgressAsync(plan.Id, day);

                if (total > 0 && done == total)
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
