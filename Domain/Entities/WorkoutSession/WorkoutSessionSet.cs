using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.WorkoutSession
{
    public class WorkoutSessionSet
    {
        public int Id { get; private set; }

        public int WorkoutSessionExerciseId { get; private set; }

        public int SetNumber { get; private set; }

        public int? Reps { get; private set; }
        public decimal? WeightKg { get; private set; }

        private WorkoutSessionSet() { }

        public WorkoutSessionSet(int workoutSessionExerciseId, int setNumber)
        {
            WorkoutSessionExerciseId = workoutSessionExerciseId;
            SetNumber = setNumber;
        }

        public void Update(int reps, decimal weightKg)
        {
            Reps = reps;
            WeightKg = weightKg;
        }
    }
}

