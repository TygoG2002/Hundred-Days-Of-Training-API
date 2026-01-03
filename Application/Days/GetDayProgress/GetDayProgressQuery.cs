using MediatR;

namespace Application.Days.GetDayProgress;

public record GetDayProgressQuery(int PlanId, int Day) : IRequest<GetDayProgressResult>;
