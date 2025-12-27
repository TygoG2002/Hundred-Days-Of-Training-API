using HundredDays.Domain.Entities;
using MediatR;

namespace HundredDays.Application.Plans.GetPlans;

public record GetPlansQuery : IRequest<List<WorkoutPlan>>;
