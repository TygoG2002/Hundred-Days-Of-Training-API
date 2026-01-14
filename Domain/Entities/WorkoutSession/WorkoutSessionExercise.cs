using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WorkoutSession
{
    public class WorkoutSessionExercise
    {
        public int Id { get; private set; }

        public int WorkoutSessionId { get; private set; }

        public string Name { get; private set; } = "";
        public int TargetSets { get; private set; }
        public int TargetReps { get; private set; }
        public int RestSeconds { get; private set; }

        private readonly List<WorkoutSessionSet> _sets = new();
        public IReadOnlyCollection<WorkoutSessionSet> Sets => _sets;

        private WorkoutSessionExercise() { }

        public WorkoutSessionExercise(
            int workoutSessionId,
            string name,
            int targetSets,
            int targetReps,
            int restSeconds)
        {
            WorkoutSessionId = workoutSessionId;
            Name = name;
            TargetSets = targetSets;
            TargetReps = targetReps;
            RestSeconds = restSeconds;
        }

        public void AddSet(int setNumber, int WorkoutSessionId)
        {
            _sets.Add(new WorkoutSessionSet(WorkoutSessionId, setNumber));
        }
    }
}
