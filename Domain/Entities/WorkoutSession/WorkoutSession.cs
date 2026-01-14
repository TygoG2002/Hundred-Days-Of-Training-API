using Domain.Entities.Template;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WorkoutSession
{
    public class WorkoutSession
    {
        public int Id { get; private set; }
        public int WorkoutTemplateId { get; private set; }

        public WorkoutTemplate WorkoutTemplate { get; private set; } = null!;

        public DateTime StartedAt { get; private set; }
        public DateTime? FinishedAt { get; private set; }

        private readonly List<WorkoutSessionExercise> _exercises = new();
        public IReadOnlyCollection<WorkoutSessionExercise> Exercises => _exercises;

        private WorkoutSession() { }

        public WorkoutSession(int workoutTemplateId)
        {
            WorkoutTemplateId = workoutTemplateId;
            StartedAt = DateTime.UtcNow;
        }

        public void Finish()
        {
            FinishedAt = DateTime.UtcNow;
        }
    }

}
