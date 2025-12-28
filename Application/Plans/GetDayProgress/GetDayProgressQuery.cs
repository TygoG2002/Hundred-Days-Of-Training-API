using MediatR;

namespace Application.Plans.GetDayProgress;

public record GetDayProgressQuery(int PlanId, int Day)
    : IRequest<GetDayProgressResult>;
