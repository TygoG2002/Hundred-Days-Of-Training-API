using Application.Days.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WorkoutDayRepository : IDayQueryRepository
    {
        private readonly AppDbContext _db;

        public WorkoutDayRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<int>> GetDaysAsync(int planId)
        {
            return await _db.WorkoutDays
                .Where(d => d.WorkoutPlanId == planId)
                .Select(d => d.DayNumber)
                .ToListAsync();
        }
    }
}
