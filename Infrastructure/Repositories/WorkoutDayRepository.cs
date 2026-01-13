using Application.Days.GetDays;
using Application.Days.Interfaces;
using Application.interfaces;
using HundredDays.Domain.Entities; 
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Repositories
{
    public class WorkoutDayRepository : IDayQueryRepository, IDayCommandRepository
    {
        private readonly AppDbContext _db;

        public WorkoutDayRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<DayOverviewDto>> GetDaysAsync(int planId)
        {
            return await _db.WorkoutDays
                .Where(d => d.WorkoutPlanId == planId)
                .OrderBy(d => d.DayNumber)
                .Select(d => new DayOverviewDto
                {
                    DayId = d.DayNumber,
                    IsCompleted = d.Completed,
                    CompletedAt = d.CompletedAt
                })
                .ToListAsync();
        }


        public async Task<(int done, int total)> GetDayProgressAsync(int planId, int day)
        {
            var sets = await _db.WorkoutDays
                .Where(d => d.WorkoutPlanId == planId && d.DayNumber == day)
                .SelectMany(d => d.Sets)
                .ToListAsync();

            return (
                sets.Count(s => s.Completed),
                sets.Count
            );
        }

        public async Task CompleteDayAsync(int planId, int dayId, bool completed)
        {
            var currentDay = await _db.WorkoutDays.SingleOrDefaultAsync(d =>
                      d.WorkoutPlanId == planId &&
                      d.DayNumber == dayId);

           

            WorkoutDay.CompleteCurrentWorkout(currentDay!, completed);
            await _db.SaveChangesAsync();


                
        }
    }
}
