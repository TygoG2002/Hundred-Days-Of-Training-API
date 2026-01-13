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

        public async Task<List<PlanOverviewDto>> GetTodayWorkoutsAsync()
        {
            var today = DateTime.Now.Date;

            var days = await _db.WorkoutDays
                .Include(d => d.Plan)
                .ToListAsync();

            return days
                .GroupBy(d => d.WorkoutPlanId)
                .Select(group =>
                {
                    var plan = group.First().Plan;

                    var nextDay = group
                        .OrderBy(d => d.DayNumber)
                        .FirstOrDefault(d => d.CompletedAt == null);

                    if (nextDay == null)
                        return null; 

                    var doneToday = group.Any(d =>
                        d.CompletedAt != null &&
                        d.CompletedAt.Value.Date == today);

                    if (doneToday)
                        return null;

                    var completedDays = group.Count(d => d.CompletedAt != null);
                    var totalDays = group.Count();

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
