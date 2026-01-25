using Application.Plans.GetPlansWithProgress;
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

    public async Task<List<PlanOverviewDto>> GetPlansOverviewAsync()
    {
        return await _db.WorkoutPlans
    .Select(p => new PlanOverviewDto
    {
        Id = p.Id,
        Name = p.Name,
        CompletedDays = p.Days.Count(d => d.CompletedAt != null),
        TotalDays = p.Days.Count()
    })
    .ToListAsync();


    }

}
