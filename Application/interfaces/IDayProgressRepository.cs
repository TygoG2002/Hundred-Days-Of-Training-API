namespace Application.Days.Interfaces;

public interface IDayProgressRepository
{
    Task<(int done, int total)> GetDayProgressAsync(int planId, int day);
}
