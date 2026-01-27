using Application.Plans.GetPlansWithProgress;
using Application.Plans.Interfaces;
using Dapper;
using HundredDays.Domain.Entities;
using Infrastructure;
using Microsoft.EntityFrameworkCore;

public class WorkoutPlanRepository : IPlanQueryRepository
{
    private readonly IDbConnectionFactory _connectionFactory; 

    public WorkoutPlanRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory; 
    }

    public async Task<List<WorkoutPlan>> GetAllAsync()
    {
        var connection = _connectionFactory.CreateConnection();
        connection.Open();

        var query = @"SELECT * FROM WorkoutPlans";

        var result = await connection.QueryAsync<WorkoutPlan>(query);
        return result.ToList();
    }

    public async Task<List<PlanOverviewDto>> GetPlansOverviewAsync()
    {
        var connection = _connectionFactory.CreateConnection();
        connection.Open();

        var query = @"SELECT
                    p.Id,
                    p.Name,
                    COUNT(d.Id) AS TotalDays,
                    SUM(CASE WHEN d.CompletedAt IS NOT NULL THEN 1 ELSE 0 END) AS CompletedDays
                FROM WorkoutPlans p
                JOIN WorkoutDays d
                    ON d.WorkoutPlanId = p.Id
                GROUP BY
                    p.Id,
                    p.Name;
";

        var result = await connection.QueryAsync<PlanOverviewDto>(query);
        return result.ToList();
    }

}
