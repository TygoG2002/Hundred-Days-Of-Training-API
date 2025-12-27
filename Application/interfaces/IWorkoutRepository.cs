using HundredDays.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.interfaces
{
    public  interface IWorkoutRepository
    {
         Task<List<WorkoutPlan>> GetAllAsync();  
    }
}
