using Application.Sets.Interfaces;
using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class WorkoutSetRepository : ISetCommandRepository, ISetQueryRepository
{
    private readonly AppDbContext _db;

    public WorkoutSetRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<WorkoutSet>> GetSetsAsync(int planId, int day)
    {
        return await _db.WorkoutDays
            .Where(d => d.WorkoutPlanId == planId && d.DayNumber == day)
            .SelectMany(d => d.Sets)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task UpdateSetAsync(int setId, bool completed)
    {
        var set = await _db.WorkoutSets.FirstAsync(s => s.Id == setId);
        set.MarkCompleted(completed);
        await _db.SaveChangesAsync();
    }
}
