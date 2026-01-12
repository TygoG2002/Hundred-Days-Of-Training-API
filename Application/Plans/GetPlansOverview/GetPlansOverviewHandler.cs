using Application.Plans.GetPlansOverview;
using Application.Plans.GetPlansWithProgress;
using Application.Plans.Interfaces;
using MediatR;

public class GetPlansOverviewHandler
    : IRequestHandler<GetPlansOverviewQuery, List<PlanOverviewDto>>
{
    private readonly IPlanQueryRepository _plans;

    public GetPlansOverviewHandler(IPlanQueryRepository plans)
    {
        _plans = plans;
    }

    public async Task<List<PlanOverviewDto>> Handle(
        GetPlansOverviewQuery request,
        CancellationToken cancellationToken)
    {
        return await _plans.GetPlansOverviewAsync();
    }
}
