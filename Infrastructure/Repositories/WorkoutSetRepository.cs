using Application.Sets.Interfaces;
using Dapper;
using HundredDays.Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class WorkoutSetRepository : ISetQueryRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public WorkoutSetRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<List<WorkoutSet>> GetSetsAsync(int planId, int day)
    {
        using var connection = _connectionFactory.CreateConnection();

        const string sql = """
        SELECT s.*
        FROM WorkoutDays d
        JOIN WorkoutSets s
            ON s.WorkoutDayId = d.Id
        WHERE d.WorkoutPlanId = @PlanId
          AND d.DayNumber = @Day;
        """;

        var result = await connection.QueryAsync<WorkoutSet>(sql, new
            {
                PlanId = planId,
                Day = day
            }
        );

        return result.ToList();
    }

}
