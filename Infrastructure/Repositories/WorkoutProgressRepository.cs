using Application.Days.Interfaces;
using Microsoft.EntityFrameworkCore;

public class WorkoutProgressRepository : IDayProgressRepository
{
    private readonly AppDbContext _db;

    public WorkoutProgressRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<(int done, int total)> GetDayProgressAsync(int planId, int day)
    {
        var sets = await _db.WorkoutDays
            .Where(d => d.WorkoutPlanId == planId && d.DayNumber == day)
            .SelectMany(d => d.Sets)
            .ToListAsync();

        return (
            sets.Count(s => s.Completed),
            sets.Count
        );
    }
}
