using Application.Plans.GetPlansWithProgress;
using HundredDays.Domain.Entities;

namespace Application.Plans.Interfaces;

public interface IPlanQueryRepository
{
    Task<List<WorkoutPlan>> GetAllAsync();

    Task<List<PlanOverviewDto>> GetPlansOverviewAsync();

}