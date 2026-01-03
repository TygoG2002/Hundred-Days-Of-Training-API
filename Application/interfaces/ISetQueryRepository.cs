using HundredDays.Domain.Entities;

namespace Application.Sets.Interfaces;

public interface ISetQueryRepository
{
    Task<List<WorkoutSet>> GetSetsAsync(int planId, int day);
}
