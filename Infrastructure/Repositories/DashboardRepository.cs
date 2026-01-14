using Application.Dashboard.GetTodaysWorkouts;
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

        public async Task<List<TodayTemplateDto>> GetTodayTemplatesAsync()
        {
            var todayDate = DateTime.Today;
            var todayDayOfWeek = DateTime.Today.DayOfWeek;

            // Templates that are already finished today
            var finishedTemplateIdsToday = await _db.WorkoutSessions
                .Where(s =>
                    s.FinishedAt != null &&
                    s.FinishedAt.Value.Date == todayDate)
                .Select(s => s.WorkoutTemplateId)
                .Distinct()
                .ToListAsync();

            return await _db.WorkoutTemplateScheduledDays
                .Include(x => x.WorkoutTemplate)
                .Where(x => x.DayOfWeek == todayDayOfWeek)
                .Where(x => !finishedTemplateIdsToday.Contains(x.WorkoutTemplateId))
                .Select(x => new TodayTemplateDto
                {
                    TemplateId = x.WorkoutTemplateId,
                    Name = x.WorkoutTemplate.Name
                })
                .ToListAsync();
        }

        public async Task<List<PlanOverviewDto>> GetTodayWorkoutsAsync()
        {
            var today = DateTime.Today;

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
