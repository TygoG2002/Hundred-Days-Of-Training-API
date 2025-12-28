using HundredDays.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.interfaces
{
    public  interface IWorkoutRepository
    {
        Task<List<WorkoutPlan>> GetAllAsync();
        Task<List<int>> GetDays(int planId);
        Task<List<WorkoutSet>> GetSets(int planId, int day );
        Task UpdateSet(int setId, bool completed);
        Task<(int done, int total)> GetDayProgress(int planId, int day);
    }
}
