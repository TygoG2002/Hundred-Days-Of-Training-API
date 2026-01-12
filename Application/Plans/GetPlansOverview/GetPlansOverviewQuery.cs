using Application.Plans.GetPlansWithProgress;
using MediatR;

namespace Application.Plans.GetPlansOverview;

public record GetPlansOverviewQuery
    : IRequest<List<PlanOverviewDto>>;
