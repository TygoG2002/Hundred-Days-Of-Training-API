using Application.Dashboard.GetTodaysWorkouts;
using Application.Dashboard.GetWeekPlanningWorkouts;
using Application.Dashboard.UpdateWeekPlanning;
using Application.interfaces;
using Application.Plans.GetPlansWithProgress;
using Dapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Data;

namespace Infrastructure.Repositories
{
    internal class DashboardRepository : IDashboardQueryRepository, IDashboardCommandRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        private readonly AppDbContext _db; 

        public DashboardRepository(IDbConnectionFactory connectionFactory, AppDbContext db)
        {
            _connectionFactory = connectionFactory;
            _db = db;
        }

        public async Task<List<TodayTemplateDto>> GetTodayTemplatesAsync()
        {
            var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var dayOfWeek = DateTime.Today.DayOfWeek;

            string query = """
                SELECT DISTINCT
                    w.Id       AS TemplateId,
                    w.Name
                FROM WorkoutTemplate w
                JOIN WorkoutTemplateScheduledDay sd 
                    ON sd.WorkoutTemplateId = w.Id
                LEFT JOIN WorkoutSession s
                    ON s.WorkoutTemplateId = w.Id
                    AND DATE(s.FinishedAt) = CURDATE()
                WHERE sd.DayOfWeek = @DayOfWeek
                  AND s.Id IS NULL;
                """;

            var result = await connection.QueryAsync<TodayTemplateDto>(query, 
                new { DayOfWeek = dayOfWeek });

            connection.Close();
            return result.ToList(); 
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
     
        public async Task<List<GetWeekPlanningDto>> GetWeekPlanningAsync()
        {
            var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var query = """
                SELECT w.Id, w.DayOfWeek, w.WorkoutTemplateId AS templateId 
                FROM WorkoutTemplateScheduledDay w; 
                """;

            var result = await connection.QueryAsync<GetWeekPlanningDto>(query);
            connection.Close();
            return result.ToList();
        }

        public async Task UpdateWeekPlanningAsync(UpdateWeekPlanningDto request)
        {
            var existing = await _db.WorkoutTemplateScheduledDays
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (existing == null)
                return;

            existing.UpdateDayOfWeek(request.DayOfWeek);

            await _db.SaveChangesAsync();
        }


    }
}
