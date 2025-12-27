using Application.interfaces;
using HundredDays.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

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

    }
}
