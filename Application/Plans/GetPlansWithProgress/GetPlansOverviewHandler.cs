using Application.Plans.GetPlansOverview;
using Application.Plans.GetPlansWithProgress;
using MediatR;

public class GetPlansOverviewHandler: IRequestHandler<GetPlansOverviewQuery, List<PlanOverviewDto>>
{
    public Task<List<PlanOverviewDto>> Handle(GetPlansOverviewQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}