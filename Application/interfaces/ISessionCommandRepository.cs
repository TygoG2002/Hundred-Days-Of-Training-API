using Application.WorkoutSession.FinishWorkoutSession;
using Application.WorkoutSession.StartWorkoutSession;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.interfaces
{
    public interface ISessionCommandRepository
    {
        Task<WorkoutSessionDto> StartWorkoutSessionAsync(int templateId);

        Task SaveWorkoutSessionAsync(int sessionId, List<FinishWorkoutSessionExerciseDto> exercises);
    }
}
