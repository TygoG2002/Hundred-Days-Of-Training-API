using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.FinishWorkoutSession
{

    public class FinishWorkoutSessionExerciseDto
    {
        public int WorkoutSessionExerciseId { get; set; }

        public List<FinishWorkoutSessionSetDto> Sets { get; set; } = [];
    }
}


