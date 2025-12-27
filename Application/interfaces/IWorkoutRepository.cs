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

        Task UpdateSet(int planId, int day, int index, bool completed);

    }
}
