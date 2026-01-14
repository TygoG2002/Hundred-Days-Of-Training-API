using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.FinishWorkoutSession
{
    public class FinishWorkoutSessionSetDto
    {
        public int SetNumber { get; set; }
        public int? Reps { get; set; }
        public decimal? WeightKg { get; set; }
    }
}

