using Application.Habits.GetHabits;
using Application.interfaces;
using Application.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HabitRepository : IHabitQueryRepository, IHabitCommandRepository
    {

        private readonly IDbConnectionFactory _connectionFactory;
        public HabitRepository(IDbConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<List<HabitDto>> GetAllAsync()
        {
            using var connection = _connectionFactory.CreateConnection();
            connection.Open();

            var query = @"SELECT h.Id, h.Name, h.Type, h.TargetValue, 
            e.Value AS TodayValue,
            e.Completed AS TodayCompleted
                FROM Habit h
                    LEFT JOIN HabitEntry e
                    ON e.HabitId = h.Id
                   AND e.Date = @today
                WHERE NOT EXISTS (
                    SELECT 1
                    FROM HabitEntry x
                    WHERE x.HabitId = h.Id
                      AND x.Date = @today
                      AND x.Completed = 1);";

            var result = await connection.QueryAsync<HabitDto>(
                query,
                new { today = DateTime.Today });  
                return result.ToList();
        }

        public async Task AddHabitValueAsync(int habitId, int amount)
        {
            using var connection = _connectionFactory.CreateConnection();
            var today = DateTime.Today;

            var sql = @"
            UPDATE HabitEntry
            SET Value = Value + @Amount
            WHERE HabitId = @HabitId
              AND Date = @Today;

            INSERT INTO HabitEntry (HabitId, Date, Value, Completed)
            SELECT @HabitId, @Today, @Amount, 0
            WHERE ROW_COUNT() = 0;
            ";

            await connection.ExecuteAsync(sql, new { HabitId = habitId, Amount = amount, Today = today }
            );
        }

        public async Task CompleteHabitAsync(int habitId)
        {
            using var connection = _connectionFactory.CreateConnection();
            var today = DateTime.Today;

            const string sql = """
                UPDATE HabitEntry
                SET Completed = 1
                WHERE HabitId = @HabitId
                  AND Date = @Today;

                INSERT INTO HabitEntry (HabitId, Date, Value, Completed)
                SELECT @HabitId, @Today, 0, 1
                WHERE ROW_COUNT() = 0;
        """;

            await connection.ExecuteAsync(sql, new
            {
                HabitId = habitId,
                Today = today
            });
        }


    }




}

