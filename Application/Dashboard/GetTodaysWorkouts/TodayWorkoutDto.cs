using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dashboard.GetTodaysWorkouts
{
    public class TodayWorkoutDto
    {
        public int PlanId { get; init; }
        public string PlanName { get; init; } = null!;
        public int DayId { get; init; }
        public int IsCompleted { get; init; }
    }

}
