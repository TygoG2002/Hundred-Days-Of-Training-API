namespace Application.Days.Interfaces;

public interface IDayQueryRepository
{
    Task<List<int>> GetDaysAsync(int planId);
}
