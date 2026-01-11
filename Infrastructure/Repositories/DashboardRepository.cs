using Application.interfaces;
using Application.Plans.GetPlansWithProgress;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    internal class DashboardRepository : IDashboardQueryRepository
    {
        private readonly AppDbContext _db;

        public DashboardRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<PlanOverviewDto>> GetTodayWorkouts()
        {
            var days = await _db.WorkoutDays
                .Include(d => d.Plan)
                .ToListAsync();

            return days
                .GroupBy(d => d.WorkoutPlanId)
                .Select(group =>
                {
                    var plan = group.First().Plan;

                    var completedDays = group.Count(d => d.CompletedAt != null);
                    var totalDays = group.Count();

                    if (completedDays >= totalDays)
                        return null;

                    return new PlanOverviewDto(
                        plan.Id,
                        plan.Name,
                        completedDays,
                        totalDays
                    );
                })
                .Where(x => x != null)
                .ToList()!;
        }
    }
}
