using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.GetWorkoutInfo
{
    public class WorkoutSessionSetDto
    {
        public int SetNumber { get; set; }

        public int? LastReps { get; set; }
        public decimal? LastWeight { get; set; }

        public int? CurrentReps { get; set; }
        public decimal? CurrentWeight { get; set; }
    }

}
