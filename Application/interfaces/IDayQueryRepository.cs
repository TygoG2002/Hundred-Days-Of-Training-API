namespace Application.Days.Interfaces;

public interface IDayQueryRepository
{
    Task<List<int>> GetDaysAsync(int planId);
    Task<(int done, int total)> GetDayProgressAsync(int planId, int day);

}
