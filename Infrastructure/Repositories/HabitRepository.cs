using Application.Habbit.GetHabbits;
using Application.Habits.GetHabits;
using Application.interfaces;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class HabitRepository : IHabitQueryRepository
    {
        private readonly AppDbContext _db;

        public HabitRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<HabitDto>> GetAllAsync()
        {
            return await _db.Habit
                .AsNoTracking()
                .Select(h => new HabitDto
                {
                    Id = h.Id,
                    Name = h.Name
                })
                .ToListAsync();
        }
    }
}
