using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.WorkoutSession.GetWorkoutInfo
{
    public class WorkoutSessionDetailsDto
    {
        public int SessionId { get; set; }
        public int WorkoutTemplateId { get; set; }
        public string TemplateName { get; set; } = "";
        public DateTime StartedAt { get; set; }

        public List<WorkoutSessionExerciseDto> Exercises { get; set; } = [];
    }

}
