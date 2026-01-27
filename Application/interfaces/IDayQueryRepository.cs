using Application.Days.GetDays;

namespace Application.Days.Interfaces;

public interface IDayQueryRepository
{
    Task<List<DayOverviewDto>> GetDaysAsync(int planId);

}
