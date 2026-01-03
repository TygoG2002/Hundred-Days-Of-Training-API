using Application.Plans.Interfaces;
using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;

public class WorkoutPlanRepository : IPlanQueryRepository
{
    private readonly AppDbContext _db;

    public WorkoutPlanRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<WorkoutPlan>> GetAllAsync()
    {
        return await _db.WorkoutPlans
            .AsNoTracking()
            .ToListAsync();
    }
}
