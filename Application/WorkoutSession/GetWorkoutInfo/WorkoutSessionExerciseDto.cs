using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.GetWorkoutInfo
{
    public class WorkoutSessionExerciseDto
    {
        public int TemplateExerciseId { get; set; }
        public string Name { get; set; } = "";

        public int Sets { get; set; }
        public int TargetReps { get; set; }

        public List<WorkoutSessionSetDto> SetsData { get; set; } = [];
    }

}
