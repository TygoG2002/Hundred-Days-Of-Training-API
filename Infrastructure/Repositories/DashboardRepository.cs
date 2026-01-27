using Application.Dashboard.GetTodaysWorkouts;
using Application.Dashboard.GetWeekPlanningWorkouts;
using Application.Dashboard.UpdateWeekPlanning;
using Application.interfaces;
using Application.Plans.GetPlansWithProgress;
using Dapper;


namespace Infrastructure.Repositories
{
    public class DashboardRepository : IDashboardQueryRepository, IDashboardCommandRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;
        public DashboardRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
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
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string query = """
               SELECT DISTINCT
            p.Id,
            p.Name
        FROM WorkoutPlans p
        JOIN WorkoutDays d 
            ON d.WorkoutPlanId = p.Id
        GROUP BY p.Id, p.Name
        HAVING
            SUM(d.CompletedAt IS NULL) > 0
            AND SUM(
                d.CompletedAt IS NOT NULL
                AND DATE(d.CompletedAt) = CURDATE()
            ) = 0;
        
        """;

            var result = await connection.QueryAsync<PlanOverviewDto>(query);
            return result.ToList();
        }


        public async Task<List<GetWeekPlanningDto>> GetWeekPlanningAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
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
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var newDayOfWeek = request.DayOfWeek;
            var requestId = request.Id;

            var query = """
                 UPDATE WorkoutTemplateScheduledDay SET 
                DayOfWeek = @DayOfWeek WHERE Id = @Id;
                """;

            var result = await connection.QueryAsync<UpdateWeekPlanningDto>(query,
               new { DayOfWeek = newDayOfWeek, Id = requestId });
        }
    }
}