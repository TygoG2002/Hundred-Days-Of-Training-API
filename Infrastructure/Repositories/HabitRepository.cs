using Application.Habits.GetHabits;
using Application.interfaces;
using Application.Interfaces;
using Dapper;
using Domain.Entities.Habits;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HabitRepository : IHabitQueryRepository, IHabitCommandRepository
    {
        private readonly AppDbContext _db;

        private readonly IDbConnectionFactory _connectionFactory;
        public HabitRepository(AppDbContext db, IDbConnectionFactory connectionFactory)
        {
            _db = db;
            _connectionFactory = connectionFactory;
        }

        public async Task<List<HabitDto>> GetAllAsync()
        {
            var connection = _connectionFactory.CreateConnection();
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
                      AND x.Completed = 1
);
";
            var result = await connection.QueryAsync<HabitDto>(
                query,
                new { today = DateTime.Today });  
                return result.ToList();
        }

        public async Task UpdateValueAsync(int habitId, int amount)
        {
            if (amount <= 0)
                throw new ArgumentException("Amount must be greater than zero");

            var today = DateOnly.FromDateTime(DateTime.UtcNow);

            var habit = await _db.Habit
                .FirstOrDefaultAsync(h => h.Id == habitId);

            if (habit == null)
                throw new InvalidOperationException("Habit not found");

            var entry = await _db.HabitEntry
                .FirstOrDefaultAsync(e =>
                    e.HabitId == habitId &&
                    e.Date == today);

            if (entry == null)
            {
                entry = new HabitEntry(habitId, today);
                _db.HabitEntry.Add(entry);
            }

            if (entry.Completed)
                return;

            if (habit.Type == HabitType.BINARY)
            {
                entry.Complete();
            }
            else 
            {
                entry.AddValue(amount);

                if (entry.Value >= habit.TargetValue)
                    entry.Complete();
            }

            await _db.SaveChangesAsync();
        }



    }
}
