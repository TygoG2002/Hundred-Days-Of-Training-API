using HundredDays.Domain.Entities;

namespace Application.Plans.Interfaces;

public interface IPlanQueryRepository
{
    Task<List<WorkoutPlan>> GetAllAsync();
}