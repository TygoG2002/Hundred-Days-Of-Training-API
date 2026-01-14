using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.FinishWorkoutSession
{
    public class FinishWorkoutSessionRequest
    {
        public int SessionId { get; set; }
        public List<FinishWorkoutSessionExerciseDto> Exercises { get; set; } = new();
    }

}
