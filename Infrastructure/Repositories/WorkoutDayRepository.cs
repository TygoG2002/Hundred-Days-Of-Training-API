using Application.Days.GetDays;
using Application.Days.Interfaces;
using Application.interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Repositories
{
    public class WorkoutDayRepository : IDayQueryRepository, IDayCommandRepository
    {
        private readonly IDbConnectionFactory _connectionFactory;

        public WorkoutDayRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<DayOverviewDto>> GetDaysAsync(int planId)
        {

            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var query = @"SELECT 
                DayNumber as DayId, 
                Completed as IsCompleted, 
                CompletedAt 
                FROM WorkoutDays 
                WHERE 
                    WorkoutPlanId = @Id 
                Order by DayNumber
            ";

            var result = await connection.QueryAsync<DayOverviewDto>(query, new { Id = planId });

            return result.ToList(); 
        }

        public async Task CompleteDayAsync(int planId, int dayId, bool completed)
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            const string sql = @"
            UPDATE WorkoutDays
            SET Completed = @Completed
            WHERE WorkoutPlanId = @PlanId
              AND DayNumber = @DayId;
        ";

            await connection.ExecuteAsync(sql, new {
                    PlanId = planId,
                    DayId = dayId,
                    Completed = completed
                }
            );
        }
    }
}
