using Application.interfaces;
using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class WorkoutRepository : IWorkoutRepository
    {

        private readonly AppDbContext _db;

        public WorkoutRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<WorkoutPlan>> GetAllAsync()
        {
            return await _db.WorkoutPlans
                .AsNoTracking()
                .ToListAsync();

        }

        public async Task<List<int>> GetDays(int planId)
        {
            return await _db.WorkoutDays
                .Where(k => k.WorkoutPlanId == planId)
                .Select(k => k.DayNumber)
                .ToListAsync();
        }

        public async Task<List<WorkoutSet>> GetSets(int planId, int day)
        {
            return await _db.WorkoutDays
                .Where(d => d.WorkoutPlanId == planId && d.DayNumber == day)
                .SelectMany(d => d.Sets)
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
