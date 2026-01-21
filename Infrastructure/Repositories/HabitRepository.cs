using Application.Habits.GetHabits;
using Application.interfaces;
using Application.Interfaces;
using Domain.Entities.Habits;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HabitRepository : IHabitQueryRepository, IHabitCommandRepository
    {
        private readonly AppDbContext _db;

        public HabitRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<HabitDto>> GetAllAsync()
        {
            var today = DateOnly.FromDateTime(DateTime.Now);

            return await _db.Habit
                .AsNoTracking()
                .Where(h =>
                    !_db.HabitEntry.Any(e =>
                        e.HabitId == h.Id &&
                        e.Date == today &&
                        e.Completed))
                .Select(h => new HabitDto
                {
                    Id = h.Id,
                    Name = h.Name,
                    Type = h.Type,
                    TargetValue = h.TargetValue,

                    TodayValue = _db.HabitEntry
                        .Where(e =>
                            e.HabitId == h.Id &&
                            e.Date == today)
                        .Select(e => e.Value)
                        .FirstOrDefault(),

                    TodayCompleted = _db.HabitEntry
                        .Where(e =>
                            e.HabitId == h.Id &&
                            e.Date == today)
                        .Select(e => e.Completed)
                        .FirstOrDefault()
                })
                .ToListAsync();
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
